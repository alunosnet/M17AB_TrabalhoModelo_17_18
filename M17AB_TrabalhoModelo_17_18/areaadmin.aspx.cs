using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_17_18 {
    public partial class areaadmin : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            //verificar se o utilizador é administrador
            if (Session["perfil"] == null || !Session["perfil"].Equals("0"))
                Response.Redirect("index.aspx");
            //esconder divs
            if (!IsPostBack) {
                divLivros.Visible = false;
                divUtilizadores.Visible = false;
                divEmprestimos.Visible = false;
                divConsultas.Visible = false;
            }

            //configurar as gridviews
            gvLivros.PageIndexChanging += new GridViewPageEventHandler(gvLivros_PageIndexChangingEvent);
            gvLivros.AllowPaging = true;
            gvLivros.PageSize = 5;

            gvEmprestimos.RowCommand += new GridViewCommandEventHandler(gvEmprestimos_RowCommand);
        }

        #region Livros
        private void gvLivros_PageIndexChangingEvent(object sender, GridViewPageEventArgs e) {
            gvLivros.PageIndex = e.NewPageIndex;
            atualizaGrelhaLivros();
        }
        protected void btLivros_Click(object sender, EventArgs e) {
            //mostrar div livros
            divLivros.Visible = true;
            //esconder as restantes divs
            divUtilizadores.Visible = false;
            divEmprestimos.Visible = false;
            divConsultas.Visible = false;
            //css botões
            btLivros.CssClass = "btn btn-info active";
            btUtilizadores.CssClass = "btn btn-info";
            btEmprestimos.CssClass = "btn btn-info";
            btConsultas.CssClass = "btn btn-info";
            //cache
            Response.CacheControl = "no-cache";
            //atualizar a grelha dos livros
            atualizaGrelhaLivros();
        }
        protected void btAdicionarLivro_Click(object sender, EventArgs e) {
            try {
                //validar os dados
                //nome
                string nome = tbNome.Text;
                if (nome == String.Empty || nome.Length > 100)
                    throw new Exception("O nome tem de ter pelo menos 1 letra e no máximo 100.");
                //ano
                int ano;

                if (int.TryParse(tbAno.Text, out ano) == false)
                    throw new Exception("O ano indicado não é válido");
                if (ano < 0 || ano > DateTime.Now.Year)
                    throw new Exception("O ano tem de ser superior a 0 e inferior ou igual ao ano atual");
                //data aquisição
                DateTime data;
                if (DateTime.TryParse(tbData.Text, out data) == false)
                    throw new Exception("A data indicada não é válida");
                if (data > DateTime.Now)
                    throw new Exception("A data tem ser inferior ou igual à data atual");

                //preço
                decimal preco;
                if (decimal.TryParse(tbPreco.Text, out preco) == false)
                    throw new Exception("O preço indicado não é válido");
                if (preco < 0 || preco >= 100)
                    throw new Exception("O preço tem de ser superior a 0 e inferior a 100.");
                //guardar o livro na bd
                int idLivro = BaseDados.Instance.adicionarLivro(nome, ano, data, preco);
                //envio da imagem da capa do livro
                if (fuCapa.HasFile == true) {
                    if (fuCapa.PostedFile.ContentType == "image/jpeg" ||
                        fuCapa.PostedFile.ContentType == "image/png") {
                        if (fuCapa.PostedFile.ContentLength > 0) {
                            string ficheiro = Server.MapPath(@"~\Imagens\");
                            ficheiro += idLivro + ".jpg";
                            fuCapa.SaveAs(ficheiro);
                        }
                    }
                }
                //atualizar a grelha dos livros
                atualizaGrelhaLivros();
                tbAno.Text = "";
                tbData.Text = "";
                tbNome.Text = "";
                tbPreco.Text = "";
            } catch(Exception erro) {
                lbErro.Text = "Ocorreu o seguinte erro: " + erro.Message;
            }
        }

        private void atualizaGrelhaLivros() {
            //limpar gridview
            gvLivros.Columns.Clear();
            gvLivros.DataSource = null;
            gvLivros.DataBind();
            //consulta bd
            DataTable dados = BaseDados.Instance.listaLivros();
            if (dados == null || dados.Rows.Count == 0) return;

            //mostrar na gridview a consulta
            //gvLivros.DataSource = dados;
            //gvLivros.DataBind();

            //configurar as colunas da gridview e datatable
            DataColumn dcRemover = new DataColumn();
            dcRemover.ColumnName = "Remover";
            dcRemover.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcRemover);

            DataColumn dcEditar = new DataColumn();
            dcEditar.ColumnName = "Editar";
            dcEditar.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcEditar);

            //associar o datatable à grid
            gvLivros.DataSource = dados;
            //desativar a geração automatica das colunas
            gvLivros.AutoGenerateColumns = false;

            //gridview
            HyperLinkField hlRemover = new HyperLinkField();
            hlRemover.HeaderText = "Remover"; //título da coluna
            hlRemover.DataTextField = "Remover";    //campo associado
            hlRemover.Text = "Remover Livro";   //texto clicavel
            //criar um link removerlivro.aspx?nlivro=1
            hlRemover.DataNavigateUrlFormatString = "removerlivro.aspx?nlivro={0}";
            hlRemover.DataNavigateUrlFields = new string[] {"nlivro"};
            gvLivros.Columns.Add(hlRemover);
            //coluna editar
            HyperLinkField hlEditar = new HyperLinkField();
            hlEditar.HeaderText = "Editar"; //título da coluna
            hlEditar.DataTextField = "Editar";    //campo associado
            hlEditar.Text = "Editar Livro";   //texto clicavel
            //criar um link editarlivro.aspx?nlivro=1
            hlEditar.DataNavigateUrlFormatString = "editarlivro.aspx?nlivro={0}";
            hlEditar.DataNavigateUrlFields = new string[] { "nlivro" };
            gvLivros.Columns.Add(hlEditar);

            //restantes colunas
            //nlivro
            BoundField bfNlivro = new BoundField();
            bfNlivro.HeaderText = "Nº Livro";
            bfNlivro.DataField = "nlivro";
            gvLivros.Columns.Add(bfNlivro);
            //nome
            BoundField bfNome = new BoundField();
            bfNome.HeaderText = "Nome";
            bfNome.DataField = "nome";
            gvLivros.Columns.Add(bfNome);
            //ano
            BoundField bfAno = new BoundField();
            bfAno.HeaderText = "Ano";
            bfAno.DataField = "ano";
            gvLivros.Columns.Add(bfAno);
            //data aquisição
            BoundField bfData = new BoundField();
            bfData.HeaderText = "Data Aquisição";
            bfData.DataField = "data_aquisicao";
            bfData.DataFormatString = "{0:dd-MM-yyyy}";
            gvLivros.Columns.Add(bfData);
            //preço
            BoundField bfPreco = new BoundField();
            bfPreco.HeaderText = "Preço";
            bfPreco.DataField = "preco";
            bfPreco.DataFormatString = "{0:C}";
            gvLivros.Columns.Add(bfPreco);
            //estado
            BoundField bfEstado = new BoundField();
            bfEstado.HeaderText = "Estado";
            bfEstado.DataField = "estado";
            gvLivros.Columns.Add(bfEstado);
            //capa
            ImageField ifCapa = new ImageField();
            ifCapa.HeaderText = "Capa";
            int rand = new Random().Next(999999);
            ifCapa.DataImageUrlFormatString = "~/Imagens/{0}.jpg?"+rand;
            ifCapa.DataImageUrlField = "nlivro";
            ifCapa.ControlStyle.Width = 100;
            gvLivros.Columns.Add(ifCapa);

            //associar o datatable à gridview
            gvLivros.DataBind();
        }
        #endregion
        #region Utilizadores
        //mostra a div dos utilizadores
        protected void btUtilizadores_Click(object sender, EventArgs e) {
            //mostrar div utilizadores
            divUtilizadores.Visible = true;
            //esconder as restantes divs
            divLivros.Visible = false;
            divEmprestimos.Visible = false;
            divConsultas.Visible = false;
            //css botões
            btLivros.CssClass = "btn btn-info";
            btUtilizadores.CssClass = "btn btn-info active";
            btEmprestimos.CssClass = "btn btn-info";
            btConsultas.CssClass = "btn btn-info";
            //cache
            Response.CacheControl = "no-cache";
            //atualizar a grelha dos utilizadores
            atualizaGrelhaUtilizadores();
        }

        private void atualizaGrelhaUtilizadores() {
            // limpar gridview
            gvUtilizadores.Columns.Clear();
            gvUtilizadores.DataSource = null;
            gvUtilizadores.DataBind();

            DataTable dados = BaseDados.Instance.listaTodosUtilizadores();

            if (dados == null || dados.Rows.Count == 0) return;

            //configurar as colunas da gridview e datatable
            DataColumn dcRemover = new DataColumn();
            dcRemover.ColumnName = "Remover";
            dcRemover.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcRemover);

            DataColumn dcEditar = new DataColumn();
            dcEditar.ColumnName = "Editar";
            dcEditar.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcEditar);

            DataColumn dcBloquear = new DataColumn();
            dcBloquear.ColumnName = "Bloquear";
            dcBloquear.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcBloquear);

            DataColumn dcHistorico = new DataColumn();
            dcHistorico.ColumnName = "Historico";
            dcHistorico.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcHistorico);

            //associar o datatable à grid
            gvUtilizadores.DataSource = dados;
            //desativar a geração automatica das colunas
            gvUtilizadores.AutoGenerateColumns = false;

            //gridview
            HyperLinkField hlRemover = new HyperLinkField();
            hlRemover.HeaderText = "Remover"; //título da coluna
            hlRemover.DataTextField = "Remover";    //campo associado
            hlRemover.Text = "Remover Utilizador";   //texto clicavel
            //criar um link removerlivro.aspx?nlivro=1
            hlRemover.DataNavigateUrlFormatString = "removerutilizador.aspx?id={0}";
            hlRemover.DataNavigateUrlFields = new string[] { "id" };
            gvUtilizadores.Columns.Add(hlRemover);
            //coluna editar
            HyperLinkField hlEditar = new HyperLinkField();
            hlEditar.HeaderText = "Editar"; //título da coluna
            hlEditar.DataTextField = "Editar";    //campo associado
            hlEditar.Text = "Editar Utilizador";   //texto clicavel
            //criar um link editarlivro.aspx?nlivro=1
            hlEditar.DataNavigateUrlFormatString = "editarutilizador.aspx?id={0}";
            hlEditar.DataNavigateUrlFields = new string[] { "id" };
            gvUtilizadores.Columns.Add(hlEditar);

            //coluna para bloquear/desbloquear
            HyperLinkField hlBloquear = new HyperLinkField();
            hlBloquear.HeaderText = "Bloquear/Desbloquear"; //título da coluna
            hlBloquear.DataTextField = "Bloquear";    //campo associado
            hlBloquear.Text = "Bloquear/Desbloquear";   //texto clicavel
            hlBloquear.DataNavigateUrlFormatString = "bloquearutilizador.aspx?id={0}";
            hlBloquear.DataNavigateUrlFields = new string[] { "id" };
            gvUtilizadores.Columns.Add(hlBloquear);
            //histórico
            HyperLinkField hlHistorico = new HyperLinkField();
            hlHistorico.HeaderText = "Histórico"; //título da coluna
            hlHistorico.DataTextField = "Historico";    //campo associado
            hlHistorico.Text = "Ver histórico";   //texto clicavel
            hlHistorico.DataNavigateUrlFormatString = "historico.aspx?id={0}";
            hlHistorico.DataNavigateUrlFields = new string[] { "id" };
            gvUtilizadores.Columns.Add(hlHistorico);

            //restantes colunas
            //id
            BoundField bfId = new BoundField();
            bfId.HeaderText = "Id";
            bfId.DataField = "id";
            gvUtilizadores.Columns.Add(bfId);
            //email
            BoundField bfEmail = new BoundField();
            bfEmail.HeaderText = "Email";
            bfEmail.DataField = "email";
            gvUtilizadores.Columns.Add(bfEmail);
            //nome
            BoundField bfNome = new BoundField();
            bfNome.HeaderText = "Nome";
            bfNome.DataField = "nome";
            gvUtilizadores.Columns.Add(bfNome);
            //morada
            BoundField bfMorada = new BoundField();
            bfMorada.HeaderText = "Morada";
            bfMorada.DataField = "morada";
            gvUtilizadores.Columns.Add(bfMorada);
            //nif
            BoundField bfNif = new BoundField();
            bfNif.HeaderText = "Nif";
            bfNif.DataField = "nif";
            gvUtilizadores.Columns.Add(bfNif);
            //estado
            BoundField bfEstado = new BoundField();
            bfEstado.HeaderText = "Estado";
            bfEstado.DataField = "estado";
            gvUtilizadores.Columns.Add(bfEstado);
            //perfil
            BoundField bfPerfil = new BoundField();
            bfPerfil.HeaderText = "Perfil";
            bfPerfil.DataField = "perfil";
            gvUtilizadores.Columns.Add(bfPerfil);

            //associar o datatable à gridview
            gvUtilizadores.DataBind();
        }

        //adicionar um utilizador
        protected void btUAdicionarUtilizador_Click(object sender, EventArgs e) {
            //validar os dados
            try {
                string email = tbUEmail.Text;
                if (email == String.Empty || email.Contains("@") == false) {
                    throw new Exception("O email indicado não é válido.");
                }
                string nome = tbUNome.Text;
                if (nome == String.Empty || nome.Length < 3) {
                    throw new Exception("O nome indicado não é válido, deve ter pelo menos 3 letras.");
                }
                string morada = tbUMorada.Text;
                if (morada == String.Empty || morada.Length < 3) {
                    throw new Exception("A morada indicada não é válida.");
                }
                string nif = tbUNif.Text;
                int z = int.Parse(nif);
                string password = tbUPassword.Text;
                if (password == String.Empty || password.Length < 5) {
                    throw new Exception("A password indicada não é válida, tem de ter pelo menos 5 letras.");
                }
                int perfil = int.Parse(ddPerfil.SelectedValue);
                if (perfil != 0 && perfil != 1)
                    throw new Exception("O perfil não é válido.");
                //adicionar o utilizador
                BaseDados.Instance.registarUtilizador(email, nome, morada, nif, password,1,perfil);


            } catch (Exception erro) {
                lbUErro.Text = "Ocorreu o seguinte erro: " + erro.Message;
                lbUErro.CssClass = "alert alert-danger";
            }
            //atualizar a grelha
            atualizaGrelhaUtilizadores();
        }
        #endregion
        #region Emprestimos
        //mostra div emprestimos
        protected void btEmprestimos_Click(object sender, EventArgs e) {
            //mostrar div emprestimos
            divEmprestimos.Visible = true;
            //esconder as restantes divs
            divUtilizadores.Visible = false;
            divLivros.Visible = false;
            divConsultas.Visible = false;
            //css botões
            btLivros.CssClass = "btn btn-info";
            btUtilizadores.CssClass = "btn btn-info";
            btEmprestimos.CssClass = "btn btn-info active";
            btConsultas.CssClass = "btn btn-info";
            //cache
            Response.CacheControl = "no-cache";
            //atualizar a grelha dos emprestimos
            atualizaGrelhaEmprestimos();
            //atualizar dropdown utilizadores
            atualizaDDUtilizadores();
            //atualizar dropdown livros
            atualizaDDLivros();
        }

        private void atualizaDDLivros() {
            ddLivro.Items.Clear();
            DataTable dados = BaseDados.Instance.listaLivrosDisponiveis();
            foreach(DataRow livro in dados.Rows) {
                ddLivro.Items.Add(new ListItem(livro[1].ToString(),
                    livro[0].ToString()));
            }
        }

        private void atualizaDDUtilizadores() {
            ddUtilizador.Items.Clear();
            DataTable dados = BaseDados.Instance.listaUtilizadoresDisponiveis();
            foreach(DataRow utilizador in dados.Rows) {
                ddUtilizador.Items.Add(new ListItem(utilizador[1].ToString(),
                    utilizador[0].ToString()));
            }
        }

        private void atualizaGrelhaEmprestimos() {
            gvEmprestimos.Columns.Clear();
            gvEmprestimos.DataSource = null;
            gvEmprestimos.DataBind();

            DataTable dados;
            if (cbEmprestimos.Checked)
                dados = BaseDados.Instance.listaTodosEmprestimosPorConcluirComNomes();
            else
                dados= BaseDados.Instance.listaTodosEmprestimosComNomes();
            if (dados == null || dados.Rows.Count == 0) return;

            //receber livro
            ButtonField btReceberLivro = new ButtonField();
            btReceberLivro.HeaderText = "Receber Livro";
            btReceberLivro.Text = "Receber";
            btReceberLivro.ButtonType = ButtonType.Button;
            btReceberLivro.CommandName = "receber";
            gvEmprestimos.Columns.Add(btReceberLivro);

            //enviar email
            ButtonField btEnviarEmail = new ButtonField();
            btEnviarEmail.HeaderText = "Enviar email";
            btEnviarEmail.Text = "Enviar";
            btEnviarEmail.ButtonType = ButtonType.Button;
            btEnviarEmail.CommandName = "enviar";
            gvEmprestimos.Columns.Add(btEnviarEmail);

            gvEmprestimos.DataSource = dados;
            gvEmprestimos.DataBind();
        }

        //adiciona emprestimo
        protected void btEAdicionar_Click(object sender, EventArgs e) {
            try {
                int idLeitor = int.Parse(ddUtilizador.SelectedValue);
                int idLivro = int.Parse(ddLivro.SelectedValue);
                DateTime data = DataDevolve.SelectedDate;
                BaseDados.Instance.adicionarEmprestimo(idLivro, idLeitor, data);
                atualizaGrelhaEmprestimos();
                atualizaDDLivros();
            } catch(Exception erro) {
                lbEErro.Text = "Ocorreu o seguinte erro: " + erro.Message;
                lbEErro.CssClass = "alert alert-danger";
            }
        }

        private void gvEmprestimos_RowCommand(object sender, GridViewCommandEventArgs e) {
            int linha = int.Parse(e.CommandArgument as string);
            int idEmprestimo = int.Parse(gvEmprestimos.Rows[linha].Cells[2].Text);
            if (e.CommandName == "receber") {
                BaseDados.Instance.concluirEmprestimo(idEmprestimo);
                atualizaGrelhaEmprestimos();
                atualizaDDLivros();
            }
            if(e.CommandName== "enviar") {
                string emailDeEnvio = "alunosnet@gmail.com";
                string assunto = "Livro emprestado";
                string mensagem = "Caro leitor deve devolver o livro que tem emprestado.";
                DataTable dadosEmprestimo = BaseDados.Instance.devolveDadosEmprestimo(idEmprestimo);
                DataTable dadosLeitor = BaseDados.Instance.devolveDadosUtilizador(
                    int.Parse(dadosEmprestimo.Rows[0]["idutilizador"].ToString())
                    );
                string emailParaQuem = dadosLeitor.Rows[0]["email"].ToString();
                string pwdEmail = ConfigurationManager.AppSettings["pwdEmail"].ToString();
                BaseDados.enviarMail(emailDeEnvio, pwdEmail, emailParaQuem, assunto, mensagem);
            }
        }
        protected void cbEmprestimos_CheckedChanged(object sender, EventArgs e) {
            atualizaGrelhaEmprestimos();
        }
        #endregion
        #region consultas
        protected void btConsultas_Click(object sender, EventArgs e) {
            //mostrar div consultas
            divConsultas.Visible = true;
            //esconder as restantes divs
            divUtilizadores.Visible = false;
            divEmprestimos.Visible = false;
            divLivros.Visible = false;
            //css botões
            btLivros.CssClass = "btn btn-info";
            btUtilizadores.CssClass = "btn btn-info";
            btEmprestimos.CssClass = "btn btn-info";
            btConsultas.CssClass = "btn btn-info active";
            //cache
            Response.CacheControl = "no-cache";
            atualizaGrelhaConsultas();
        }
        protected void ddConsula_SelectedIndexChanged(object sender, EventArgs e) {
            atualizaGrelhaConsultas();
        }

        private void atualizaGrelhaConsultas() {
            gvConsultas.Columns.Clear();
            int iconsulta = int.Parse(ddConsula.SelectedValue);
            DataTable dados;
            string sql= "";
            switch (iconsulta) {
                case 0: sql = @"select nome,count(nlivro) as [nr emprestimos] from utilizadores inner join emprestimos
                                on utilizadores.id=emprestimos.idutilizador
                                group by utilizadores.id,nome
                                order by [nr emprestimos] DESC";
                    break;
                case 1: sql = @"select nome,count(*) as [nr emprestimos] from livros inner join emprestimos
                                on livros.nlivro=emprestimos.nlivro
                                group by livros.nlivro,nome
                                order by [nr emprestimos] DESC";
                    break;
                case 2: sql = @"select emprestimos.*,utilizadores.nome as Leitor,utilizadores.email,livros.nome as Livro
                                from emprestimos
                                inner join utilizadores on utilizadores.id=emprestimos.idutilizador
                                inner join livros on livros.nlivro=emprestimos.nlivro
                                where emprestimos.estado=1 and data_devolve<getdate()
                                order by emprestimos.data_devolve 
                                ";
                    break;
                case 3: sql = @"select nome,data_aquisicao from livros
                                where DATEDIFF(day, data_aquisicao, getdate())< 7";
                    break;
                case 4: sql = @"select avg(datediff(day,data_emprestimo,data_devolve)) from emprestimos";
                    break;
                case 5:
                    sql = @"select nome from utilizadores inner join emprestimos 
                            on emprestimos.idutilizador=utilizadores.id
                            where emprestimos.nlivro=(select top 1 nlivro from livros order by preco desc)";
                    break;
            }
            dados = BaseDados.Instance.devolveConsulta(sql);
            gvConsultas.DataSource = dados;
            gvConsultas.DataBind();
        }

        #endregion


    }
}