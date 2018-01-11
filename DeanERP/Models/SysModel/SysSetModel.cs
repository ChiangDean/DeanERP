using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeanERP.Models.SysModel
{
    /// <summary>
    /// 2018/1/10 By Dean_Chiang
    /// </summary>
    public class SysSetModel
    {
        [Key]
        public string SET_ITEM { get; set; }

        public string SET_TYPE { get; set; }

        public string SET_VALUE { get; set; }

        public string MEMO { get; set; }
    }
}