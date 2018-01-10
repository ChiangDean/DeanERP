using DeanERP.Models;
using DeanERP.Models.OrderFood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace DeanERP.Dac.OrderFood
{
    public class OrderStoreDac :_ERPDBDac
    {
        /// <summary>
        /// 透過店家名稱取得店家資料
        /// </summary>
        public IList<OrderStoreModel> GetAllStore()
        {
            sbSql.Clear();
            sbSql.AppendLine(@"
                            SELECT *
                            FROM OrderFoodStore
                            WHERE STORE_STATUS=N'營業'
                            ");
            var result = dc.ExecuteQuery<OrderStoreModel>(sbSql.ToString()).ToList();
            return result;
        }


        /// <summary>
        /// 透過店家名稱取得店家資料
        /// </summary>
        public OrderStoreModel GetStoreByStoreName(string storeName)
        {
            sbSql.Clear();
            sbSql.AppendLine(@"
                            SELECT *
                            FROM OrderFoodStore
                            WHERE STORE_NAME={0}
                            ");
            var result = dc.ExecuteQuery<OrderStoreModel>(sbSql.ToString(), storeName).FirstOrDefault();
            return result;
        }

        public IList<DropDownListModel> GetDrowDown(String condition)
        {
            sbSql.Clear();
            sbSql.AppendLine(@"
                                SELECT DROP_NAME,DROP_VALUE
                                FROM DropDown
                                WHERE DROP_TYPE={0}
                            ");
            var result = dc.ExecuteQuery<DropDownListModel>(sbSql.ToString(), condition).ToList();
            return PreProcessModel(result);
        }


        public int InsertStore(OrderStoreModel model)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    sbSql.Clear();
                    sbSql.AppendLine(@"
							        INSERT INTO OrderFoodStore
                                    VALUES ( {0} --[STORE_NAME]
		                                    ,{1}--[STORE_STATUS]
		                                    ,{2}--[STORE_TYPE]
		                                    ,{3}--[STORE_COST]
		                                    ,{4}--[STORE_DELIVERY]
		                                    ,{5}--[STORE_COPIES]
		                                    ,0--[STORE_BLICKLIST]
		                                    ,0--[STORE_FREQ]
		                                    ,''--[STORE_FINALDATE]
		                                    ,{6}--[STORE_TEL1]
		                                    ,{7}--[STORE_TEL2]
		                                    ,{8}--[STORE_ADDRESS]
		                                    ,{9}--[STORE_SUB]
		                                    ,{10}--[STORE_MENUURL]
		                                    ,N'蔣定文'--[STORE_SIGNNAME]
		                                    ,GETDATE()--[STORE_SIGNDATE]
		                                    ,{11}--[STORE_REMARK]
                                    )
                                    ");
                    int result = ExecuteNoQuery(dc, sbSql.ToString(), 
                        model.STORE_NAME, model.STORE_STATUS, model.STORE_TYPE, model.STORE_COST
                        , model.STORE_DELIVERY, model.STORE_COPIES, model.STORE_TEL1, model.STORE_TEL2, model.STORE_ADDRESS
                        , model.STORE_SUB, model.STORE_MENUURL, model.STORE_REMARK
                    );

                    scope.Complete();
                    return result;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        public int UpdateStore()
        {
            sbSql.Clear();
            sbSql.AppendLine(@"
                            UPDATE OrderFoodStore
                            SET [STORE_NAME]=N'楊寶寶' --[STORE_NAME]
		                            ,[STORE_STATUS]='1'--[STORE_STATUS]
		                            ,[STORE_TYPE]= N'食物'--[STORE_TYPE]
		                            ,[STORE_COST]='300'--[STORE_COST]
		                            ,[STORE_DELIVERY]=N'外送'--[STORE_DELIVERY]
		                            ,[STORE_COPIES]='3'--[STORE_COPIES]
		                            ,[STORE_TEL1]='0930198023'--[STORE_TEL1]
		                            ,[STORE_TEL2]='0930198023'--[STORE_TEL2]
		                            ,[STORE_ADDRESS]=N'台北市永吉路'--[STORE_ADDRESS]
		                            ,[STORE_SUB]=N'台北公司'--[STORE_SUB]
		                            ,[STORE_MENUURL]=''--[STORE_MENUURL]
		                            ,[STORE_REMARK]=N'無'--[STORE_REMARK]
                            WHERE STORE_ID='1'
                            ");
            return dc.ExecuteCommand(sbSql.ToString());
        }
        
        /// <summary>
        /// 2016/6/3 GCR3101 Dean_Chiang 取得Email範本
        /// </summary>
//        public IList<OrganBatchModel> GetEmailContent()
//        {
//            sbSql.Clear();
//            sbSql.AppendLine(@"
//                            SELECT MAIL_SUBJECT AS MAIL_SUBJECT --主旨
//                            ,MAIL_CONTENT AS MAIL_CONTENT --內容
//                            FROM SYS_MAIL_TMPL
//                            WHERE TEMPLATE_ID='OrganReject_Template'
//                            ");
//            return dc.ExecuteQuery<OrganBatchModel>(sbSql.ToString()).ToList();
//        }

        /// <summary>
        /// 2016/6/4 GCE3104 Dean_Chiang 取得最大彙整編號
        /// </summary>
//        public string GetBatchNo(string KEYID, string ORGAN_CODE)
//        {
//            sbSql.Clear();
//            sbSql.AppendLine(@"
//	                        SELECT MAX(BATCH_NO) AS BATCH_NO
//	                        FROM WAPL_PLAN 
//	                        WHERE CHK_YEAR=(SELECT CHK_YEAR FROM MAIN_PLAN WHERE KEYID={0})
//	                        AND ADM_ORGAN LIKE {1}
//                            ");
//            var result = dc.ExecuteQuery<string>(sbSql.ToString(), KEYID, ORGAN_CODE).FirstOrDefault();
//            return result;
//        }
    }
}