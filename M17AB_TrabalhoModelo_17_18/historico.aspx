<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="historico.aspx.cs" Inherits="M17AB_TrabalhoModelo_17_18.historico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Histórico</h2>
    <asp:GridView runat="server" ID="gvHistorico" CssClass="table table-responsive" />
</asp:Content>
