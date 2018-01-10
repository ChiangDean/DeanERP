using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeanERP.Models.SysModel
{
    /// <summary>
    /// 
    /// </summary>
    public class SysSetModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string SET_ITEM { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SET_TYPE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SET_VALUE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MEMO { get; set; }
    }
}