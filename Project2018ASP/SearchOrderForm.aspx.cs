using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project2018ASP.MyDataSetTableAdapters;

namespace Project2018ASP
{
    using TableOrdercf = MyDataSet.OrdercfDataTable;
    using TableOrderDetails = MyDataSet.OrderDetailsDataTable;

    public partial class SearchOrderForm : System.Web.UI.Page
    {
        OrdercfTableAdapter adpAllOrdercf = new OrdercfTableAdapter();
        TableOrdercf tblOrdercf = new TableOrdercf();

        OrderDetailsTableAdapter adpOrderDetails = new OrderDetailsTableAdapter();
        TableOrderDetails tblOrderDetails = new TableOrderDetails();

        private void Refresh()
        {
            if (Session["DepartmentId"] == null || Session["EmployeeId"] == null)
            {
                Response.Redirect("~/LoginForm.aspx");
            }
            tblOrdercf = adpAllOrdercf.GetData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Refresh();
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblErrorMessage.Text = "";
            if (String.IsNullOrEmpty(txtOrderNumber.Text))
            {
                lblErrorMessage.Text = "Please enter an order number";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }
            else {
                int id = int.Parse(txtOrderNumber.Text);

                tblOrdercf = adpAllOrdercf.GetDataByID(id);
                var row = tblOrdercf;

                if (row.Count > 0)
                {
                    if (row != null)
                    {
                        txtDate.Text = row[0].OrderDate.ToString();
                        txtSubtotal.Text = row[0].OrderSubTotal.ToString();
                        txtTax.Text = row[0].OrderTax.ToString();
                        txtTotal.Text = row[0].OrderTotal.ToString();

                        tblOrderDetails = adpOrderDetails.GetDataByOrderId(id);
                        var rowOrdercf = tblOrderDetails;
                        //lblErrorMessage.Text = rowOrdercf.Count.ToString();
                        grdOrder.DataSource = null;
                        grdOrder.DataBind();
                        if (row.Count > 0 && row !=null)
                        {
                            grdOrder.DataSource = tblOrderDetails;
                            grdOrder.DataBind();
                        }
                        if (rowOrdercf.Count == 0)
                        {
                            lblErrorMessage.Text = "This order was not Completed";
                            lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                        }

                    }
                }
                else
                {
                    lblErrorMessage.Text = " Order ID " + id + " not found";
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                }

            }
        }
    }
}