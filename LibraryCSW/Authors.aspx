<%@ Page Title="Authors" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Authors.aspx.cs" Async="true" Inherits="About" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="content/Site.css" rel="stylesheet" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h2><%: Title %></h2>
            <br />

            <asp:Button ID="BtnNew" runat="server" Text="Add Author" CausesValidation="false" OnClick="BtnNew_Click" />

            <br />
            <br />
            <div>
                <asp:GridView ID="gvAuthors" runat="server" PageSize="15" AllowPaging="true" OnRowCommand="gvAuthors_RowCommand" OnPageIndexChanging="gvAuthors_PageIndexChanging" GridLines="Horizontal" AutoGenerateColumns="false" BorderColor="#DDDDDD">
                    <Columns>
                        <asp:BoundField DataField="ID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide" />
                        <asp:BoundField DataField="Name" ItemStyle-Width="200" HeaderText="FIRST NAME" />
                        <asp:BoundField DataField="LastName" ItemStyle-Width="200" HeaderText="LAST NAME" />
                        <asp:ButtonField ButtonType="Image" ItemStyle-Width="50" />
                        <asp:ButtonField ButtonType="Image" CommandName="editar" ImageUrl="~/images/editar.png" />
                        <asp:ButtonField ButtonType="Image" CommandName="eliminar" ItemStyle-Width="40" ItemStyle-HorizontalAlign="center" ImageUrl="~/images/d4.png" />
                    </Columns>

                </asp:GridView>
            </div>
            <div style="clear: both"></div>
            <br />
            <br />

            <asp:Panel ID="PanelAuthor" Style="padding-top: 2px; padding-left: 2px; display: none; font-family: 'Arial'; background-color: rgba(256, 256, 256, 1); color: Black; width: 700px;" runat="server">

                <div style="width: 99%; text-align: right; height: 20px; background-color: gray">

                    <asp:Label ID="lblAuthor" ForeColor="White" Text="Enter the author information" runat="server"></asp:Label>
                </div>
                <br />
                <br />
                <br />
                <table border="0">
                    <tr style="height: 50px;">
                        <td style="width: 150px;">
                            <b>First Name: </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAuthorName" MaxLength="50" Width="200" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtAuthorName" ValidationGroup="author" runat="server" ControlToValidate="txtAuthorName">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 50px;">
                        <td>
                            <b>Last Name: </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAuthorLastName" MaxLength="50" Width="200" runat="server"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="rfvtxtAuthorLastName" ValidationGroup="author" runat="server" ControlToValidate="txtAuthorLastName">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 50px;">
                        <td>
                            <b>Country: </b>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlCountry"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlCountry" InitialValue="0" ValidationGroup="author" runat="server" ControlToValidate="ddlCountry">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>

                <br />
                <br />
                <asp:Label ID="lblIDAuthor" Visible="false" runat="server"></asp:Label>
                <asp:Label ID="lblError" ForeColor="Red" runat="server"></asp:Label>
                <br />
                <br />
                <div style="text-align: center; width: 700px">
                    <asp:Button ID="btnCerrarModal" CausesValidation="false" runat="server" Width="120" Text="Cancel" />&nbsp;&nbsp;
                        <asp:Button ID="btnAddAuthor" CausesValidation="true" ValidationGroup="author" Width="120" runat="server" OnClick="btnAddAuthor_Click" Text="Add" />&nbsp;&nbsp;
                </div>
                <br />
            </asp:Panel>
            <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lblAuthor"
                PopupControlID="PanelAuthor" BackgroundCssClass="modalBackground">
            </ajax:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
