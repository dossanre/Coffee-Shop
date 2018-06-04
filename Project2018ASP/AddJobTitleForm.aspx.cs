using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project2018ASP.MyDataSetTableAdapters;

namespace Project2018ASP
{
    using TableDepartment = MyDataSet.DepartmentDataTable;
    using TableJobTitle = MyDataSet.JobTitleDataTable;
    using TableJobEDepartment = MyDataSet.JobEDepartmentDataTable;

    public partial class AddJobTitleForm : System.Web.UI.Page
    {
        DepartmentTableAdapter adpDepartment = new DepartmentTableAdapter();
        TableDepartment tblDepartment = new TableDepartment();

        JobTitleTableAdapter adpJobTitle = new JobTitleTableAdapter();
        TableJobTitle tblJobTitle = new TableJobTitle();

        JobEDepartmentTableAdapter adpJobEDepartment = new JobEDepartmentTableAdapter();
        TableJobEDepartment tblJobEDepartment = new TableJobEDepartment();

        private void Refresh()
        {

            if (Session["DepartmentId"] == null || Session["EmployeeId"] == null)
            {
                Response.Redirect("~/LoginForm.aspx");
            }

            tblDepartment = adpDepartment.GetData();
            Cache["tblDepartment"] = tblDepartment;

            ddlDepartment.DataSource = tblDepartment;
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentId";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("Select a Department", "-1"));

            tblJobEDepartment = adpJobEDepartment.GetDataMyData();
            Cache["JobEDepartment"] = tblJobEDepartment;

            grdJobTitle.DataSource = tblJobEDepartment;
            grdJobTitle.DataBind();
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
            if (ddlDepartment.SelectedIndex > 0)
            {
                int deptid = int.Parse(ddlDepartment.Text);
                string jobtitle = txtJobTitle.Text;
                if (string.IsNullOrEmpty(jobtitle))
                {
                    lblErrorMessage.Text = "Please Enter a Job Title";
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    insertJobTitle(jobtitle, deptid);
                }
            }
            else
            {
                lblErrorMessage.Text = "Please Enter a Department";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        private void insertJobTitle(string jobtitle, int dept)
        {

            tblJobTitle = adpJobTitle.GetData();
            Cache["tblJobTitle"] = tblJobTitle;

            int result = adpJobTitle.Insert(jobtitle, dept);

            if (result == 1)
            {
                lblErrorMessage.Text = "Job Title Inserted ";
                lblErrorMessage.ForeColor = System.Drawing.Color.Yellow;
                txtJobTitle.Text = "";
                ddlDepartment.SelectedIndex = -1;
            }
            else
            {
                lblErrorMessage.Text = "Job Title NOT Inserted";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }
            Refresh();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobTitleNumber.Text))
            {
                lblErrorMessage.Text = "Please Insert the Job Title Number";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                loadFields();
            }
        }

        private void loadFields()
        {
            int id = int.Parse(txtJobTitleNumber.Text);

            tblJobTitle = adpJobTitle.GetDataBy(id);
            Cache["tblJobTitle"] = tblJobTitle;

            var row = tblJobTitle;

            if (row.Count > 0)
            {
                if (row != null)
                {
                    string name = row[0].JobTitleName.ToString();
                    txtJobTitle.Text = name.Trim();
                    int deptid = row[0].JobDepartment;
                    ddlDepartment.SelectedValue = deptid.ToString();
                }
            }
            else
            {
                lblErrorMessage.Text = " Department ID " + id + " is not found";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }

        }

        protected void Update_Click(object sender, EventArgs e)
        {
            tblJobTitle = adpJobTitle.GetData();
            Cache["tblJobTitle"] = tblJobTitle;

            if (string.IsNullOrEmpty(txtJobTitleNumber.Text))
            {
                lblErrorMessage.Text = "Please Insert the Job Title Number";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                if (int.Parse(ddlDepartment.SelectedValue) <= 0)
                {
                    lblErrorMessage.Text = "Please Select a Department";
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    if (string.IsNullOrEmpty(txtJobTitle.Text))
                    {
                        lblErrorMessage.Text = "Please Enter a Job Title";
                        lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        updateRow();
                    }
                }
            }
        }
        private void updateRow()
        {

            int id = int.Parse(txtJobTitleNumber.Text);
            string name = txtJobTitle.Text;
            int dptID = int.Parse(ddlDepartment.SelectedValue);

            int result = adpJobTitle.Update(name, dptID, id);

            if (result == 1)
            {
                lblErrorMessage.Text = "Job Title Update ";
                lblErrorMessage.ForeColor = System.Drawing.Color.Yellow;
                txtJobTitle.Text = "";
                txtJobTitleNumber.Text = "";
                Refresh();
            }
            else
            {
                lblErrorMessage.Text = "Job Title NOT Update";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}