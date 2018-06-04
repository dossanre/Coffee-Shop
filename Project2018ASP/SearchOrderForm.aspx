<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SearchOrderForm.aspx.cs" Inherits="Project2018ASP.SearchOrderForm" %>

<%@ MasterType VirtualPath="~/Main.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphEmployeeInformation">
    <asp:Label ID="lblDepthr" Class="lblEmployeeInfo" runat="server" EnableTheming="True" Text="Department Information"></asp:Label>
    <div class="form-group row">
        &nbsp
        <asp:Label ID="lblErrorMessage" Class="lblEmployeeInfo" runat="server" EnableTheming="True" Text="" Font-Bold="True" Font-Italic="True"></asp:Label>
    </div>
    <div class="form-group row">
        &nbsp
        <asp:TextBox ID="txtOrderNumber" CssClass="form-control col-sm-3" placeholder="Order Number" runat="server" TextMode="Number"></asp:TextBox>
        &nbsp
       <asp:Button ID="btnSearch" CssClass="btn btn-primary btn-block col-sm-2" runat="server" Text="Search" OnClick="btnSearch_Click" />
    </div>
    <div class="form-group row">
        &nbsp
        <asp:TextBox ID="txtDate" CssClass="form-control col-sm-3" placeholder="Date" runat="server" ReadOnly="True"></asp:TextBox>
    </div>
    <div class="form-group row">
        &nbsp
        <asp:TextBox ID="txtSubtotal" CssClass="form-control col-sm-3" placeholder="Subtotal" runat="server" ReadOnly="True"></asp:TextBox>
        &nbsp
        <asp:TextBox ID="txtTax" CssClass="form-control col-sm-3" placeholder="Tax" runat="server" ReadOnly="True"></asp:TextBox>
        &nbsp
        <asp:TextBox ID="txtTotal" CssClass="form-control col-sm-3" placeholder="Total" runat="server" ReadOnly="True"></asp:TextBox>
    </div>
    <div class="form-group row">
        &nbsp
        <asp:Button ID="btnSave" CssClass="btn btn-success btn-block col-sm-2" runat="server" Text="Save" />
    </div>
    <div >
        <asp:GridView ID="grdOrder" CssClass="col-sm-12 grids" runat="server" CellPadding="4" ForeColor="#333333" GridLines="Horizontal">
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
        </asp:GridView>
    </div>
</asp:Content>
