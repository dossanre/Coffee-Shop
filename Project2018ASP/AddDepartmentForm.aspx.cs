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

    public partial class AddDepartmentForm : System.Web.UI.Page
    {
        DepartmentTableAdapter adpDepartment = new DepartmentTableAdapter();
        TableDepartment tblDepartment = new TableDepartment();

        private void RefreshDepartment()
        {
            if (Session["DepartmentId"] == null || Session["EmployeeId"] == null)
            {
                Response.Redirect("~/LoginForm.aspx");
            }

            tblDepartment = adpDepartment.GetData();

            grdDepart.DataSource = tblDepartment;
            grdDepart.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshDepartment();
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string depart = txtDeptName.Text;

            if (string.IsNullOrEmpty(depart))
            {
                lblErrorMessage.Text = "Please Enter a Department Name";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                insertDepartment(depart);
            }
        }

        private void insertDepartment(string dpt)
        {

            tblDepartment = adpDepartment.GetData();

            int result = adpDepartment.Insert(dpt);

            if (result == 1)
            {
                lblErrorMessage.Text = "Department Inserted ";
                lblErrorMessage.ForeColor = System.Drawing.Color.Yellow;
                txtDeptName.Text = "";
            }
            else
            {
                lblErrorMessage.Text = "Department NOT Inserted";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }
            RefreshDepartment();
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            tblDepartment = adpDepartment.GetData();

            int id = 0;

            string depart = txtDeptName.Text;

            if (string.IsNullOrEmpty(txtDeptNumber.Text))
            {
                lblErrorMessage.Text = "Please Insert the Department Number";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                if (string.IsNullOrEmpty(depart))
                {
                    lblErrorMessage.Text = "Please Enter a Department Name";
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    try
                    {
                        id = int.Parse(txtDeptNumber.Text);
                    }
                    catch (Exception ex)
                    {
                        lblErrorMessage.Text = "Please Enter a Department Number";
                        lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    int result = adpDepartment.Update(depart, id);

                    if (result == 1)
                    {
                        lblErrorMessage.Text = "Department Update ";
                        lblErrorMessage.ForeColor = System.Drawing.Color.Yellow;
                        txtDeptName.Text = "";
                        RefreshDepartment();
                    }
                    else
                    {
                        lblErrorMessage.Text = "Department NOT Update";
                        lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDeptNumber.Text))
            {
                lblErrorMessage.Text = "Please Insert the Department Number";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                int id = int.Parse(txtDeptNumber.Text);
                tblDepartment = adpDepartment.GetDataBy(id);
                Cache["tblDepartment"] = tblDepartment;

                var row = tblDepartment;
                if (row.Count > 0)
                {
                    if (row != null)
                    {
                        string name = row[0].DepartmentName.ToString();
                        txtDeptName.Text = name.Trim();
                    }
                }
                else
                {
                    lblErrorMessage.Text = " Department ID " + id + " is not found";
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}