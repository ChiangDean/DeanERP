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
        /// 2018/1/10 By Dean_Chiang
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
        /// 2018/1/10 By Dean_Chiang
        /// </summary>
        public OrderStoreModel GetStoreByStoreId(string storeName)
        {
            sbSql.Clear();
            sbSql.AppendLine(@"
                            SELECT *
                            FROM OrderFoodStore
                            WHERE STORE_ID={0}
                            ");
            var result = dc.ExecuteQuery<OrderStoreModel>(sbSql.ToString(), storeName).FirstOrDefault();
            return result;
        }

        /// <summary>
        /// 2018/1/10 By Dean_Chiang
        /// </summary>
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

        /// <summary>
        /// 2018/1/10 By Dean_Chiang
        /// </summary>
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

        /// <summary>
        /// 2018/1/11 By Dean_Chiang
        /// </summary>
        public int UpdateStore(OrderStoreModel model)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    sbSql.Clear();
                    sbSql.AppendLine(@"
                                    UPDATE OrderFoodStore
                                    SET [STORE_NAME]={0}--[STORE_NAME]
		                                    ,[STORE_STATUS]={1}--[STORE_STATUS]
		                                    ,[STORE_TYPE]= {2}--[STORE_TYPE]
		                                    ,[STORE_COST]={3}--[STORE_COST]
		                                    ,[STORE_DELIVERY]={4}--[STORE_DELIVERY]
		                                    ,[STORE_COPIES]={5}--[STORE_COPIES]
		                                    ,[STORE_TEL1]={6}--[STORE_TEL1]
		                                    ,[STORE_TEL2]={7}--[STORE_TEL2]
		                                    ,[STORE_ADDRESS]={8}--[STORE_ADDRESS]
		                                    ,[STORE_SUB]={9}--[STORE_SUB]
		                                    ,[STORE_MENUURL]={10}--[STORE_MENUURL]
		                                    ,[STORE_REMARK]={11}--[STORE_REMARK]
                                    WHERE STORE_ID={12}
                                    ");


                    int result = ExecuteNoQuery(dc, sbSql.ToString(),
                                model.STORE_NAME, model.STORE_STATUS, model.STORE_TYPE, model.STORE_COST, model.STORE_DELIVERY, model.STORE_COPIES
                                , model.STORE_TEL1, model.STORE_TEL2, model.STORE_ADDRESS, model.STORE_SUB, model.STORE_MENUURL, model.STORE_REMARK, model.STORE_ID
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
        
    }
}