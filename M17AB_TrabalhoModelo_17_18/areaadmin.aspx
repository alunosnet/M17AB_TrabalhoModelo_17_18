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
    <div id="divUtilizadores" runat="server">
        <h2>Utilizadores</h2>
        <asp:GridView CssClass="table table-responsive" runat="server" ID="gvUtilizadores" />
        <h3>Adicionar</h3>
        <!--Email-->
        <div class="form-group">
            <label for="tbUEmail">Email:</label>
            <asp:TextBox runat="server" ID="tbUEmail" CssClass="form-control"></asp:TextBox>
        </div>
        <!--Nome-->
        <div class="form-group">
            <label for="tbUNome">Nome:</label>
            <asp:TextBox runat="server" ID="tbUNome" CssClass="form-control"></asp:TextBox>
        </div>
        <!--Morada-->
        <div class="form-group">
            <label for="tbUMorada">Morada:</label>
            <asp:TextBox runat="server" ID="tbUMorada" CssClass="form-control"></asp:TextBox>
        </div>
        <!--nif-->
        <div class="form-group">
            <label for="tbUNif">NIF:</label>
            <asp:TextBox runat="server" ID="tbUNif" CssClass="form-control"></asp:TextBox>
        </div>
        <!--password-->
        <div class="form-group">
            <label for="tbUPassword">Password:</label>
            <asp:TextBox TextMode="Password" runat="server" ID="tbUPassword" CssClass="form-control"></asp:TextBox>
        </div>
        <!--perfil-->
        <div class="form-group">
            <label for="ddPerfil">Perfil</label>
            <asp:DropDownList runat="server" ID="ddPerfil" CssClass="form-control">
                <asp:ListItem Value="0">Administrador</asp:ListItem>
                <asp:ListItem Value="1" Selected="True">Leitor</asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:Label runat="server" ID="lbUErro" />
        <asp:Button CssClass="btn btn-danger" runat="server" ID="btUAdicionarUtilizador" Text="Adicionar" OnClick="btUAdicionarUtilizador_Click" />
    </div>
    <div id="divEmprestimos" runat="server">
        <h2>Empréstimos</h2>
        Só listar empréstimos por concluir:<asp:CheckBox runat="server" ID="cbEmprestimos" AutoPostBack="true" OnCheckedChanged="cbEmprestimos_CheckedChanged" />
        <asp:GridView runat="server" ID="gvEmprestimos" CssClass="table table-responsive" />
        <h3>Adicionar</h3>
        <div class="form-group">
            <label for="ddLivro">Livro:</label>
            <asp:DropDownList runat="server" ID="ddLivro" CssClass="form-control" />
        </div>
        <div class="form-group">
            <label for="ddUtilizador">Leitor:</label>
            <asp:DropDownList runat="server" ID="ddUtilizador" CssClass="form-control" />
        </div>
        <div class="form-group">
            <label for="DataDevolve">Data de Devolução:</label>
            <asp:Calendar runat="server" ID="DataDevolve"></asp:Calendar>
        </div>
        <asp:Label runat="server" ID="lbEErro" />
        <asp:Button runat="server" ID="btEAdicionar" Text="Adicionar" CssClass="btn btn-danger" OnClick="btEAdicionar_Click" />
    </div>
    <div id="divConsultas" runat="server"></div>
</asp:Content>
