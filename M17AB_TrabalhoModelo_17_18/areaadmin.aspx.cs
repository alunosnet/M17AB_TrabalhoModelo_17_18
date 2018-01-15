using System;
using System.Collections.Generic;
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
        }

        private void gvLivros_PageIndexChangingEvent(object sender, GridViewPageEventArgs e) {
            gvLivros.PageIndex = e.NewPageIndex;
            atualizaGrelhaLivros();
        }
        #region Livros
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
        protected void btUtilizadores_Click(object sender, EventArgs e) {

        }
        #endregion
        #region Emprestimos
        protected void btEmprestimos_Click(object sender, EventArgs e) {

        }
        #endregion
        #region consultas
        protected void btConsultas_Click(object sender, EventArgs e) {

        }
        #endregion

    }
}