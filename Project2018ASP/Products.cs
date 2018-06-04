using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2018ASP
{
    public class Products
    {

        private int pproductID;

        public int PProductID
        {
            get { return pproductID; }
            set { pproductID = value; }
        }

        private string pName;

        public string PName
        {
            get { return pName; }
            set { pName = value; }
        }

        private decimal pPrice;

        public decimal PPrice
        {
            get { return pPrice; }
            set { pPrice = value; }
        }

        private int pQuantity;

        public int PQuantity
        {
            get { return pQuantity; }
            set { pQuantity = value; }
        }

        private int pProductCateg;

        public int PProductCateg
        {
            get { return pProductCateg; }
            set { pProductCateg = value; }
        }

    }
}