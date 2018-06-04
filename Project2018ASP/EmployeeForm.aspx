<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EmployeeForm.aspx.cs" Inherits="Project2018ASP.EmployeeForm" %>

<%@ MasterType VirtualPath="~/Main.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphEmployeeInformation">

    <asp:Label ID="lblEmployeeInfohr" Class="lblEmployeeInfo" runat="server" EnableTheming="True" Text="Employee Information" Font-Bold="True" Font-Italic="True"></asp:Label>
    <br />
    <div class="form-group row">
        &nbsp
         <asp:Label ID="lblErrorMessage" Class="lblEmployeeInfo" runat="server" EnableTheming="True" Text="" Font-Bold="True" Font-Italic="True"></asp:Label>
    </div>
    <div class="form-group row">
        &nbsp
        <asp:TextBox ID="txtEmployeeNumber" CssClass="form-control col-sm-3" placeholder="Employee Number" runat="server" TextMode="Number"></asp:TextBox>
        &nbsp
        <asp:Button ID="btnSearch" CssClass="btn btn-primary btn-block col-sm-2" runat="server" Text="Search" OnClick="btnSearch_Click" />
    </div>
    <div class="form-group row">
        &nbsp
        <asp:TextBox ID="txtName" CssClass="form-control col-sm-9" placeholder="First Name" runat="server"></asp:TextBox>
    </div>
    <div class="form-group row">
        &nbsp
        <asp:TextBox ID="txtAddress" CssClass="form-control input-lg col-sm-9" placeholder="Address" runat="server"></asp:TextBox>
    </div>
    <div class="form-group row">
        &nbsp
        <asp:TextBox ID="txtCity" CssClass="form-control input-lg col-sm-3" placeholder="City" runat="server"></asp:TextBox>
        &nbsp
        <asp:DropDownList ID="ddlProvinceE" CssClass="form-control input-lg col-sm-3" runat="server">
        </asp:DropDownList>
        &nbsp
        <asp:TextBox ID="txtZipcode" CssClass="form-control input-lg col-sm-3" placeholder="Zip Code" runat="server" MaxLength="6"></asp:TextBox>
    </div>
    <div class="form-group row">
        &nbsp
        <asp:TextBox ID="txtHomePhone" CssClass="form-control input-lg col-sm-3" placeholder="Home Phone" runat="server" TextMode="Number" MaxLength="10"></asp:TextBox>
        &nbsp
        <asp:TextBox ID="txtCellPhone" CssClass="form-control input-lg col-sm-3" placeholder="Mobile Phone" runat="server" TextMode="Number" MaxLength="10"></asp:TextBox>
    </div>
    <div class="form-group row">
        &nbsp
        <asp:TextBox ID="txtEmail" CssClass="form-control input-lg col-sm-6" runat="server" placeholder="Email" TextMode="Email"></asp:TextBox>
    </div>
    <div class="form-group row">
        &nbsp
    <div class="left-align-text col-sm-3">
        <asp:Label ID="lblDOB" runat="server" EnableTheming="True" Text="Date Of Birth :" ForeColor="White" Font-Bold="True" Font-Italic="True" Font-Size="Larger" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></asp:Label>
    </div>
        &nbsp
        <asp:DropDownList ID="ddlDOBY" CssClass="form-control input-lg col-sm-2" runat="server">
        </asp:DropDownList>&nbsp
        <asp:DropDownList ID="ddlDOBM" CssClass="form-control input-lg col-sm-2" runat="server" OnSelectedIndexChanged="ddlDOBM_SelectedIndexChanged" AutoPostBack="True">
        </asp:DropDownList>&nbsp
        <asp:DropDownList ID="ddlDOBD" CssClass="form-control input-lg col-sm-1" runat="server">
        </asp:DropDownList>
    </div>
    <div class="form-group row">
        &nbsp
        <div class="left-align-text col-sm-3">
            <asp:Label ID="lblGender" runat="server" EnableTheming="True" Text="Gender :" ForeColor="White" Font-Bold="True" Font-Italic="True" Font-Size="Larger"></asp:Label>
        </div>
        &nbsp
        <asp:RadioButtonList ID="rdbGender" runat="server" RepeatDirection="Horizontal" CellPadding="5" CellSpacing="5" Font-Bold="False" Font-Italic="False" ForeColor="White" Font-Size="Larger">
            <asp:ListItem>Male</asp:ListItem>
            <asp:ListItem>Female</asp:ListItem>
            <asp:ListItem>Other</asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <!-- Department form -->
    <asp:Label ID="lblDepthr" Class="lblEmployeeInfo" runat="server" EnableTheming="True" Text="Department Information"></asp:Label>
    <br />
    <div class="form-group row">
        &nbsp
        <asp:DropDownList ID="ddlDept" CssClass="form-control input-lg col-sm-3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
        </asp:DropDownList>
        &nbsp
        <asp:DropDownList ID="ddlTitle" CssClass="form-control input-lg col-sm-3" runat="server">
        </asp:DropDownList>
        &nbsp
        <asp:TextBox ID="txtSalary" CssClass="form-control input-lg col-sm-3" runat="server" placeholder="Salary"></asp:TextBox>
    </div>
    <div class="form-group row">
        &nbsp
        <div class="left-align-text col-sm-2">
            <asp:Label ID="lblStartDate" runat="server" EnableTheming="True" Text="Start Date :" ForeColor="White" Font-Bold="True" Font-Italic="True" Font-Size="Larger"></asp:Label>
        </div>
        &nbsp
        <asp:DropDownList ID="ddlStartdty" CssClass="form-control input-lg col-sm-1" runat="server">
        </asp:DropDownList>&nbsp
        <asp:DropDownList ID="ddlStartdtm" CssClass="form-control input-lg col-sm-2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStartdtm_SelectedIndexChanged">
        </asp:DropDownList>&nbsp
        <asp:DropDownList ID="ddlStartdtd" CssClass="form-control input-lg col-sm-1" runat="server">
        </asp:DropDownList>
    </div>
    <div class="form-group row">
        &nbsp
        <div class="left-align-text col-sm-2">
            <asp:Label ID="lblStatus" runat="server" EnableTheming="True" Text="Status:" ForeColor="White" Font-Bold="True" Font-Italic="True" Font-Size="Larger"></asp:Label>
        </div>
        &nbsp
        <asp:RadioButtonList ID="rdbStatus" runat="server" RepeatDirection="Horizontal" CellPadding="5" CellSpacing="5" Font-Bold="False" Font-Italic="False" ForeColor="White" Font-Size="Larger">
            <asp:ListItem>Active</asp:ListItem>
            <asp:ListItem>Inactive</asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <div class="form-group row">
        &nbsp;<asp:Button ID="btnAddEmployee" CssClass="btn btn-success btn-block col-sm-3" runat="server" Text="Add Employee" OnClick="btnAddEmployee_Click" />
        &nbsp
        <asp:Button ID="btnUpdEmployee" CssClass="btn btn-success btn-block col-sm-3" runat="server" Text="Update Employee" OnClick="btnUpdEmployee_Click" />
    </div>
</asp:Content>
