using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Project2018ASP.MyDataSetTableAdapters;

namespace Project2018ASP
{
    using System.Text.RegularExpressions;
    using TablePassword = MyDataSet.PasswordDataTable;
    using TableEmployee = MyDataSet.EmployeeDataTable;
    using TableDepartment = MyDataSet.DepartmentDataTable;

    public partial class LoginForm : System.Web.UI.Page
    {
        PasswordTableAdapter adpPassword = new PasswordTableAdapter();
        TablePassword tblPassword = new TablePassword();

        EmployeeTableAdapter adpEmployee = new EmployeeTableAdapter();
        TableEmployee tblEmployee = new TableEmployee();

        DepartmentTableAdapter adpDepartment = new DepartmentTableAdapter();
        TableDepartment tblDepartment = new TableDepartment();

        public void refresh()
        {
            txtNewPassword.Visible = false;
            txtNewPasswordConfirm.Visible = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                refresh();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (btnLogin.Text == "Login")
            {
                loginButton();
            }
            else
            {
                updatePassword();
            }
        }

        public void loginButton()
        {
            if (validUserandPassword())
            {
                int id = int.Parse(txtEmployeeID.Text);
                string pass = txtPassword.Text;

                tblPassword = adpPassword.GetDataBy(id, pass);
               
                var row = tblPassword;
                if (row.Count > 0)
                {
                    if (row != null)
                    {
                        if (row[0].PasswordExpire.ToString() == "Y")
                        {
                            threatNewPassword(row[0].Password.ToString());
                        }
                        else
                        {
                            createSession();
                        }
                    }
                }
                else
                {
                    lblMessageError.Text = " User or Password Invalid";
                    lblMessageError.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        public void threatNewPassword(string pass)
        {
            txtNewPassword.Visible = true;
            txtNewPasswordConfirm.Visible = true;
            txtPassword.Visible = false;
            btnLogin.Text = "Update Password";
        }

        public void updatePassword()
        {
            if (validNewPassword())
            {
                string newpass = txtNewPassword.Text;
                int id = int.Parse(txtEmployeeID.Text);
                int result = adpPassword.Update(newpass, "N", id);

                if (result == 1)
                {
                    createSession();
                }
                else
                {
                    lblMessageError.Text = "Employee NOT Updated";
                    lblMessageError.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        public bool validUserandPassword()
        {
            if (string.IsNullOrEmpty(txtEmployeeID.Text))
            {
                lblMessageError.Text = "Please Insert the Employee Number";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                return false;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                lblMessageError.Text = "Please Insert the Password";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                return false;
            }
            return true;
        }

        public bool validNewPassword()
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text))
            {
                lblMessageError.Text = "Please Insert the New Password";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                return false;
            }
            if (string.IsNullOrEmpty(txtNewPasswordConfirm.Text))
            {
                lblMessageError.Text = "Please Insert the Confirm password";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                return false;
            }
            if (txtNewPassword.Text != txtNewPasswordConfirm.Text)
            {
                lblMessageError.Text = "The New Password NOT Match";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                return false;
            }
            if (!checkCharacters()) {
                return false;
            }
            return true;
        }

        private bool checkCharacters()
        {
            var input = txtNewPassword.Text;

            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasNumber = new Regex(@"[0-9]+");

            if (!hasLowerChar.IsMatch(input))
            {
                lblMessageError.Text = "Password should contain At least one lower case letter";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                lblMessageError.Text = "Password should contain At least one upper case letter";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                lblMessageError.Text = "Password should not be less than or greater than 12 characters";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                lblMessageError.Text = "Password should contain At least one numeric value";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                return false;
            }
            else if (!hasSymbols.IsMatch(input))
            {
                lblMessageError.Text = "Password should contain At least one special case characters";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                return false;
            }
            else
            {
                return true;
            }
        }

        public void createSession()
        {
            int id = int.Parse(txtEmployeeID.Text);
            tblEmployee = adpEmployee.GetDataBy(id);

            var row = tblEmployee;

            if (row.Count > 0)
            {
                if (row != null)
                {
                    Session["DepartmentId"] = row[0].EmployeeDepartmentID;
                    Session["EmployeeId"] = txtEmployeeID.Text;
                    Response.Redirect("~/WelcomeForm.aspx");
                }
            }
            else
            {
                lblMessageError.Text = " Error to create Session";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
            }
        }

    }
}