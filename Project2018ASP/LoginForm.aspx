<%@ Page Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="Project2018ASP.LoginForm" %>


<%@ MasterType VirtualPath="~/Login.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphLogin">

    <asp:Label ID="lblLogin" runat="server" CssClass="headLabel" Text="Log In"></asp:Label>
    <br />
    <asp:Label ID="lblMessageError" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="yellow"></asp:Label>
    <br />
    <br />
    <asp:TextBox ID="txtEmployeeID" placeholder="Employee Number" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
    <span>
        <br />
        <asp:TextBox ID="txtPassword" placeholder="Password" CssClass="form-control" runat="server" value="RIS@5642" TextMode="Password"></asp:TextBox>
    </span>
    <span>
        <asp:TextBox ID="txtNewPassword" placeholder="New Password" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <asp:TextBox ID="txtNewPasswordConfirm" placeholder="Confirm New Password" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>      
        <br />
    </span>
    <asp:Button ID="btnLogin" CssClass="btn btn-primary btn-block" runat="server" OnClick="btnLogin_Click" Text="Login" />
    <br />
</asp:Content>



