using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_17_18 {
    public partial class historico : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            //verificar se é admin
            if (Session["perfil"] == null || !Session["perfil"].Equals("0"))
                Response.Redirect("index.aspx");
            //listar o histórico do leitor pelo id
            try {
                int id = int.Parse(Request["id"].ToString());
                DataTable dados = BaseDados.Instance.listaTodosEmprestimosComNomes(id);
                gvHistorico.DataSource = dados;
                gvHistorico.DataBind();
            } catch {
                Response.Redirect("areaadmin.aspx");
            }
        }
    }
}