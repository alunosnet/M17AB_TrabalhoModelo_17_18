﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_17_18 {
    public partial class index : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            //se tem sessão iniciada não mostra div login
            if (Session["perfil"] != null)
                divLogin.Visible = false;
            //listar os livros
            if (!IsPostBack) {
                DataTable dados;
                HttpCookie cookie = Request.Cookies["ultimolivro"] as HttpCookie;
                try {
                    int id = int.Parse(cookie.Value);
                    dados = BaseDados.Instance.listaLivrosComPrecoInferior(id);
                } catch {
                    dados = BaseDados.Instance.listaLivrosDisponiveis();
                }
                atualizaDivLivros(dados);
            }
        }
        private void atualizaDivLivros(DataTable dados) {
            if (dados == null || dados.Rows.Count == 0) {
                divLivros.InnerHtml = "";
                return;
            }
            string grelha = "<div class='container-fluid'>";
            grelha += "<div class='row'>";
            foreach (DataRow livro in dados.Rows) {
                grelha += "<div class='col-md-4 text-center'>";
                grelha += "<img src='/Imagens/" + livro[0].ToString() + ".jpg' class='img-responsive'/>";
                grelha += "<span class='stat-title'>" + livro[1].ToString() + "</span>";
                grelha += "<span class='stat-title'>" +
                    String.Format(" | {0:C}", Decimal.Parse(livro["preco"].ToString())) + "</span>";
                grelha += "<br/><a href='detalheslivro.aspx?id=" + livro[0].ToString() + "'>Detalhes</a>";
                grelha += "</div>";
            }
            grelha += "</div></div>";
            divLivros.InnerHtml = grelha;
        }
        protected void btLogin_Click(object sender, EventArgs e) {
            try {
                string email = tbEmail.Text;
                string password = tbPassword.Text;
                DataTable dados = BaseDados.Instance.verificarLogin(email, password);
                if (dados == null || dados.Rows.Count == 0)
                    throw new Exception("Login falhou.");

                //perfil
                string perfil = dados.Rows[0]["perfil"].ToString();
                //guardar na variável session
                //nome
                Session["nome"] = dados.Rows[0]["nome"].ToString();
                //perfil
                Session["perfil"] = perfil;
                //id
                Session["id"] = dados.Rows[0]["id"].ToString();
                //area de acordo com o perfil
                if (perfil == "0")
                    Response.Redirect("areaadmin.aspx");
                else
                    Response.Redirect("areacliente.aspx");
            } catch (Exception erro) {
                lbErro.Text = erro.Message;
                lbErro.CssClass = "alert alert-danger";
            }
        }

        protected void btRecuperar_Click(object sender, EventArgs e) {
            try {
                if (tbEmail.Text == String.Empty)
                    throw new Exception("Tem de indicar um email.");

                //verificar se email existe
                string email = tbEmail.Text;
                DataTable dados = BaseDados.Instance.devolveDadosUtilizador(email);
                if (dados == null || dados.Rows.Count == 0)
                    throw new Exception("");

                //GUID
                Guid g = Guid.NewGuid();
                //guardar guid na bd
                BaseDados.Instance.recuperarPassword(email, g.ToString());
                //enviar email com guid
                string mensagem = "Clique no link para recuperar a sua password.\n";
                mensagem += "<a href='http://" + Request.Url.Authority + "/recuperarPassword.aspx?id=";
                mensagem += Server.UrlEncode(g.ToString()) + "'>Clique aqui</a>";
                string senha = ConfigurationManager.AppSettings["pwdEmail"].ToString();
                BaseDados.enviarMail("alunosnet@gmail.com", senha, email, "Recuperação de password", mensagem);
                lbErro.Text = "Foi enviado um email de recuperação";
                lbErro.CssClass = "alert alert-success";
            } catch (Exception erro) {

            }
        }

        protected void btPesquisa_Click(object sender, EventArgs e) {
            string pesquisa = tbPesquisa.Text;
            if (pesquisa == string.Empty) return;
            DataTable dados = BaseDados.Instance.pesquisaLivrosPeloNome(pesquisa);
            atualizaDivLivros(dados);
        }
    }
}