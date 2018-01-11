using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Reflection;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Collections;
using DeanERP.Models.SysModel;

namespace DeanERP.Dac
{
    /// <summary>
    /// 2018/1/10 By Dean_Chiang
    /// </summary>
    public class _ERPDBDac: IDisposable
    {
        protected DataContext dc;
        protected StringBuilder sbSql;
        protected IList<SysSetModel> errorMsgs;
        public _ERPDBDac()
        {
            dc = new DataContext(ConfigurationManager.ConnectionStrings["DeanERPConnection"].ConnectionString);
            dc.CommandTimeout = 600;
            dc.ObjectTrackingEnabled = false;
            sbSql = new StringBuilder();
            //errorMsgs = dc.ExecuteQuery<SysSetModel>(@"select * from [VW_SYSSET] (NOLOCK) where SET_ITEM='ErrorMsg'").ToList();
        }

        /// <summary>
        /// 2018/1/10 By Dean_Chiang
        /// </summary>
        public void Dispose()
        {
            dc.Connection.Close();
            dc.Dispose();
            GC.SuppressFinalize(this);
        }



        /// <summary>
        /// 取得系統內建訊息
        /// </summary>
        /// <param name="setType">訊息代碼</param>
        /// <returns></returns>
        protected string GetErrorMsg(string setType)
        {
            try
            {
                return errorMsgs.Where(p => p.SET_TYPE == setType).Select(p => p.SET_VALUE).SingleOrDefault();
            }
            catch
            {
                return "查詢無符合資料！";
            }
        }

        /// <summary>
        /// 取得系統當前日期(格式yyyyMMdd)
        /// </summary>
        /// <returns></returns>
        protected string GetDate()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }
        /// <summary>
        /// 取得系統當前時間(格式HH:mm:ss)
        /// </summary>
        /// <returns></returns>
        protected string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }




        /// <summary>
        /// 執行 SQL 指令
        /// 將傳入 Model 中的 string 型別成員去除前後空白和編碼(Encode)
        /// </summary>
        /// <param name="dc">DataContext</param>
        /// <param name="strSql">SQL 語句</param>
        /// <param name="objParas">SQL 參數</param>
        /// <remarks>
        ///     1. 此函數是針對資料庫設定 NULL 值而產生
        ///     2. Created by Steven Tsai at 2016/08/12
        /// </remarks>
        /// <returns>資料列影響筆數</returns>
        public int ExecuteNoQuery(DataContext dc, string strSql, params object[] objParas)
        {
            for (int i = 0; i < objParas.Length; i++)
            {
                if (objParas[i] == null || Convert.IsDBNull(objParas[i]))
                {
                    strSql = strSql.Replace("{" + i.ToString() + "}", "NULL");
                    objParas[i] = "";
                }
                else if (objParas[i].GetType() == typeof(string))
                {
                    objParas[i]= HttpUtility.HtmlEncode(objParas[i].ToString().Trim()); 
                }
            }
            return dc.ExecuteCommand(strSql, objParas);
        }

        /// <summary>
        /// 將傳入 Model 中的 string 型別成員去除前後空白和編碼(Encode)。
        /// </summary>
        /// 2016/08/15 Created by Leon
        /// 2016/08/16 Modify  by Dean 同時增加編碼(Encode)
        /// <typeparam name="T">Model 的型別</typeparam>
        /// <param name="model">要處理的 Model</param>
        public T PreProcessModel<T>(T model)
        {
            if (model is IList)
            {
                foreach (var m in (model as IList))
                {
                    foreach (var prop in m.GetType().GetProperties())
                    {
                        if (prop.PropertyType == typeof(string) && prop.GetValue(m) != null)
                        { prop.SetValue(m, HttpUtility.HtmlEncode(prop.GetValue(m).ToString().Trim())); }
                    }
                }
            }
            else
            {
                foreach (var prop in model.GetType().GetProperties())
                {
                    if (prop.PropertyType == typeof(string) && prop.GetValue(model) != null)
                    { prop.SetValue(model, HttpUtility.HtmlEncode(prop.GetValue(model).ToString().Trim())); }
                }
            }

            return model;
        }

    }

}