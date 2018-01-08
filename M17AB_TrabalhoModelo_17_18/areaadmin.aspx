<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="areaadmin.aspx.cs" Inherits="M17AB_TrabalhoModelo_17_18.areaadmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Área de administrador</h1>
    <div class="btn-group">
        <asp:Button runat="server" ID="btLivros" Text="Gerir Livros" CssClass="btn btn-info" OnClick="btLivros_Click" />
        <asp:Button runat="server" ID="btUtilizadores" Text="Gerir Utilizadores" CssClass="btn btn-info" OnClick="btUtilizadores_Click" />
        <asp:Button runat="server" ID="btEmprestimos" Text="Gerir Empréstimos" CssClass="btn btn-info" OnClick="btEmprestimos_Click" />
        <asp:Button runat="server" ID="btConsultas" Text="Consultas" CssClass="btn btn-info" OnClick="btConsultas_Click" />
    </div>
    <div id="divLivros" runat="server">
        <h2>Livros</h2>
        <asp:GridView ID="gvLivros" runat="server" CssClass="table table-responsive"></asp:GridView>
        <h3>Adicionar</h3>
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
        <!--capa-->
        <div class="form-group">
            <label for="fuCapa">Capa:</label>
            <asp:FileUpload runat="server" ID="fuCapa" CssClass="form-control" />
        </div>
        <!--erros-->
        <asp:Label runat="server" ID="lbErro"></asp:Label>
        <asp:Button runat="server" ID="btAdicionarLivro" Text="Adicionar"
            CssClass="btn btn-info" OnClick="btAdicionarLivro_Click" />
    </div>
    <div id="divUtilizadores" runat="server"></div>
    <div id="divEmprestimos" runat="server"></div>
    <div id="divConsultas" runat="server"></div>
</asp:Content>
