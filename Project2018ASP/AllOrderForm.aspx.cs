using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project2018ASP.MyDataSetTableAdapters;

namespace Project2018ASP
{
    using TableAllOrder = MyDataSet.AllOrderDataTable;

    public partial class AllOrderForm : System.Web.UI.Page
    {
        AllOrderTableAdapter adpAllOrder = new AllOrderTableAdapter();
        TableAllOrder tblAllOrder = new TableAllOrder();

        private void Refresh()
        {
            if (Session["DepartmentId"] == null || Session["EmployeeId"] == null)
            {
                Response.Redirect("~/LoginForm.aspx");
            }

            tblAllOrder = adpAllOrder.GetData();

            grdAllOrder.DataSource = tblAllOrder;
            grdAllOrder.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Refresh();
            }

        }
    }
}