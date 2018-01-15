using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17AB_TrabalhoModelo_17_18 {
    public partial class MasterPage : System.Web.UI.MasterPage {
        protected void Page_Load(object sender, EventArgs e) {
            //verificar se já aceitou os cookies
            HttpCookie cookie = Request.Cookies["avisoCookies"] as HttpCookie;
            if (cookie != null)
                div_aviso.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e) {
            //criar o cookie que indica a aceitação dos cookies
            Guid g = Guid.NewGuid();

            HttpCookie cookie = new HttpCookie("avisoCookies", g.ToString());
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);
            div_aviso.Visible = false;
        }
    }
}