using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_17_18 {
    public partial class areacliente : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            //verificar se tem login e é leitor
            if (Session["perfil"] == null || Session["perfil"].Equals("0"))
                Response.Redirect("index.aspx");
            //verificar se é postback
            if (!IsPostBack) {
                divDevolver.Visible = false;
                divEmprestimo.Visible = false;
                divHistorico.Visible = false;
            }
            //evento click no command button devolver
            gvDevolver.RowCommand += new GridViewCommandEventHandler(gvDevolver_RowCommand);
        }

        private void gvDevolver_RowCommand(object sender, GridViewCommandEventArgs e) {
            int linha = int.Parse(e.CommandArgument as string);
            int id = int.Parse(gvDevolver.Rows[linha].Cells[1].Text);
            if (e.CommandName == "devolver") {
                BaseDados.Instance.concluirEmprestimo(id);
                atualizaGrelhaDevolve();
            }
        }

        protected void btEmprestimo_Click(object sender, EventArgs e) {
            divEmprestimo.Visible = true;
            divDevolver.Visible = false;
            divHistorico.Visible = false;

            atualizaGrelhaEmprestimo();
        }
        private void atualizaGrelhaEmprestimo() {
            gvEmprestar.Columns.Clear();
            gvEmprestar.DataSource = null;
            gvEmprestar.DataBind();

            gvEmprestar.DataSource = BaseDados.Instance.listaLivrosDisponiveis();

            gvEmprestar.DataBind();
        }
        protected void btDevolve_Click(object sender, EventArgs e) {
            divEmprestimo.Visible = false;
            divDevolver.Visible = true;
            divHistorico.Visible = false;

            atualizaGrelhaDevolve();
        }

        private void atualizaGrelhaDevolve() {
            gvDevolver.Columns.Clear();
            gvDevolver.DataSource = null;
            gvDevolver.DataBind();

            int idUtilizador = int.Parse(Session["id"].ToString());
            gvDevolver.DataSource = BaseDados.Instance.listaTodosEmprestimosPorConcluirComNomes(idUtilizador);

            //botão devolver
            ButtonField btDevolver = new ButtonField();
            btDevolver.HeaderText = "Devolver";
            btDevolver.Text = "Devolver";
            btDevolver.CommandName = "devolver";
            btDevolver.ButtonType = ButtonType.Button;
            gvDevolver.Columns.Add(btDevolver);

            gvDevolver.DataBind();
        }

        protected void btHistorico_Click(object sender, EventArgs e) {
            divEmprestimo.Visible = false;
            divDevolver.Visible = false;
            divHistorico.Visible = true;

            atualizaGrelhaHistorico();
        }
        private void atualizaGrelhaHistorico() {
            gvHistorico.Columns.Clear();
            gvHistorico.DataSource = null;
            gvHistorico.DataBind();

            int idUtilizador = int.Parse(Session["id"].ToString());

            gvHistorico.DataSource = BaseDados.Instance.listaTodosEmprestimosComNomes(idUtilizador);
            gvHistorico.DataBind();
        }
    }
}