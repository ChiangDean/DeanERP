using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeanERP.Models.OrderFood
{
    public class OrderStoreModel
    {
        [Display(Name = "店家代號")]
        public int STORE_ID { get; set; }

        
        [Display(Name = "店家名稱")]
        [Required]
        [StringLength(50, ErrorMessage = "店家名稱必須少於50字！")]

        public string STORE_NAME { get; set; }


        [Display(Name = "店家狀態")]
        [Required]
        public string STORE_STATUS { get; set; }


        [Display(Name = "店家型態")]
        [Required]
        public string STORE_TYPE { get; set; }

        [Display(Name = "外送金額")]
        [RegularExpression(@"[0-9]{0,4}", ErrorMessage = "外送金額必須介於0~9999之間")]
        public string STORE_COST { get; set; }


        [Display(Name = "服務方式")]
        [Required]
        public string STORE_DELIVERY { get; set; }

        [Display(Name = "最少外送份數")]
        [RegularExpression(@"[0-9]{0,3}", ErrorMessage = "最少外送份數必須介於0~999之間")]

        public string STORE_COPIES { get; set; }


        [Display(Name = "黑單次數")]
        public int STORE_BLICKLIST { get; set; }


        [Display(Name = "訂購次數")]
        public int STORE_FREQ { get; set; }


        [Display(Name = "最後訂購時間")]
        public string STORE_FINALDATE { get; set; }


        [Display(Name = "店家電話1")]
        [RegularExpression(@"[0-9]{0,15}", ErrorMessage = "店家電話1必須為數字")]


        public string STORE_TEL1 { get; set; }


        [Display(Name = "店家電話2")]
        [RegularExpression(@"[0-9]{0,15}", ErrorMessage = "店家電話2必須為數字")]
        public string STORE_TEL2 { get; set; }


        [Display(Name = "店家地址")]
        [StringLength(50, ErrorMessage = "店家地址必須少於50字！")]

        public string STORE_ADDRESS { get; set; }


        [Display(Name = "公司位置")]
        [Required]
        public string STORE_SUB { get; set; }

        [Display(Name = "菜單圖片檔名")]
        [RegularExpression(@"[a-zA-z0-9!@#$%^&()_+{}]{0,50}", ErrorMessage = "菜單JPG圖片檔名必須是英文與數字混合！(不用.jpg)")]
        [StringLength(50, ErrorMessage = "店家菜單必須少於50字！")]

        public string STORE_MENUURL { get; set; }


        [Display(Name = "店家備註")]
        [StringLength(100, ErrorMessage = "店家菜單必須少於100字！")]

        public string STORE_REMARK { get; set; }
    }
}