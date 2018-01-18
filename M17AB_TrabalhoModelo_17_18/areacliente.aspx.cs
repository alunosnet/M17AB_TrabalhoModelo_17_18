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
        }

        protected void btEmprestimo_Click(object sender, EventArgs e) {
            divEmprestimo.Visible = true;
            divDevolver.Visible = false;
            divHistorico.Visible = false;

            //todo: atualizagrelhaemprestimo
        }

        protected void btDevolve_Click(object sender, EventArgs e) {
            divEmprestimo.Visible = false;
            divDevolver.Visible = true;
            divHistorico.Visible = false;

            //todo: atualizagrelhaemprestimo
        }

        protected void btHistorico_Click(object sender, EventArgs e) {
            divEmprestimo.Visible = false;
            divDevolver.Visible = false;
            divHistorico.Visible = true;

            //todo: atualizagrelhaemprestimo
        }
    }
}