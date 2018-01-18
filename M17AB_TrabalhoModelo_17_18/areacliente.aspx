<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="areacliente.aspx.cs" Inherits="M17AB_TrabalhoModelo_17_18.areacliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1>Área do cliente</h1>
    <div class="btn-group">
        <asp:Button OnClick="btEmprestimo_Click" runat="server" ID="btEmprestimo" Text="Empréstimo" CssClass="btn btn-info" />
        <asp:Button OnClick="btDevolve_Click" runat="server" ID="btDevolve" Text="Devolver" CssClass="btn btn-info"/>
        <asp:Button OnClick="btHistorico_Click" runat="server" ID="btHistorico" Text="Histórico" CssClass="btn btn-info"/>
    </div>
    <div id="divEmprestimo" runat="server">
        <h2>Livros a emprestar</h2>
        <asp:GridView runat="server" ID="gvEmprestar" CssClass="table table-responsive" />
    </div>
    <div id="divDevolver" runat="server">
        <h2>Livros emprestados</h2>
        <asp:GridView runat="server" ID="gvDevolver" CssClass="table table-responsive" />
    </div>
    <div id="divHistorico" runat="server">
        <h2>Histórico</h2>
        <asp:GridView runat="server" ID="gvHistorico" CssClass="table table-responsive" />
    </div>
</asp:Content>
