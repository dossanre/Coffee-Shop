<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ProductForm.aspx.cs" Inherits="Project2018ASP.ProductForm" %>

<%@ MasterType VirtualPath="~/Main.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphEmployeeInformation">
    <!-- Department form -->
    <asp:Label ID="lblDepthr" Class="lblEmployeeInfo" runat="server" EnableTheming="True" Text="Product Information"></asp:Label>
    <div class="form-group row">
        &nbsp
        <asp:Label ID="lblErrorMessage" Class="lblEmployeeInfo" runat="server" EnableTheming="True" Text="" Font-Bold="True" Font-Italic="True"></asp:Label>
    </div>
    <div class="form-group row">
        &nbsp
        <asp:TextBox ID="txtProductNumber" CssClass="form-control col-sm-3" placeholder="Product Number" runat="server" TextMode="Number"></asp:TextBox>
        &nbsp
       <asp:Button ID="btnSearch" CssClass="btn btn-primary btn-block col-sm-2" runat="server" Text="Search" OnClick="btnSearch_Click" />
    </div>
    <div class="form-group row">
        &nbsp
        <asp:DropDownList ID="ddlCategory" CssClass="form-control input-lg col-sm-3" runat="server">
        </asp:DropDownList>
        &nbsp
        <asp:TextBox ID="txtProductName" CssClass="form-control col-sm-3" placeholder="Product Name" runat="server"></asp:TextBox>
    </div>
    <div class="form-group row">
        &nbsp
        <asp:TextBox ID="txtPrice" CssClass="form-control col-sm-3" placeholder="Price" runat="server"></asp:TextBox>
        &nbsp
        <asp:TextBox ID="txtQuantity" CssClass="form-control col-sm-3" placeholder="Quantity" runat="server" TextMode="Number"></asp:TextBox>
    </div>

    <div class="form-group row">
        &nbsp
        <asp:Button ID="btnSave" CssClass="btn btn-success btn-block col-sm-2" runat="server" Text="Save" OnClick="btnSave_Click" />
        &nbsp
        <asp:Button ID="Update" CssClass="btn btn-success btn-block col-sm-2" runat="server" Text="Update" OnClick="Update_Click" />
    </div>
    <div style="height: 300px; overflow: scroll" >
        <asp:GridView ID="grdCategProduct" CssClass="col-sm-12 grids" runat="server" CellPadding="4" ForeColor="#333333" GridLines="Horizontal">
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


