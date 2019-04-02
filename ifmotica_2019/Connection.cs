using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace ifmotica_2019
{
    public class Connection
    {
        public SqlConnection con = new SqlConnection("Data Source = DESKTOP-V8IGHQD; initial catalog = IFMOTICA1; integrated security = true");
        public DataSet ds = new DataSet();
        public SqlDataAdapter da;

        public SqlConnection ouver()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            return con;
        }
        public SqlConnection ferme()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            return con;
        }

        //pour determiner la promotion
        public int annee()
        {
            int anne = 0;
            if (DateTime.Now.Month >= 9 && DateTime.Now.Month <= 12)
            {
                anne = int.Parse(DateTime.Now.Year.ToString()) + 1;
            }
            else if (DateTime.Now.Month >= 1 && DateTime.Now.Month <= 7) anne = int.Parse(DateTime.Now.Year.ToString());

            return anne;
        }


    }
}