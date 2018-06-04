using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Project2018ASP.MyDataSetTableAdapters;

namespace Project2018ASP
{
    using TableDepartment = MyDataSet.DepartmentDataTable;
    using TableJobTitle = MyDataSet.JobTitleDataTable;
    using TableEmployee = MyDataSet.EmployeeDataTable;
    using TablePassword = MyDataSet.PasswordDataTable;

    public partial class EmployeeForm : System.Web.UI.Page
    {
        DepartmentTableAdapter adpDepartment = new DepartmentTableAdapter();
        TableDepartment tblDepartment = new TableDepartment();

        JobTitleTableAdapter adpJobTitle = new JobTitleTableAdapter();
        TableJobTitle tblJobTitle = new TableJobTitle();

        EmployeeTableAdapter adpEmployee = new EmployeeTableAdapter();
        TableEmployee tblEmployee = new TableEmployee();

        PasswordTableAdapter adpPassword = new PasswordTableAdapter();
        TablePassword tblPassword = new TablePassword();

        private void RefreshDepartment()
        {
            if (Session["DepartmentId"] == null || Session["EmployeeId"] == null)
            {
                Response.Redirect("~/LoginForm.aspx");
            }
            tblDepartment = adpDepartment.GetData();
            Cache["tblDepartment"] = tblDepartment;

            ddlDept.DataSource = tblDepartment;
            ddlDept.DataTextField = "DepartmentName";
            ddlDept.DataValueField = "DepartmentId";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, new ListItem("Select a Department", "-1"));

            ddlProvinceE.Items.Add("ALBERTA - AB");
            ddlProvinceE.Items.Add("BRITISH COLUMBIA -BC");
            ddlProvinceE.Items.Add("MANITOBA - MB");
            ddlProvinceE.Items.Add("NEW BRUNSWICK -NB");
            ddlProvinceE.Items.Add("NEWFOUNDLAND and LABRADOR - NL");
            ddlProvinceE.Items.Add("NORTHWEST TERRITORIES -NT");
            ddlProvinceE.Items.Add("NOVA SCOTIA -NS");
            ddlProvinceE.Items.Add("NUNAVUT - NU");
            ddlProvinceE.Items.Add("ONTARIO - ON");
            ddlProvinceE.Items.Add("PRINCE EDWARD ISLAND - PE");
            ddlProvinceE.Items.Add("QUEBEC - QC");
            ddlProvinceE.Items.Add("SASKATCHEWAN - SK");
            ddlProvinceE.Items.Add("YUKON - YT");
            ddlProvinceE.Items.Insert(0, new ListItem("Select a Province", "-1"));

            rdbGender.SelectedIndex = 0;
            rdbStatus.SelectedIndex = 0;

            int year = int.Parse(DateTime.Now.ToString("yyyy"));
            for (int i = year; i >= 1900; i--)
            {
                ddlDOBY.Items.Add(i.ToString());
            }
            for (int i = year + 1; i >= 1900; i--)
            {
                ddlStartdty.Items.Add(i.ToString());
            }

            ddlDOBM.Items.Add("January");
            ddlDOBM.Items.Add("February");
            ddlDOBM.Items.Add("March");
            ddlDOBM.Items.Add("April");
            ddlDOBM.Items.Add("May");
            ddlDOBM.Items.Add("June");
            ddlDOBM.Items.Add("July");
            ddlDOBM.Items.Add("August");
            ddlDOBM.Items.Add("September");
            ddlDOBM.Items.Add("October");
            ddlDOBM.Items.Add("November");
            ddlDOBM.Items.Add("December");
            ddlDOBM.Items.Insert(0, new ListItem("Month", "0"));

            ddlStartdtm.Items.Add("January");
            ddlStartdtm.Items.Add("February");
            ddlStartdtm.Items.Add("March");
            ddlStartdtm.Items.Add("April");
            ddlStartdtm.Items.Add("May");
            ddlStartdtm.Items.Add("June");
            ddlStartdtm.Items.Add("July");
            ddlStartdtm.Items.Add("August");
            ddlStartdtm.Items.Add("September");
            ddlStartdtm.Items.Add("October");
            ddlStartdtm.Items.Add("November");
            ddlStartdtm.Items.Add("December");
            ddlStartdtm.Items.Insert(0, new ListItem("Month", "0"));
            ddlStartdty.SelectedValue = year.ToString();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshDepartment();
            }

        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDept.SelectedIndex > 0)
            {
                loadJobTitle();

            }
        }

        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {
            clearFields();
            if (checkFields())
            {
                insertEmployee();
            }
        }

        private bool checkFields()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                lblErrorMessage.Text = "Please Enter the Employee Name";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                txtName.BackColor = System.Drawing.Color.Yellow;
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                lblErrorMessage.Text = "Please Enter the Employee Address";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                txtAddress.BackColor = System.Drawing.Color.Yellow;
                txtAddress.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtCity.Text))
            {
                lblErrorMessage.Text = "Please Enter the Employee City";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                txtCity.BackColor = System.Drawing.Color.Yellow;
                txtCity.Focus();
                return false;
            }
            if (ddlProvinceE.SelectedIndex <= 0)
            {
                lblErrorMessage.Text = "Please Select the Province";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                ddlProvinceE.BackColor = System.Drawing.Color.Yellow;
                ddlProvinceE.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtZipcode.Text))
            {
                lblErrorMessage.Text = "Please Enter the Employee City";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                txtZipcode.BackColor = System.Drawing.Color.Yellow;
                txtZipcode.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtHomePhone.Text))
            {
                lblErrorMessage.Text = "Please Enter the Employee Home Phone";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                txtHomePhone.BackColor = System.Drawing.Color.Yellow;
                txtHomePhone.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtCellPhone.Text))
            {
                lblErrorMessage.Text = "Please Enter the Employee Cell Phone";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                txtCellPhone.BackColor = System.Drawing.Color.Yellow;
                txtCellPhone.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                lblErrorMessage.Text = "Please Enter the Email";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                txtEmail.BackColor = System.Drawing.Color.Yellow;
                txtEmail.Focus();
                return false;
            }
            if (ddlDOBM.SelectedIndex <= 0)
            {
                lblErrorMessage.Text = "Please Select a Month";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                ddlDOBM.BackColor = System.Drawing.Color.Yellow;
                ddlDOBM.Focus();
                return false;
            }
            if (ddlDOBD.SelectedIndex < 0)
            {
                lblErrorMessage.Text = "Please Select a Day";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                ddlDOBD.BackColor = System.Drawing.Color.Yellow;
                ddlDOBD.Focus();
                return false;
            }
            if (ddlDept.SelectedIndex <= 0)
            {
                lblErrorMessage.Text = "Please Select the Department";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                ddlDept.BackColor = System.Drawing.Color.Yellow;
                ddlDept.Focus();
                return false;
            }
            if (ddlTitle.SelectedIndex <= 0)
            {
                lblErrorMessage.Text = "Please Select the Department";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                ddlTitle.BackColor = System.Drawing.Color.Yellow;
                ddlTitle.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtSalary.Text))
            {
                lblErrorMessage.Text = "Please Enter the Salary";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                txtSalary.BackColor = System.Drawing.Color.Yellow;
                txtSalary.Focus();
                return false;
            }
            try
            {
                decimal salary = decimal.Parse(txtSalary.Text);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "Only number are allowed 9999.99";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                txtSalary.BackColor = System.Drawing.Color.Yellow;
                txtSalary.Focus();
                return false;
            }
            if (ddlStartdtm.SelectedIndex <= 0)
            {
                lblErrorMessage.Text = "Please Select a Month";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                ddlStartdtm.BackColor = System.Drawing.Color.Yellow;
                ddlStartdtm.Focus();
                return false;
            }
            if (ddlStartdtd.SelectedIndex < 0)
            {
                lblErrorMessage.Text = "Please Select a Day";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                ddlStartdtd.BackColor = System.Drawing.Color.Yellow;
                ddlStartdtd.Focus();
                return false;
            }

            return true;
        }

        private void clearFields()
        {
            lblErrorMessage.Text = "";
            txtName.BackColor = System.Drawing.Color.White;
            txtAddress.BackColor = System.Drawing.Color.White;
            txtCity.BackColor = System.Drawing.Color.White;
            ddlProvinceE.BackColor = System.Drawing.Color.White;
            txtZipcode.BackColor = System.Drawing.Color.White;
            txtHomePhone.BackColor = System.Drawing.Color.White;
            txtCellPhone.BackColor = System.Drawing.Color.White;
            txtEmail.BackColor = System.Drawing.Color.White;

            ddlDept.BackColor = System.Drawing.Color.White;
            ddlTitle.BackColor = System.Drawing.Color.White;
            txtSalary.BackColor = System.Drawing.Color.White;

            ddlDOBY.BackColor = System.Drawing.Color.White;
            ddlDOBM.BackColor = System.Drawing.Color.White;
            ddlDOBD.BackColor = System.Drawing.Color.White;

            ddlStartdty.BackColor = System.Drawing.Color.White;
            ddlStartdtm.BackColor = System.Drawing.Color.White;
            ddlStartdtd.BackColor = System.Drawing.Color.White;

        }

        private void resetFields()
        {
            lblErrorMessage.Text = "";
            txtName.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            ddlProvinceE.SelectedIndex = -1;
            txtZipcode.Text = "";
            txtHomePhone.Text = "";
            txtCellPhone.Text = "";
            txtEmail.Text = "";
            // txtDOB.Text = "";
            ddlDept.SelectedIndex = -1;
            ddlTitle.SelectedIndex = -1;
            txtSalary.Text = "";
            ddlDOBY.SelectedIndex = 0;
            ddlDOBD.SelectedIndex = 0;
            ddlStartdty.SelectedIndex = 0;
            ddlStartdtm.SelectedIndex = 0;

        }

        private void loadJobTitle()
        {
            int deptId = int.Parse(ddlDept.SelectedValue);
            tblJobTitle = adpJobTitle.GetDataByDepartment(deptId);
            Cache["tblJobTitle"] = tblJobTitle;

            ddlTitle.DataSource = tblJobTitle;
            ddlTitle.DataTextField = "JobTitleName";
            ddlTitle.DataValueField = "JobTitleId";
            ddlTitle.DataBind();
            ddlTitle.Items.Insert(0, new ListItem("Select a Job Title", "-1"));

        }
        // insert register into employee table
        private void insertEmployee()
        {

            tblEmployee = adpEmployee.GetData();
            Cache["tblEmployee"] = tblEmployee;

            string name = txtName.Text;
            string address = txtAddress.Text;
            string city = txtCity.Text;
            string province = ddlProvinceE.SelectedValue;
            string zipcode = txtZipcode.Text;
            decimal homephone = decimal.Parse(txtHomePhone.Text);
            decimal cellphone = decimal.Parse(txtCellPhone.Text);
            string email = txtEmail.Text;
            //Treat DOB 
            string year = ddlDOBY.SelectedValue;
            string month = ddlDOBM.SelectedValue;
            string day = ddlDOBD.SelectedValue;
            string dobTemp = year + "-" + month + "-" + day;
            DateTime dob = DateTime.Parse(dobTemp);

            int dept = int.Parse(ddlDept.SelectedValue);
            int title = int.Parse(ddlTitle.SelectedValue);
            decimal salary = decimal.Parse(txtSalary.Text);
            //Treat startDate 
            string syear = ddlStartdty.SelectedValue;
            string smonth = ddlStartdtm.SelectedValue;
            string sday = ddlStartdtd.SelectedValue;
            string startTemp = syear + "-" + smonth + "-" + sday;
            DateTime startdate = DateTime.Parse(startTemp);
            string gender = "";
            if (rdbGender.SelectedIndex == 1)
            {
                gender = "F";
            }
            else if (rdbGender.SelectedIndex == 2)
            {
                gender = "O";
            }
            else
            {
                gender = "M";
            }

            int result = adpEmployee.Insert(name, address, city, province, zipcode, homephone, cellphone, email, dob, gender, dept, title, startdate, salary, "A");

            if (result == 1)
            {
                clearFields();
                resetFields();
                lblErrorMessage.Text = "Employee Inserted ";
                lblErrorMessage.ForeColor = System.Drawing.Color.Yellow;
                insertPassword();
            }
            else
            {
                lblErrorMessage.Text = "Employee NOT Inserted";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmployeeNumber.Text))
            {
                clearFields();
                lblErrorMessage.Text = "Please Insert the Employee Number";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                clearFields();
                loadFields();
            }
        }
        // Load fields From Employee Table
        private void loadFields()
        {
            int id = int.Parse(txtEmployeeNumber.Text);

            tblEmployee = adpEmployee.GetDataBy(id);
            Cache["tblEmployee"] = tblEmployee;

            var row = tblEmployee;

            if (row.Count > 0)
            {
                if (row != null)
                {
                    txtName.Text = row[0].EmployeeName.ToString();
                    txtAddress.Text = row[0].EmployeeAddress.ToString();
                    txtCity.Text = row[0].EmployeeCity.ToString();
                    txtZipcode.Text = row[0].EmployeeZipCode.ToString();
                    txtHomePhone.Text = row[0].EmployeeHomePhone.ToString();
                    txtCellPhone.Text = row[0].EmployeeCellPhone.ToString();
                    txtEmail.Text = row[0].EmployeeEmail.ToString();
                    txtSalary.Text = row[0].EmployeeSalary.ToString();
                    selectGender(row[0].EmployeeGender.ToString());
                    selectProvince(row[0].EmployeeProvince.ToString());
                    selectDepartment(row[0].EmployeeDepartmentID.ToString());
                    loadJobTitle();
                    selectJobTitle(row[0].EmployeeJobTitleId.ToString());
                    loadDOB(row[0].EmployeeDOB);
                    loadStartDate(row[0].EmployeeStartDate);
                    if (row[0].EmployeeStatus.ToString() == "A")
                    {
                        rdbStatus.SelectedIndex = 0;
                    }
                    else
                    {
                        rdbStatus.SelectedIndex = 1;
                    }
                }
                else
                {
                    clearFields();
                    lblErrorMessage.Text = "Employee with ID " + id + " is not found";
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                clearFields();
                lblErrorMessage.Text = " Employee with ID " + id + " is not found";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void selectProvince(string province)
        {
            string tmpProvince = province.Trim();

            switch (tmpProvince)
            {
                case "ALBERTA - AB":
                    ddlProvinceE.SelectedIndex = 1;
                    break;
                case "BRITISH COLUMBIA -BC":
                    ddlProvinceE.SelectedIndex = 2;
                    break;
                case "MANITOBA - MB":
                    ddlProvinceE.SelectedIndex = 3;
                    break;
                case "NEW BRUNSWICK -NB":
                    ddlProvinceE.SelectedIndex = 4;
                    break;
                case "NEWFOUNDLAND and LABRADOR - NL":
                    ddlProvinceE.SelectedIndex = 5;
                    break;
                case "NORTHWEST TERRITORIES -NT":
                    ddlProvinceE.SelectedIndex = 6;
                    break;
                case "NOVA SCOTIA -NS":
                    ddlProvinceE.SelectedIndex = 7;
                    break;
                case "NUNAVUT - NU":
                    ddlProvinceE.SelectedIndex = 8;
                    break;
                case "ONTARIO - ON":
                    ddlProvinceE.SelectedIndex = 9;
                    break;
                case "PRINCE EDWARD ISLAND - PE":
                    ddlProvinceE.SelectedIndex = 10;
                    break;
                case "QUEBEC - QC":
                    ddlProvinceE.SelectedIndex = 11;
                    break;
                case "SASKATCHEWAN - SK":
                    ddlProvinceE.SelectedIndex = 12;
                    break;
                case "YUKON - YT":
                    ddlProvinceE.SelectedIndex = 13;
                    break;
                default:
                    ddlProvinceE.SelectedIndex = -1;
                    break;
            }
        }

        private void selectDepartment(string deptId)
        {
            ddlDept.SelectedValue = deptId.ToString();
        }

        public void selectJobTitle(string jobTitle)
        {
            string tmp = jobTitle.Trim();
            ddlTitle.SelectedValue = tmp;

        }

        public void selectGender(string gender)
        {
            if (gender == "F")
            {
                rdbGender.SelectedIndex = 1;
            }
            else if (gender == "O")
            {
                rdbGender.SelectedIndex = 2;
            }
            else
            {
                rdbGender.SelectedIndex = 0;
            }

        }

        public void loadDOB(DateTime dob)
        {
            string dobTemp = dob.ToString();
            string[] date = dobTemp.Split('/');
            string[] yearTemp = date[2].Split(' ');
            ddlDOBY.SelectedValue = yearTemp[0];
            ddlDOBM.SelectedIndex = int.Parse(date[0]);
            int year = int.Parse(yearTemp[0]);
            int month = int.Parse(date[0]);
            ddlDOBD.Items.Clear();
            int day = CalculateDate(year, month);
            for (int i = 1; i <= day; i++)
            {
                ddlDOBD.Items.Add(i.ToString());
            }
            ddlDOBD.SelectedValue = date[1];
        }

        public void loadStartDate(DateTime startDate)
        {
            string startDateTemp = startDate.ToString();
            string[] date = startDateTemp.Split('/');
            string[] yearTemp = date[2].Split(' ');
            ddlStartdty.SelectedValue = yearTemp[0];
            ddlStartdtm.SelectedIndex = int.Parse(date[0]);
            int year = int.Parse(yearTemp[0]);
            int month = int.Parse(date[0]);
            ddlStartdtd.Items.Clear();
            int day = CalculateDate(year, month);
            for (int i = 1; i <= day; i++)
            {
                ddlStartdtd.Items.Add(i.ToString());
            }
            ddlStartdtd.SelectedValue = date[1];
        }

        protected void ddlDOBM_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = int.Parse(ddlDOBY.SelectedValue);
            int month = ddlDOBM.SelectedIndex;
            int day = CalculateDate(year, month);
            ddlDOBD.Items.Clear();
            for (int i = 1; i <= day; i++)
            {
                ddlDOBD.Items.Add(i.ToString());
            }
        }

        protected void ddlStartdtm_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = int.Parse(ddlStartdty.SelectedValue);
            int month = ddlStartdtm.SelectedIndex;
            int day = CalculateDate(year, month);
            ddlStartdtd.Items.Clear();
            for (int i = 1; i <= day; i++)
            {
                ddlStartdtd.Items.Add(i.ToString());
            }
        }

        protected void btnUpdEmployee_Click(object sender, EventArgs e)
        {
            clearFields();
            if (checkFields())
            {
                updateEmployee();
            }
        }
        // insert Update into employee table
        protected void updateEmployee()
        {
            int id = int.Parse(txtEmployeeNumber.Text);
            string name = txtName.Text;
            string address = txtAddress.Text;
            string city = txtCity.Text;
            string province = ddlProvinceE.SelectedValue;
            string zipcode = txtZipcode.Text;
            decimal homephone = decimal.Parse(txtHomePhone.Text);
            decimal cellphone = decimal.Parse(txtCellPhone.Text);
            string email = txtEmail.Text;
            //Treat DOB 
            string year = ddlDOBY.SelectedValue;
            string month = ddlDOBM.SelectedValue;
            string day = ddlDOBD.SelectedValue;
            string dobTemp = year + "-" + month + "-" + day;
            DateTime dob = DateTime.Parse(dobTemp);

            int dept = int.Parse(ddlDept.SelectedValue);
            int title = int.Parse(ddlTitle.SelectedValue);
            decimal salary = decimal.Parse(txtSalary.Text);
            //Treat startDate 
            //Treat startDate 
            string syear = ddlStartdty.SelectedValue;
            string smonth = ddlStartdtm.SelectedValue;
            string sday = ddlStartdtd.SelectedValue;
            string startTemp = syear + "-" + smonth + "-" + sday;
            DateTime startdate = DateTime.Parse(startTemp);

            string gender = "";
            if (rdbGender.SelectedIndex == 1)
            {
                gender = "F";
            }
            else if (rdbGender.SelectedIndex == 2)
            {
                gender = "O";
            }
            else
            {
                gender = "M";
            }
            string status = "A";
            if (rdbStatus.SelectedIndex != 0)
            {
                status = "F";
            }

            int result = adpEmployee.Update(name, address, city, province, zipcode, homephone, cellphone, email, dob, gender, dept, title, startdate, salary, status, id);

            if (result == 1)
            {
                lblErrorMessage.Text = "Employee Update";
                lblErrorMessage.ForeColor = System.Drawing.Color.Green;
 
            }
            else
            {
                lblErrorMessage.Text = "Employee NOT Update";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }

        }

        public void insertPassword()
        {
            int result = adpPassword.Insert("Ris@5642","Y");

            if (result != 1)
            {
                lblErrorMessage.Text = "Employee NOT Update";
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        public int CalculateDate(int year, int month)
        {
            int day = 0;
            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                day = 31;
            }
            else if (month == 4 || month == 6 || month == 9 || month == 11)
            {
                day = 30;
            }
            else
            {
                if ((year % 400 == 0) || (year % 100 == 0) || (year % 4 == 0))
                {
                    day = 29;
                }
                else
                {
                    day = 28;
                }

            }

            return day;

        }
    }
}