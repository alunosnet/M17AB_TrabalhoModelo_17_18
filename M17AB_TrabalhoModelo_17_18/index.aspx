<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="M17AB_TrabalhoModelo_17_18.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divLogin" runat="server">
        Email:<asp:TextBox runat="server" ID="tbEmail"></asp:TextBox>
        Password:<asp:TextBox runat="server" ID="tbPassword" TextMode="Password"></asp:TextBox>
        <asp:Button ID="btLogin" runat="server" Text="Login" OnClick="btLogin_Click" />
        <asp:Label ID="lbErro" runat="server"></asp:Label>
    </div>
</asp:Content>
