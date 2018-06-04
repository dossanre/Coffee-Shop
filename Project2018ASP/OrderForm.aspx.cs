using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using Project2018ASP.MyDataSetTableAdapters;

namespace Project2018ASP
{
    using TableCategory = MyDataSet.CategoryDataTable;
    using TableProduct = MyDataSet.ProductDataTable;
    using TableOrderDetails = MyDataSet.OrderDetailsDataTable;
    using TableOrdercf = MyDataSet.OrdercfDataTable;

    public partial class OrderForm : System.Web.UI.Page
    {
        CategoryTableAdapter adpCategory = new CategoryTableAdapter();
        TableCategory tblCategory = new TableCategory();

        ProductTableAdapter adpProduct = new ProductTableAdapter();
        TableProduct tblProduct = new TableProduct();

        OrderDetailsTableAdapter adpOrderDetails = new OrderDetailsTableAdapter();
        TableOrderDetails tblOrderDetails = new TableOrderDetails();

        OrdercfTableAdapter adpOrdercf = new OrdercfTableAdapter();
        TableOrdercf tblOrdercf = new TableOrdercf();

        decimal tax = 0;
        decimal subTotal = 0;
        decimal total = 0;
        int OrderId = 0;
        DataTable dt = new DataTable();
   
        private void Refresh()
        {
            tblOrdercf = adpOrdercf.GetDataMax();
            Cache["tblOrderfc"] = tblOrdercf;

            createOrder();

            txtQuantity.Text = "1";
            tblCategory = adpCategory.GetData();

            ddlCategoryProd.DataSource = tblCategory;
            ddlCategoryProd.DataTextField = "CategoryName";
            ddlCategoryProd.DataValueField = "CategoryId";
            ddlCategoryProd.DataBind();
            ddlCategoryProd.Items.Insert(0, new ListItem("Select a Category", "-1"));
            txtSubtotal.Text = subTotal.ToString("C");
            txtTax.Text = tax.ToString("C");
            txtTotal.Text = total.ToString("C");

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Refresh();
            }
        }

        public void createOrder()
        {
            tblOrdercf = adpOrdercf.GetData();

            int result = adpOrdercf.Insert(DateTime.Now, 0, 0, 0);

            tblOrdercf = adpOrdercf.GetDataMax();
            Cache["tblOrdercf"] = tblOrdercf;
            OrderId = tblOrdercf[0].OrderId;
            tblOrderDetails = adpOrderDetails.GetDataByOrderId(OrderId);
            Cache["tblOrderDetails"] = tblOrderDetails;
        }

        protected void ddlCategoryProd_SelectedIndexChanged(object sender, EventArgs e)
        {
            int categId = int.Parse(ddlCategoryProd.SelectedValue);
            tblProduct = adpProduct.GetDataByCategory(categId);

            ddlProductName.DataSource = tblProduct;
            ddlProductName.DataTextField = "ProductName";
            ddlProductName.DataValueField = "ProductId";
            ddlProductName.DataBind();
            ddlProductName.Items.Insert(0, new ListItem("Select a Product", "-1"));
        }

        protected void btnDecreaseItem_Click(object sender, EventArgs e)
        {
            int quantity = int.Parse(txtQuantity.Text);
            if (quantity > 1)
            {
                quantity--;
                txtQuantity.Text = quantity.ToString();
            }
        }

        protected void btnIncreaseItem_Click(object sender, EventArgs e)
        {
            int quant = int.Parse(txtQuantity.Text);
            quant++;
            txtQuantity.Text = quant.ToString();
        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            if (validFields())
            {
                if (!productExist())
                {
                    tblOrderDetails = Cache["tblOrderDetails"] as MyDataSet.OrderDetailsDataTable;
                    tblOrdercf = Cache["tblOrdercf"] as MyDataSet.OrdercfDataTable;
                    Products prod = getProductInformation();
                    var newRow = tblOrderDetails.NewOrderDetailsRow();
                    //newRow.OrderDetailId = autoIncrement 
                    newRow.Item = int.Parse(ddlProductName.SelectedValue);
                    
                    newRow.Product = ddlProductName.SelectedItem.ToString();
                    newRow.Price = prod.PPrice;
                    newRow.Quant = int.Parse(txtQuantity.Text);
                    newRow.OrderID = tblOrdercf[0].OrderId;
                    newRow.Num = newRow.Num*-1;
                    tblOrderDetails.AddOrderDetailsRow(newRow);

                    Cache["tblOrderDetails"] = tblOrderDetails;

                    grdOrder.DataSource = tblOrderDetails;
                    grdOrder.DataBind();

                }
                calcTotal();
                resetFields();
            }

        }

        public bool validFields()
        {
            //lblMessage.Text = ddlCategoryProd.SelectedIndex.ToString();
            if (ddlCategoryProd.SelectedIndex <= 0)
            {
                lblMessage.Text = "Please Select a category";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                ddlCategoryProd.Focus();
                return false;
            }
            if (ddlProductName.SelectedIndex <= 0)
            {
                lblMessage.Text = "Please Select a Product";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                ddlProductName.Focus();
                return false;
            }
            if (int.Parse(txtQuantity.Text) <= 0)
            {
                lblMessage.Text = "Quantity must be greater than Zero";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                txtQuantity.Focus();
                return false;
            }


            return true;
        }

        private Products getProductInformation()
        {
            Products prod = new Products();

            int id = int.Parse(ddlProductName.SelectedValue);

            tblProduct = adpProduct.GetDataBy(id);

            var row = tblProduct;

            if (row != null)
            {
                prod.PName = row[0].ProductName.ToString();
                prod.PProductID = row[0].ProductId;
                prod.PPrice = row[0].ProductPrice;
                prod.PQuantity = row[0].ProductInStock;
                prod.PProductCateg = row[0].ProductCategory;
            }

            return prod;
        }

        public void calcTotal()
        {

            double tot = 0.0;
            foreach (GridViewRow row in grdOrder.Rows)
            {
                tot += double.Parse(row.Cells[5].Text) * int.Parse(row.Cells[6].Text);
            }
            subTotal = (decimal)(tot);
            double dTax= 0.13;
            tax = subTotal * (decimal)(dTax);
            total = subTotal + tax;
            txtSubtotal.Text = subTotal.ToString("C");
            txtTax.Text = tax.ToString("C");
            txtTotal.Text = total.ToString("C");

        }

        protected void rowdeleteGridview(object sender, GridViewDeleteEventArgs e)
        {

            DataTable dtORder = (DataTable)Cache["tblOrderDetails"];

            if (dtORder.Rows.Count > 0)
            {
                dtORder.Rows[e.RowIndex].Delete();
                grdOrder.DataSource = dtORder;
                grdOrder.DataBind();
                calcTotal();
            }
        }

        public bool productExist()
        {
            foreach (GridViewRow row in grdOrder.Rows)
            {
               int quant = int.Parse(txtQuantity.Text);
               if (row.Cells[3].Text == ddlProductName.SelectedValue)
                {
                    row.Cells[6].Text = (int.Parse(row.Cells[6].Text) + quant).ToString();
                    updateDataTable(row.Cells[6].Text);
                    return true;
                }
            }
            return false;
        }

        public void resetFields()
        {
            ddlCategoryProd.SelectedIndex = -1;
            ddlProductName.Items.Clear();
            txtQuantity.Text = "1";
            lblMessage.Text = "";

        }

        public void updateDataTable(string quant)
        {
            DataTable dtORder = (DataTable)Cache["tblOrderDetails"];

            foreach (DataRow dr in dtORder.Rows) // search whole table
            {
                 if(int.Parse(dr[2].ToString()) == int.Parse(ddlProductName.SelectedValue)) // if id==2
                {
                    dr[5] = int.Parse(quant); //change the name
                                                //break; break or not depending on you
                }
            }
        }

        protected void btnFinalizeOrder_Click(object sender, EventArgs e)
        {
            tblOrderDetails = Cache["tblOrderDetails"] as MyDataSet.OrderDetailsDataTable;

            adpOrderDetails.Update(tblOrderDetails);

            tblOrdercf = Cache["tblOrdercf"] as MyDataSet.OrdercfDataTable;
            
            string subTemp = txtSubtotal.Text.Remove(0, 1);
            double calcSub = double.Parse(subTemp);

            subTotal = (decimal)(calcSub);
            double dTax = 0.13;
            tax = subTotal * (decimal)(dTax);
            total = subTotal + tax;

            tblOrdercf[0].OrderSubTotal = subTotal;
            tblOrdercf[0].OrderTax = tax;
            tblOrdercf[0].OrderTotal = total;

            adpOrdercf.Update(tblOrdercf);

            UpdateProductTable();
            //Response.Redirect("~/OrderForm.aspx");

        }

        public void UpdateProductTable()
        {
            foreach (GridViewRow row in grdOrder.Rows)
            {
                int quantity = int.Parse(row.Cells[6].Text);
                int productId = int.Parse(row.Cells[3].Text);

                tblProduct = adpProduct.GetDataBy(productId);
                int prodInStock = tblProduct[0].ProductInStock - quantity;
                adpProduct.UpdateByProductID(prodInStock, productId);
            }
            Response.Redirect("~/OrderForm.aspx");
        }

    }
}