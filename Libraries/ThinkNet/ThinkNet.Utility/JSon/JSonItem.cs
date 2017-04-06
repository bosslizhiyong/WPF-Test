using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkNet.Utility
{
    /// <summary>
    /// 通用JSon类描述
    /// </summary>
    public class JSonItem : IComparable
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        public JSonItem()
            : this(0, "", "", "", "", "")
        {

        }
        public JSonItem(int id, string code, string name, string value, string text, string description)
        {
            this.ID = id;
            this.Code = code;
            this.Name = name;
            this.Value = value;
            this.Text = text;
            this.Description = description;
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            int result = 0;
            try
            {
                JSonItem info = obj as JSonItem;
                if (!ParameterValidator.IsNumber(info.Value.Trim()))
                {
                    return 0;
                }
                if (Convert.ToDecimal(this.Value.Trim()) < Convert.ToDecimal(info.Value.Trim()))
                {
                    result = -1;
                }
                else if (Convert.ToDecimal(this.Value.Trim()) > Convert.ToDecimal(info.Value.Trim()))
                {
                    result = 1;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            return result;  
        }
    }

}

