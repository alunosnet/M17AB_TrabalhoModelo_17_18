using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_17_18 {
    public partial class index : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

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
            }catch(Exception erro) {
                lbErro.Text = erro.Message;
                lbErro.CssClass = "alert alert-danger";
            }
        }
    }
}