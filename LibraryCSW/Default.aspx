<%@ Page Title="Library Application" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="content/Site.css" rel="stylesheet" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="min-height: 500px;">
                <br />
                <br />
                <asp:Button ID="btnAddBook" runat="server" Text="New Book" OnClick="btnAddBook_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;    Filter by:
                <asp:DropDownList ID="ddlAuthorsActives" AutoPostBack="true" OnSelectedIndexChanged="ddlAuthorsActives_SelectedIndexChanged" runat="server"></asp:DropDownList>
                <br />
                <br />
                <asp:GridView ID="gvBooks" runat="server" OnRowCommand="gvBooks_RowCommand" GridLines="Horizontal" AutoGenerateColumns="false" BorderColor="#DDDDDD">
                    <Columns>
                        <asp:BoundField DataField="IdBook" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide" />
                        <asp:BoundField DataField="Title" ItemStyle-Width="200" HeaderText="TITLE" />
                        <asp:BoundField DataField="ISBN" ItemStyle-Width="150" HeaderText="ISBN" />
                        <asp:BoundField DataField="Author" ItemStyle-Width="200" HeaderText="AUTHOR" />
                        <asp:BoundField DataField="Publisher" ItemStyle-Width="200" HeaderText="PUBLISHER" />
                        <asp:BoundField DataField="Category" ItemStyle-Width="150" HeaderText="CATEGORY" />
                        <asp:ButtonField ButtonType="Image" ItemStyle-Width="50" />
                        <asp:ButtonField ButtonType="Image" CommandName="eliminar" ItemStyle-Width="40" ItemStyle-HorizontalAlign="center" ImageUrl="~/images/d4.png" />
                    </Columns>

                </asp:GridView>

                <asp:Panel ID="PanelBook" Style="padding-top: 2px; padding-left: 2px; display: none; font-family: 'Arial'; background-color: rgba(256, 256, 256, 1); color: Black; width: 700px;" runat="server">

                    <div style="width: 99%; text-align: right; height: 20px; background-color: gray">

                        <asp:Label ID="lblBook" ForeColor="White" Text="Enter the book information" runat="server"></asp:Label>
                    </div>
                    <br />
                    <br />
                    <br />
                    <table border="0">
                        <tr style="height: 50px;">
                            <td style="width: 150px;">
                                <b>Title: </b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTitle" MaxLength="50" Width="200" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtTitle" ValidationGroup="book" runat="server" ControlToValidate="txtTitle">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr style="height: 50px;">
                            <td>
                                <b>ISBN: </b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtISBN" MaxLength="50" Width="200" runat="server"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="rfvISBN" ValidationGroup="book" runat="server" ControlToValidate="txtISBN">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr style="height: 50px;">
                            <td>
                                <b>Publisher: </b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPublisher" MaxLength="50" Width="200" runat="server"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="rfvtxtPublisher" ValidationGroup="book" runat="server" ControlToValidate="txtPublisher">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr style="height: 50px;">
                            <td>
                                <b>Author: </b>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlAuthor"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlAuthor" InitialValue="0" ValidationGroup="book" runat="server" ControlToValidate="ddlAuthor">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr style="height: 50px;">
                            <td>
                                <b>Category: </b>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlCategory"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlCategory" InitialValue="0" ValidationGroup="book" runat="server" ControlToValidate="ddlCategory">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>

                    <br />
                    <br />
                    <asp:Label ID="lblError" ForeColor="Red" runat="server"></asp:Label>
                    <br />
                    <br />
                    <div style="text-align: center; width: 700px">
                        <asp:Button ID="btnCerrarModal" CausesValidation="false" runat="server" Width="120" Text="Cancel" />&nbsp;&nbsp;
                        <asp:Button ID="btnAddBookAcept" CausesValidation="true" ValidationGroup="book" Width="120" runat="server" OnClick="btnAddBookAcept_Click" Text="Add" />&nbsp;&nbsp;
                    </div>
                    <br />
                </asp:Panel>
                <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lblBook"
                    PopupControlID="PanelBook" BackgroundCssClass="modalBackground">
                </ajax:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
