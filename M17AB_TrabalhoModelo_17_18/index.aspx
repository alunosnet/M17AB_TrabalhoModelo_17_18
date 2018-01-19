<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="M17AB_TrabalhoModelo_17_18.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divLogin" runat="server" class="pull-right col-md-3 table-bordered text-center">
        Email:<asp:TextBox runat="server" ID="tbEmail"></asp:TextBox>
        Password:<asp:TextBox runat="server" ID="tbPassword" TextMode="Password"></asp:TextBox>
        <asp:Button ID="btLogin" runat="server" Text="Login" OnClick="btLogin_Click" />
        <asp:Label ID="lbErro" runat="server"></asp:Label>
        <asp:Button ID="btRecuperar" runat="server" Text="Recuperar password"
            CssClass="btn btn-danger" OnClick="btRecuperar_Click" />
    </div>
    <div class="pull-left col-md-4 col-sm-4 input-group">
        <asp:TextBox runat="server" ID="tbPesquisa" CssClass="form-control" />
        <span class="input-group-btn">
            <asp:Button OnClick="btPesquisa_Click" Text="Pesquisar" ID="btPesquisa" runat="server" CssClass="btn btn-info" />
        </span>
    </div>
    <div id="divLivros" runat="server" class="pull-left col-md-9"></div>
</asp:Content>
