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

    public partial class CategoryForm : System.Web.UI.Page
    {
        CategoryTableAdapter adpCategory = new CategoryTableAdapter();
        TableCategory tblCategory = new TableCategory();

        private void refresh()
        {
            tblCategory = adpCategory.GetData();
            Cache["tblCategory"] = tblCategory;

            grdCategory.DataSource = tblCategory;
            grdCategory.DataBind();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                refresh();
            }
        }

        protected void btnSearchc_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCategNumber.Text))
            {
                lblErrorMessage.Text = "Please Insert the Category Number";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                int id = int.Parse(txtCategNumber.Text);
                tblCategory = adpCategory.GetDataBy(id);
                Cache["tblCategory"] = tblCategory;

                var row = tblCategory;
                if (row.Count > 0)
                {
                    if (row != null)
                    {
                        string name = row[0].CategoryName.ToString();
                        txtCategName.Text = name.Trim();
                    }
                }
                else
                {
                    lblErrorMessage.Text = " Department ID " + id + " is not found";
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string category = txtCategName.Text;

            if (string.IsNullOrEmpty(category))
            {
                lblErrorMessage.Text = "Please Enter a Category Name";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                insertCategory(category);
            }
        }

        private void insertCategory(string categ)
        {

            tblCategory = adpCategory.GetData();
            Cache["tblCategory"] = tblCategory;

            int result = adpCategory.Insert(categ);

            if (result == 1)
            {
                lblErrorMessage.Text = "Category Inserted ";
                lblErrorMessage.ForeColor = System.Drawing.Color.Yellow;
                txtCategName.Text = "";
                txtCategNumber.Text = "";
            }
            else
            {
                lblErrorMessage.Text = "Category NOT Inserted";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }
            refresh();
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            tblCategory = adpCategory.GetData();
            Cache["tblCategory"] = tblCategory;

            int id = 0;

            string categ = txtCategName.Text;

            if (string.IsNullOrEmpty(txtCategNumber.Text))
            {
                lblErrorMessage.Text = "Please Insert the Category Number";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                if (string.IsNullOrEmpty(categ))
                {
                    lblErrorMessage.Text = "Please Enter a Category Name";
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    try
                    {
                        id = int.Parse(txtCategNumber.Text);
                    }
                    catch (Exception ex)
                    {
                        lblErrorMessage.Text = "Please Enter a Category Number";
                        lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                        txtCategNumber.Focus();
                    }
                    int result = adpCategory.Update(categ, id);

                    if (result == 1)
                    {
                        lblErrorMessage.Text = "Department Update ";
                        lblErrorMessage.ForeColor = System.Drawing.Color.Yellow;
                        txtCategName.Text = "";
                        txtCategNumber.Text = "";
                        refresh();
                    }
                    else
                    {
                        lblErrorMessage.Text = "Department NOT Update";
                        lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
    }
}