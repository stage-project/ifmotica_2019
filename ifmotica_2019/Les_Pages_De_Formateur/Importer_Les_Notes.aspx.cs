using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Configuration;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace ifmotica_2019.Les_Pages_De_Formateur
{
    public partial class Importer_Les_Notes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // vider la selection du nom de fichier excel
            string filename = "";
            //vérifier la sélection de fichiers
            if (FileUploadToServer.HasFile)
            {
                try
                {
                    //table des extensions autorisées
                    string[] allowdFile = { ".xlsx", ".xls" };
                    string FileExt = System.IO.Path.GetExtension(FileUploadToServer.PostedFile.FileName);
                    bool isvalidFile = allowdFile.Contains(FileExt);
                    if (!(isvalidFile))
                    {
                        lblmsg.ForeColor = System.Drawing.Color.Red; lblmsg.Text = "upload only excel";
                    }
                    else
                    {
                        //la taille de fichier
                        int filesize = FileUploadToServer.PostedFile.ContentLength;
                        //la taile du fichier excel ne depasse pas 4MB
                        if (filesize <= 4048576)
                        {
                            //le nom de fichier importer+leur extention Path.GetFileName(Server.MapPath(FileUploadToServer.FileName)) ou filename
                            //le chema de fichier Server.MapPath(FilePath)
                            //le nom dossier pour enregistrer le fichier excel "~/datas/"
                            filename = Path.GetFileName(Server.MapPath(FileUploadToServer.FileName));
                            FileUploadToServer.SaveAs(Server.MapPath("~/Data/") + filename);
                            string filePath = Server.MapPath("~/Data/") + filename;
                            OleDbConnection con = null;
                            //connexion ça dépend la version du fichier excel 
                            if (FileExt == ".xls")
                            {
                                con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=Excel 8.0;");
                            }
                            else if (FileExt == ".xlsx")
                            {
                                con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;");
                            }
                            con.Open();
                            //selection du nom de la feuille excel 
                            DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string getExcelSheetName = dt.Rows[0]["Table_Name"].ToString();
                            //la creation de dataset
                            OleDbCommand ExcelCommand = new OleDbCommand(@"select * from [" + getExcelSheetName + "]", con);
                            OleDbDataAdapter ExcelAdapter = new OleDbDataAdapter(ExcelCommand);
                            DataSet ExcelDataSet = new DataSet();
                            ExcelAdapter.Fill(ExcelDataSet);
                            con.Close();
                            //affichier les donnee sur la datagrid      
                            GridView1.DataSource = ExcelDataSet;
                            GridView1.DataBind();
                        }
                        else
                        {
                            lblmsg.Text = "le fichier excel doit etre moins de 1MB";
                        }
                    }

                }
                catch (Exception ex)
                {
                    lblmsg.Text = "error occurred while uploading a file:" + ex.Message;
                }
            }
            else lblmsg.Text = "error aucune fichier été sélectionné!";
        }
    }
}