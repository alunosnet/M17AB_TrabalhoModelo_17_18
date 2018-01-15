<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="editarlivro.aspx.cs" Inherits="M17AB_TrabalhoModelo_17_18.editarlivro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Editar</h3>
    <!--Nome-->
    <div class="form-group">
        <label for="tbNome">Nome:</label>
        <asp:TextBox runat="server" ID="tbNome" CssClass="form-control"></asp:TextBox>
    </div>
    <!--ano-->
    <div class="form-group">
        <label for="tbAno">Ano:</label>
        <asp:TextBox runat="server" ID="tbAno" CssClass="form-control"></asp:TextBox>
    </div>
    <!--data aquisição-->
    <div class="form-group">
        <label for="tbData">Data de Aquisição:</label>
        <asp:TextBox runat="server" ID="tbData" CssClass="form-control"></asp:TextBox>
    </div>
    <!--preço-->
    <div class="form-group">
        <label for="tbPreco">Preço:</label>
        <asp:TextBox runat="server" ID="tbPreco" CssClass="form-control"></asp:TextBox>
    </div>
    <!--capa atual-->
    <asp:Image ID="imgCapa" runat="server" />
    <!--capa-->
    <div class="form-group">
        <label for="fuCapa">Capa:</label>
        <asp:FileUpload runat="server" ID="fuCapa" CssClass="form-control" />
    </div>
    <!--erros-->
    <asp:Label runat="server" ID="lbErro"></asp:Label>
    <asp:Button runat="server" ID="btEditarLivro" Text="Editar"
        CssClass="btn btn-info" OnClick="btEditarLivro_Click" />
</asp:Content>
