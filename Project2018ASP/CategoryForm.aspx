<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CategoryForm.aspx.cs" Inherits="Project2018ASP.CategoryForm" %>


<%@ MasterType VirtualPath="~/Main.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphEmployeeInformation">
    <!-- Department form -->
    <asp:Label ID="lblCategory" Class="lblEmployeeInfo" runat="server" EnableTheming="True" Text="Category Information"></asp:Label>
    <div class="form-group row">
        &nbsp
        <asp:Label ID="lblErrorMessage" Class="lblEmployeeInfo" runat="server" EnableTheming="True" Text="" Font-Bold="True" Font-Italic="True"></asp:Label>
    </div>
    <div class="form-group row">
        &nbsp
        <asp:TextBox ID="txtCategNumber" CssClass="form-control col-sm-3" placeholder="Category Number" runat="server" TextMode="Number"></asp:TextBox>
        &nbsp
       <asp:Button ID="btnSearchc" CssClass="btn btn-primary btn-block col-sm-2" runat="server" Text="Search" OnClick="btnSearchc_Click" />
    </div>
    <div class="form-group row">
        &nbsp
        <asp:TextBox ID="txtCategName" CssClass="form-control col-sm-3" placeholder="Category Name" runat="server"></asp:TextBox>
    </div>
    <div class="form-group row">
        &nbsp
        <asp:Button ID="btnSave" CssClass="btn btn-success btn-block col-sm-2" runat="server" Text="Save" OnClick="btnSave_Click"  />
        &nbsp
        <asp:Button ID="Update" CssClass="btn btn-success btn-block col-sm-2" runat="server" Text="Update" OnClick="Update_Click" />

    </div>
    <br />
    <div style="height: 300px; overflow: scroll" >
    <asp:GridView ID="grdCategory" CssClass="col-sm-12 grids" runat="server" CellPadding="4" ForeColor="#333333" GridLines="Horizontal">
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
