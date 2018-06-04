using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project2018ASP.MyDataSetTableAdapters;

namespace Project2018ASP
{
    using TableCategory = MyDataSet.CategoryDataTable;
    using TableProduct = MyDataSet.ProductDataTable;
    using TableCategProduct = MyDataSet.CategProductDataTable;

    public partial class ProductForm : System.Web.UI.Page
    {
        CategoryTableAdapter adpCategory = new CategoryTableAdapter();
        TableCategory tblCategory = new TableCategory();

        ProductTableAdapter adpProduct = new ProductTableAdapter();
        TableProduct tblProduct = new TableProduct();

        CategProductTableAdapter adpCategProduct = new CategProductTableAdapter();
        TableCategProduct tblCategProduct = new TableCategProduct();

        private void Refresh()
        {
            tblCategory = adpCategory.GetData();
            Cache["tblCategory"] = tblCategory;

            ddlCategory.DataSource = tblCategory;
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryId";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("Select a Category", "-1"));

            tblCategProduct = adpCategProduct.GetData();
            Cache["tblCategProduct"] = tblCategProduct;

            grdCategProduct.DataSource = tblCategProduct;
            grdCategProduct.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Refresh();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (validFields())
            {
                insertProduct();
            }
            
        }

        private void insertProduct()
        {

            tblProduct = adpProduct.GetData();
            Cache["tblProduct"] = tblProduct;

            string name = txtProductName.Text;
            decimal price = decimal.Parse(txtPrice.Text);
            int quant = int.Parse(txtQuantity.Text);
            int categID = int.Parse(ddlCategory.SelectedValue);

            int result = adpProduct.Insert(name, price, quant, categID);

            if (result == 1)
            {
                lblErrorMessage.Text = "Product Inserted ";
                lblErrorMessage.ForeColor = System.Drawing.Color.Yellow;
                txtProductName.Text = "";
                txtProductNumber.Text = "";
                ddlCategory.SelectedIndex = -1;
                txtPrice.Text = "";
                txtQuantity.Text = "";
            }
            else
            {
                lblErrorMessage.Text = "Product NOT Inserted";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }
            Refresh();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProductNumber.Text))
            {
                lblErrorMessage.Text = "Please Insert the Product Number";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                loadFields();
            }
        }

        private void loadFields()
        {
            int id = int.Parse(txtProductNumber.Text);

            tblProduct = adpProduct.GetDataBy(id);
            Cache["tblProduct"] = tblProduct;

            var row = tblProduct;

            if (row.Count > 0)
            {
                if (row != null)
                {
                    string name = row[0].ProductName.ToString();
                    txtProductName.Text = name.Trim();
                    txtPrice.Text = row[0].ProductPrice.ToString();
                    txtQuantity.Text = row[0].ProductInStock.ToString();
                    int deptid = row[0].ProductCategory;
                    ddlCategory.SelectedValue = deptid.ToString();
                }
            }
            else
            {
                lblErrorMessage.Text = " Product ID " + id + " is not found";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }

        }

        protected void Update_Click(object sender, EventArgs e)
        {
            
            tblProduct = adpProduct.GetData();
            Cache["tblProduct"] = tblProduct;

            if (string.IsNullOrEmpty(txtProductNumber.Text))
            {
                lblErrorMessage.Text = "Please Insert the Product Number";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                if (validFields())
                {
                    updateRow();
                }
            }
        }

        private void updateRow()
        {

            string name = txtProductName.Text;
            decimal price = decimal.Parse("2.50");
            int quant = int.Parse("50");
            int categID = int.Parse(ddlCategory.SelectedValue);
            int id = int.Parse(txtProductNumber.Text);

            int result = adpProduct.Update(name,price, quant, categID, id);

            if (result == 1)
            {
                lblErrorMessage.Text = "Product Update ";
                lblErrorMessage.ForeColor = System.Drawing.Color.Yellow;
                txtProductName.Text = "";
                txtProductNumber.Text = "";
                txtPrice.Text = "";
                txtQuantity.Text = "";
                Refresh();
            }
            else
            {
                lblErrorMessage.Text = "Product NOT Updated";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        public bool validFields()
        {

            if (int.Parse(ddlCategory.SelectedValue) <= 0)
            {
                lblErrorMessage.Text = "Please Select a Category";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                return false;
            }
            if (string.IsNullOrEmpty(txtProductName.Text))
            {
                lblErrorMessage.Text = "Please Enter a Product Name";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                txtProductName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtPrice.Text))
            {
                lblErrorMessage.Text = "Please Enter a Price";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                txtPrice.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtQuantity.Text))
            {
                lblErrorMessage.Text = "Please Enter a Quantity";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                txtQuantity.Focus();
                return false;
            }
            try
            {
                decimal salary = decimal.Parse(txtPrice.Text);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "Only number are allowed 9999.99";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                txtPrice.BackColor = System.Drawing.Color.Yellow;
                txtPrice.Focus();
                return false;
            }

            return true;
        }
    }
}