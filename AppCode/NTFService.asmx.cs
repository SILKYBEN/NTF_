using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace NTF.AppCode
{
    /// <summary>
    /// Summary description for FasadaService
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    [WebService(Namespace = "http://www.adstestsite.com/NTFService.asmx/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class NTFService : System.Web.Services.WebService
    {
        public static string con = ConnectionString.Connection();
        Random _r = new Random();
        [WebMethod]
        public void VerifyLogin(string email_ln, string pass_ln)
        {
            try
            {
                SqlParameter[] @params =
                {
                    new SqlParameter("@Email", email_ln),
                    new SqlParameter("@Password",BLL.Encrypt(pass_ln))
                };

                DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "SP_PlayerLogin", @params);
                String number = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (number == "-1")
                {
                    String feedback = BLL.OutPut_Message(number, ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                    Context.Response.Write(feedback);
                }
                else
                {
                    Context.Response.Write(ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                }

            }
            catch (Exception ex)
            {
                Context.Response.Write(BLL.OutPut_Message("-1", ex.Message));
            }

        }

        [WebMethod]
        public void VerifyLoginAdmin(string email_ln, string pass_ln)
        {
            try
            {
                SqlParameter[] @params =
                {
                    new SqlParameter("@Email", email_ln),
                    new SqlParameter("@Password",pass_ln)
                };

                DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "SP_AdminLogin", @params);
                String number = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (number == "-1")
                {
                    String feedback = BLL.OutPut_Message(number, ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                    Context.Response.Write(feedback);
                }
                else
                {
                    Context.Response.Write(ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                }

            }
            catch (Exception ex)
            {
                Context.Response.Write(BLL.OutPut_Message("-1", ex.Message));
            }

        }
        [WebMethod]
        public void AddUser(string sname, string othername, string email_ln,
            string phone_ln, string gender_ln,
            string dob_ln, string pass_ln,
            string username_ln, string cat_ln, string age_ln, string ins_ln)
        {
            try
            {
                string n = _r.Next().ToString().Substring(0, 4);
                String pin = "NTF" + sname.Substring(0, 1) + othername.Substring(0, 1) + System.DateTime.UtcNow.Year.ToString().Substring(2) + n + GetUserCount();
                SqlParameter[] @params =
                {
                    new SqlParameter("@Pin", pin),
                    new SqlParameter("@Email", email_ln),
                    new SqlParameter("@Surname", sname),
                    new SqlParameter("@Othername", othername),
                    new SqlParameter("@DOB", dob_ln),
                    new SqlParameter("@Password", BLL.Encrypt(pass_ln)),
                    new SqlParameter("@Phone", phone_ln),
                    new SqlParameter("@Gender", gender_ln),
                    new SqlParameter("@Username", username_ln),
                    new SqlParameter("@CategoryName", cat_ln),
                    new SqlParameter("@AgeGroup", age_ln),
                    new SqlParameter("@InstitutionName", ins_ln)
                };

                DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "[sp_AddPlayer]", @params);
                String val = ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                if (val == "1")
                {
                    //String message_ln = "Hello, " + sname + " " + othername + ". You have sucessfully registered into the Nigeria Tennis Federation" + Environment.NewLine +
                    //"Here is your NTF pin :" + pin + ". Please keep it safe. You are welcome";
                    string message_ln = PopulateBody(sname, othername, pin, email_ln, cat_ln, username_ln, gender_ln, phone_ln);
                    BLL.SendEmail(email_ln, "NTF Registration", message_ln);
                }
                Context.Response.Write(val);


            }
            catch (Exception ex)
            {
                //Context.Response.Write(BLL.OutPut_Message("-1", ex.Message));
            }

        }

        [WebMethod]
        public void AddCoachOrUmpire(string sname, string othername, string email_ln,
          string phone_ln, string gender_ln,
          string dob_ln, string pass_ln,
          string username_ln, string cert_ln, string type_ln)
        {
            try
            {
                String pin = "";
                string n = _r.Next().ToString().Substring(0, 4);
                if (type_ln == "Umpire")
                {
                    pin = "U_NTF" + sname.Substring(0, 1) + othername.Substring(0, 1) + System.DateTime.UtcNow.Year.ToString().Substring(2) + n + GetCoachCount();
                }
                else
                {
                    pin = "C_NTF" + sname.Substring(0, 1) + othername.Substring(0, 1) + System.DateTime.UtcNow.Year.ToString().Substring(2) + n + GetCoachCount();
                }
                SqlParameter[] @params =
                {
                    new SqlParameter("@Pin", pin),
                    new SqlParameter("@Email", email_ln),
                    new SqlParameter("@Surname", sname),
                    new SqlParameter("@Othername", othername),
                    new SqlParameter("@DOB", dob_ln),
                    new SqlParameter("@Password", BLL.Encrypt(pass_ln)),
                    new SqlParameter("@Phone", phone_ln),
                    new SqlParameter("@Gender", gender_ln),
                    new SqlParameter("@Username", username_ln),
                    new SqlParameter("@CertificateLevel", cert_ln),
                    new SqlParameter("@Type", type_ln)
                };

                DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "[sp_AddCoachOrUmpire]", @params);
                String val = ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                if (val == "1")
                {
                    //String message_ln = "Hello, " + sname + " " + othername + ". You have sucessfully registered into the Nigeria Tennis Federation" + Environment.NewLine +
                    //"Here is your NTF pin :" + pin + ". Please keep it safe. You are welcome";
                    string message_ln = PopulateBody(sname, othername, pin, email_ln, "", username_ln, gender_ln, phone_ln);
                    BLL.SendEmail(email_ln, "NTF Registration", message_ln);
                }
                Context.Response.Write(val);


            }
            catch (Exception ex)
            {
                //Context.Response.Write(BLL.OutPut_Message("-1", ex.Message));
            }

        }

        

        [WebMethod]
        public void AddNews(string title, string content, string author, string img, string intro)
        {
            try
            {
                SqlParameter[] @params =
                {
                    new SqlParameter("@NewsTitle", title),
                    new SqlParameter("@NewsContent", content),
                    new SqlParameter("@NewsAuthor", author),
                    new SqlParameter("@NewsImage", img),
                    new SqlParameter("@NewsIntro", intro)
                };

                DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "[sp_AddNews]", @params);
                //String val = ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                Context.Response.Write(BLL.OutPut_Message(ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString(), ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString()));


            }
            catch (Exception ex)
            {
                //Context.Response.Write(BLL.OutPut_Message("-1", ex.Message));
            }

        }

        [WebMethod]
        public void AddEvent(string name,string date_ln, string prize, string venue, string sponsor)
        {
            try
            {
                //string date_ln = System.DateTime.UtcNow.ToShortDateString();
                SqlParameter[] @params =
                {
                    new SqlParameter("@Name", name),
                    new SqlParameter("@StartDAATE", date_ln),
                    new SqlParameter("@Prize", prize),
                    new SqlParameter("@Venue", venue),
                    new SqlParameter("@Sponsor", sponsor)
                };

                DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "[sp_AddEvent]", @params);
                Context.Response.Write(BLL.OutPut_Message(ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString(), ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString()));


            }
            catch (Exception ex)
            {
                //Context.Response.Write(BLL.OutPut_Message("-1", ex.Message));
            }

        }

        [WebMethod]
        public void GetNews(string type)
        {
            try
            {
                SqlParameter[] @params =
                {
                    new SqlParameter("@type", type)
                };
                Context.Response.Write(BLL.DataTableToJSONArray(SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "[sp_GetNews]",@params).Tables[0]));
            }
            catch (Exception ex)
            {
                Context.Response.Write("");
            }

        }
        [WebMethod]
        public void GetEvents()
        {
            try
            {
                DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "[sp_GetEvents]");
                String Janexp = BLL.DataTableToJSONArray(ds.Tables[0]);
                String FebExp = BLL.DataTableToJSONArray(ds.Tables[1]);
                String MarExp = BLL.DataTableToJSONArray(ds.Tables[2]);
                String AprExp = BLL.DataTableToJSONArray(ds.Tables[3]);
                String MayExp = BLL.DataTableToJSONArray(ds.Tables[4]);
                String JunExp = BLL.DataTableToJSONArray(ds.Tables[5]);
                String JlyExp = BLL.DataTableToJSONArray(ds.Tables[6]);
                String AugExp = BLL.DataTableToJSONArray(ds.Tables[7]);
                String SepExp = BLL.DataTableToJSONArray(ds.Tables[8]);
                String OctExp = BLL.DataTableToJSONArray(ds.Tables[9]);
                String NovExp = BLL.DataTableToJSONArray(ds.Tables[10]);
                String DecExp = BLL.DataTableToJSONArray(ds.Tables[11]);
                Context.Response.Write(Janexp + "=" + FebExp + "=" + MarExp + "=" + AprExp + "=" + MayExp + "=" + JunExp + "=" + JlyExp + "=" + AugExp + "=" +
                    SepExp + "=" + OctExp + "=" + NovExp + "=" + DecExp);
            }
            catch (Exception ex)
            {
                Context.Response.Write("");
            }

        }
        

        [WebMethod]
        public void GetNewsDetails(string title)
        {
            try
            {
                SqlParameter[] @params =
                {
                    new SqlParameter("@title", title)
                };
                string val = BLL.DataTableToJSONArray(SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "[sp_GetNewsDetails]", @params).Tables[0]);
                Context.Response.Write(val);
            }
            catch (Exception ex)
            {
                Context.Response.Write("");
            }

        }

        public int GetUserCount()
        {
            return int.Parse(SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "[sp_GetUserCount]").Tables[0].Rows[0]["count"].ToString()) + 1;

        }
        public int GetCoachCount()
        {
            return int.Parse(SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "[sp_GetOfiicialCount]").Tables[0].Rows[0]["count"].ToString()) + 1;

        }
        public DataSet DashBoard()
        {
            return SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "[sp_DashBoard]");

        }
        public string PopulateBody(string sname, string oname, string npin, string email, string category, string uname, string gender, string phone)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplates/Welcome.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{sname}", sname);
            body = body.Replace("{onames}", oname);
            body = body.Replace("{npin}", npin);
            body = body.Replace("{email}", email);
            body = body.Replace("{category}", category);
            body = body.Replace("{uname}", uname);
            body = body.Replace("{gender}", gender);
            body = body.Replace("{phone}", phone);
            return body;
        }
    }

}
