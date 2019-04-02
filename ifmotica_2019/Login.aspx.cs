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
            string R1, output = "0", user = "", etat = "", compt = "0";
            R1 = "select count(nom) ,nom,prenom,Etat,compt from Users where utilisateur='"+TextBox1.Text+"' and MotPasse='"+TextBox2.Text+"' group by nom,Prenom,Etat,compt ";
            SqlCommand cmd1 = new SqlCommand(R1, d.ouver());
            SqlDataReader dr;
            dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                output = dr[0].ToString();
                 user= dr[1].ToString() + "  " + dr[2].ToString();
                etat = dr[3].ToString();
                compt = dr[4].ToString();
            }
            d.ferme();

            if (output == "1")
            {
                if (compt == "0")
                {
                    Response.Redirect("~/changer_Password.aspx");
                }
                else
                {
                    Response.Redirect(d.Redirect(etat));

                }
            }
            else
            {
                Response.Write("Your User Name and Password is wrong !");
            }

        }
    }
}