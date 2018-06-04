using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project2018ASP.MyDataSetTableAdapters;

namespace Project2018ASP
{
    using TableEmployee = MyDataSet.EmployeeDataTable;
    using TableDepartment = MyDataSet.DepartmentDataTable;
    using System.Web.UI.HtmlControls;

    public partial class RHPage : System.Web.UI.MasterPage
    {
        EmployeeTableAdapter adpEmployee = new EmployeeTableAdapter();
        TableEmployee tblEmployee = new TableEmployee();

        DepartmentTableAdapter adpDepartment = new DepartmentTableAdapter();
        TableDepartment tblDepartment = new TableDepartment();

        public void refresh()
        {
            if (Session["DepartmentId"] != null || Session["EmployeeId"] != null)
            {
                int departmentID = int.Parse(Session["DepartmentId"].ToString());
                int employeeId = int.Parse(Session["EmployeeId"].ToString());
                mountPage(departmentID);
                tblEmployee = adpEmployee.GetDataBy(employeeId);
                Cache["tblEmployee"] = tblEmployee;
                lblEmployeeName.Text = "Hello " + tblEmployee[0].EmployeeName;
            }
            else
            {
                Response.Redirect("~/LoginForm.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                refresh();

            }
        }
        // hr Informations
        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/EmployeeForm.aspx");
        }

        protected void btnAddDepartment_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AddDepartmentForm.aspx");
        }

        protected void btnAddJobTitle_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AddJobTitleForm.aspx");
        }
        // Products 
        protected void btnCategory_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CategoryForm.aspx");
        }

        protected void btnProduct_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ProductForm.aspx");
        }
        // Order
        protected void btnAddOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/OrderForm.aspx");
        }

        private void mountPage(int deptid)
        {
            switch (deptid)
            {
                case 28: // HR
                    panelHR.Visible = true;
                    panelOrder.Visible = false;
                    panelProduct.Visible = false;
                    break;
                case 33: // Inventory
                case 34: // Purchasing
                    panelHR.Visible = false;
                    panelOrder.Visible = false;
                    panelProduct.Visible = true;
                    break;
                case 29: // Sales
                    panelHR.Visible = false;
                    panelOrder.Visible = true;
                    panelProduct.Visible = false;
                    break;
                default:
                    panelHR.Visible = true;
                    panelOrder.Visible = true;
                    panelProduct.Visible = true;
                    break;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["DepartmentId"] = null;
            Session["EmployeeId"] = null;
            Response.Redirect("~/LoginForm.aspx");
        }

        protected void btnAllOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AllOrderForm.aspx");
        }

        protected void btnSerachOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SearchOrderForm.aspx");
        }
    }
}