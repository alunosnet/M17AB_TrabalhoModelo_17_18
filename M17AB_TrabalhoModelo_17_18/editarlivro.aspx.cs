using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_17_18 {
    public partial class editarlivro : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            //verificar se é um admin
            if (Session["perfil"] == null || !Session["perfil"].Equals("0"))
                Response.Redirect("index.aspx");
            //mostrar os dados do livro
            if (!IsPostBack) {
                try {
                    int nlivro = int.Parse(Request["nlivro"].ToString());
                    DataTable dados = BaseDados.Instance.devolveDadosLivro(nlivro);
                    if (dados == null || dados.Rows.Count == 0)
                        throw new Exception("Erro");
                    tbNome.Text = dados.Rows[0]["nome"].ToString();
                    tbAno.Text = dados.Rows[0]["ano"].ToString();
                    tbData.Text = DateTime.Parse(dados.Rows[0]["data_aquisicao"]
                        .ToString()).ToShortDateString();
                    tbPreco.Text =dados.Rows[0]["preco"].ToString();
                    
                    //capa
                    string ficheiro = @"~\Imagens\" + nlivro + ".jpg";
                    imgCapa.ImageUrl = ficheiro;
                    imgCapa.Width = 100;
                } catch (Exception erro) {
                    Response.Redirect("areaadmin.aspx");
                }
            }
        }

        protected void btEditarLivro_Click(object sender, EventArgs e) {
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
                //atualizar o livro na bd
                int nlivro = int.Parse(Request["nlivro"].ToString());
                BaseDados.Instance.atualizaLivro(nlivro, nome, ano, data, preco);
                //envio da imagem da capa do livro
                if (fuCapa.HasFile == true) {
                    if (fuCapa.PostedFile.ContentType == "image/jpeg" ||
                        fuCapa.PostedFile.ContentType == "image/png") {
                        if (fuCapa.PostedFile.ContentLength > 0) {
                            string ficheiro = Server.MapPath(@"~\Imagens\");
                            ficheiro += nlivro + ".jpg";
                            fuCapa.SaveAs(ficheiro);
                        }
                    }
                }
                Response.Redirect("areaadmin.aspx");
            } catch (Exception erro) {
                lbErro.Text = "Ocorreu o seguinte erro: " + erro.Message;
            }
        }
    }
}