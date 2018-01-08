using System;
using System.Collections.Generic;
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

        }

        protected void btLivros_Click(object sender, EventArgs e) {

        }

        protected void btUtilizadores_Click(object sender, EventArgs e) {

        }

        protected void btEmprestimos_Click(object sender, EventArgs e) {

        }

        protected void btConsultas_Click(object sender, EventArgs e) {

        }

        protected void btAdicionarLivro_Click(object sender, EventArgs e) {

        }
    }
}