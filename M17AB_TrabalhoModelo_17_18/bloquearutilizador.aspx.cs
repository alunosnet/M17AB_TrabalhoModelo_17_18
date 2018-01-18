using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_17_18 {
    public partial class bloquearutilizador : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            //verificar se é admin
            if (Session["perfil"] == null || !Session["perfil"].Equals("0"))
                Response.Redirect("index.aspx");
            //id utilizador
            try {
                int id = int.Parse(Request["id"].ToString());
                BaseDados.Instance.ativarDesativarUtilizador(id);
            }catch(Exception ) {

            }
            Response.Redirect("areaadmin.aspx");
        }
    }
}