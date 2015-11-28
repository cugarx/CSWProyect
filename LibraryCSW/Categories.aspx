<%@ Page Title="Categories" Language="C#" Async="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Categories.aspx.cs" Inherits="Contact" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <h2><%: Title %></h2>
            <div style="float: left; width: 500px;">
                Category name: &nbsp;
    <asp:TextBox ID="txtCategory" Width="250" MaxLength="50" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="rfvtxtCategory" ControlToValidate="txtCategory" runat="server" ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:Label ID="lblIDHidden" runat="server" Visible="false"></asp:Label>
                <asp:Button ID="btnUpdate" runat="server" Text="Add" OnClick="btnUpdate_Click" />

            </div>
            <div style="float: left; margin-left: 50px; width: 400px;">
                <asp:GridView ID="gvCategories" runat="server" AllowPaging="true" PageSize="15" OnRowCommand="gvCategories_RowCommand" OnPageIndexChanging="gvCategories_PageIndexChanging" GridLines="Horizontal" AutoGenerateColumns="false" BorderColor="#DDDDDD">
                    <Columns>
                        <asp:BoundField DataField="ID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide" />
                        <asp:BoundField DataField="Name" ItemStyle-Width="300" HeaderText="CATEGORY NAME" />
                        <asp:ButtonField ButtonType="Image" CommandName="editar" ImageUrl="~/images/editar.png" />
                        <asp:ButtonField ButtonType="Image" CommandName="eliminar" ItemStyle-Width="40" ItemStyle-HorizontalAlign="center" ImageUrl="~/images/d4.png" />
                    </Columns>

                </asp:GridView>
            </div>
            <div style="clear: both"></div>
            <br />
            <br />
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
