using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Menu
/// </summary>
public class Menu
{  
		public int ID { get; set; }
        public string Code { get; set; }        
        public string Description { get; set; }     
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }      
}