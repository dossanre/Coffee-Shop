<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="OrderForm.aspx.cs" Inherits="Project2018ASP.OrderForm" %>

<%@ MasterType VirtualPath="~/Main.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphEmployeeInformation">
    <asp:Label ID="lblOrder" Class="lblEmployeeInfo" runat="server" EnableTheming="True" Text="Order here" Font-Bold="True" Font-Italic="True"></asp:Label>
    <br />
    <asp:Label ID="lblMessage" Class="lblEmployeeInfo" runat="server" EnableTheming="True" Text="" Font-Bold="True" Font-Italic="True"></asp:Label>
    <br />
    <div class="form-group row">
        &nbsp
        <asp:DropDownList ID="ddlCategoryProd" CssClass="form-control input-lg col-sm-3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategoryProd_SelectedIndexChanged">
        </asp:DropDownList>
        &nbsp
        <asp:DropDownList ID="ddlProductName" CssClass="form-control input-lg col-sm-3" runat="server">
        </asp:DropDownList>
        &nbsp
        <asp:Button ID="btnDecreaseItem" CssClass="btn btn-success btn-block col-sm-1" runat="server" Text=" - " OnClick="btnDecreaseItem_Click" />
        &nbsp
        <asp:TextBox ID="txtQuantity" CssClass="form-control input-lg col-sm-1" runat="server" value="1" ReadOnly="True" TextMode="Number"></asp:TextBox>
        &nbsp
        <asp:Button ID="btnIncreaseItem" CssClass="btn btn-success btn-block col-sm-1" runat="server" Text=" + " OnClick="btnIncreaseItem_Click" />
        &nbsp
        <asp:Button ID="btnAddItem" CssClass="btn btn-success btn-block col-sm-2" runat="server" Text="Add Item " OnClick="btnAddItem_Click" />
    </div>
    <div class="form-group row">
        &nbsp
        <div style="height: 600px; width: 80%;">
            <asp:Panel ID="panelGrid" CssClass="col-sm-9" runat="server">
                <asp:GridView ID="grdOrder" runat="server" CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnRowDeleting="rowdeleteGridview">
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
                    <Columns>
                        <asp:ButtonField ButtonType="Button" CommandName="Delete" ShowHeader="True" Text="Remove" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </div>
        <asp:Panel ID="panelTotal" CssClass="col-sm-2 lblEmployeeInfo" runat="server">
            <asp:Panel ID="Panel2" runat="server" Height="98px">
                <asp:Label ID="lblSubtotal" runat="server" Text="Subtotal:"></asp:Label>
                <asp:TextBox ID="txtSubtotal" CssClass="form-control input-lg " runat="server"></asp:TextBox>
                <asp:Label ID="lblTax" runat="server" Text="Tax:"></asp:Label>
                <asp:TextBox ID="txtTax" CssClass="form-control input-lg " runat="server"></asp:TextBox>
                <asp:Label ID="lblTotal" runat="server" Text="Total:"></asp:Label>
                <asp:TextBox ID="txtTotal" CssClass="form-control input-lg " runat="server"></asp:TextBox><br />
                <asp:Button ID="btnFinalizeOrder" CssClass="btn btn-warning btn-block" runat="server" Text="Finish Order" OnClick="btnFinalizeOrder_Click" />
            </asp:Panel>
        </asp:Panel>
    </div>

</asp:Content>
