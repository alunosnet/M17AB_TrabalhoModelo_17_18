using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_17_18 {
    public partial class registo : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void btRegistar_Click(object sender, EventArgs e) {
            //validar os dados
            try {
                string email = tbEmail.Text;
                if (email == String.Empty || email.Contains("@") == false) {
                    throw new Exception("O email indicado não é válido.");
                }
                string nome = tbNome.Text;
                if(nome==String.Empty || nome.Length < 3) {
                    throw new Exception("O nome indicado não é válido, deve ter pelo menos 3 letras.");
                }
                string morada = tbMorada.Text;
                if(morada==String.Empty || morada.Length < 3) {
                    throw new Exception("A morada indicada não é válida.");
                }
                string nif = tbNif.Text;
                int z = int.Parse(nif);
                string password = tbPassword.Text;
                if(password==String.Empty || password.Length <5) {
                    throw new Exception("A password indicada não é válida, tem de ter pelo menos 5 letras.");
                }
                //validar o recaptcha
                //registar o utilizador

                //redirecionar para index
            } catch (Exception erro) {
                lbErro.Text = "Ocorreu o seguinte erro: " + erro.Message;
                lbErro.CssClass = "alert alert-danger";
            }

        }
    }
}