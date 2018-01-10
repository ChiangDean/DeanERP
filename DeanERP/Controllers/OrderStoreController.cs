using DeanERP.Dac.OrderFood;
using DeanERP.Models.OrderFood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DeanERP.Controllers
{
    public class OrderStoreController : Controller
    {
        /// <summary>
        /// 訂便當-新增店家
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateOrderStore()
        {
            using (OrderStoreDac dac = new OrderStoreDac())
            {
                SelectList LIST_STORE_SUB = new SelectList(dac.GetDrowDown("SubType"), "DROP_VALUE", "DROP_NAME");
                SelectList LIST_STORE_STATUS = new SelectList(dac.GetDrowDown("StoreStatus"), "DROP_VALUE", "DROP_NAME");
                SelectList LIST_STORE_TYPE = new SelectList(dac.GetDrowDown("StoreType"), "DROP_VALUE", "DROP_NAME");
                SelectList LIST_STORE_DELIVERY = new SelectList(dac.GetDrowDown("StoreDelivery"), "DROP_VALUE", "DROP_NAME");
                ViewBag.STORE_SUB = LIST_STORE_SUB;
                ViewBag.STORE_STATUS = LIST_STORE_STATUS;
                ViewBag.STORE_TYPE = LIST_STORE_TYPE;
                ViewBag.STORE_DELIVERY = LIST_STORE_DELIVERY;
                OrderStoreModel model = new OrderStoreModel();
                return View(model);
            }
        }

        /// <summary>
        /// 訂便當-新增店家(新增動作)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult CreateOrderStore(OrderStoreModel model)
        {
            if (ModelState.IsValid)
            {
                using (OrderStoreDac dac = new OrderStoreDac())
                {
                    int InsertResult = dac.InsertStore(model);
                    return RedirectToAction("CreateOrderStore", "OrderFood");
                }
            }
            return View(model);
        }

        public ActionResult ReadOrderStore()
        {
            using (OrderStoreDac dac = new OrderStoreDac())
            {
                IList<OrderStoreModel> model = dac.GetAllStore();
                return View(model);
            }
        }


        /// <summary>
        /// 查詢店家詳細資料
        /// </summary>
        /// <returns></returns>
        public ActionResult ReadDetailOrderStore(string storeName)
        {
            using (OrderStoreDac dac = new OrderStoreDac())
            {
                OrderStoreModel model = dac.GetStoreByStoreName(storeName);
                return View(model);
            }
        }
    }
}