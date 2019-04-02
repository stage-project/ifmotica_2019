using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace ifmotica_2019
{
    public partial class Login : System.Web.UI.Page
    {
        Connection d = new Connection();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            String R1 = "select count(nom) ,nom,prenom,Etat,compt from Users where utilisateur='"+TextBox1.Text+"' and MotPasse='"+TextBox2.Text+"' group by nom,Prenom,Etat,compt ";
            SqlCommand cmd1 = new SqlCommand(R1, d.ouver());
            SqlDataReader dr;
            dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                Session["User"] = dr[0].ToString() + "  " + dr[1].ToString();
            }
            d.ferme();

            if (output == "1")
            {

                Response.Write("la connection et bien passer");
                //Response.Redirect("~/Stagiaires.aspx");
            }
            else
            {
                Response.Write("Your User Name and Password is wrong !");
            }

        }
    }
}