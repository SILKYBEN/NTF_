using System;
using System.Web;
using System.Web.UI.WebControls;

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.CSharp;
using System.Data;
using System.Net.Mail;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;

namespace NTF.AppCode
{
    public class BLL
    {
        public static string DataTableToJSONArray(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            return JSONString.ToString();
        }

        public static string DataTableToJSONObject(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
            }
            return JSONString.ToString();
        }

        public static bool GenerateReservationSummary(string reservationId_ln, string roomId_ln, string email_ln, string indate_ln, string inTime_ln, string outDate_ln, string outTime_ln)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
                sb.Append("<tr><td ><b>Company</b></td><td>Yankari Reserve</td></tr>");
                sb.Append("<tr><td ><b>Reservation ID</b></td><td>" + reservationId_ln + "</td></tr>");
                sb.Append("<tr><td ><b>Room ID</b></td><td>" + roomId_ln + "</td></tr>");
                sb.Append("<tr><td ><b>Check In Date</b></td><td>" + indate_ln + "</td></tr>");
                sb.Append("<tr><td ><b>Check In Time</b></td><td>" + inTime_ln + "</td></tr>");
                sb.Append("<tr><td ><b>Check out Date</b></td><td>" + outDate_ln + "</td></tr>");
                sb.Append("<tr><td ><b>Check out Time</b></td><td>" + outDate_ln + "</td></tr>");
                sb.Append("<tr><td ><b>Date Applied </b></td><td>" + System.DateTime.Now + "</td></tr>");
                if (SendEmail(email_ln, "Room Reservation Summary", sb.ToString()))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static bool SendEmail(string mailToSendTo, string subjectOfMail, string message)
        {
            bool status = false;
            try
            {
                MailMessage email = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                // set up the Gmail server
                smtp.EnableSsl = false;
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["mail"].ToString(), ConfigurationManager.AppSettings["password"].ToString());
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                email.IsBodyHtml = true;
                email.Subject = subjectOfMail; 
                email.From = new MailAddress(ConfigurationManager.AppSettings["mail"].ToString(), ConfigurationManager.AppSettings["displayName"].ToString());
                email.Body = message;
                email.To.Add(mailToSendTo.Trim());
                smtp.Send(email);
                status = true;
            }
            catch (Exception ex)
            {
               
            }
            return status;

        }
        public static string OutPut_Message(string Output_Number, string Display_Message, bool MultiMode = false, string Modal = "")
        {
            string Message = null;
            switch (Output_Number)
            {
                case "2627":
                    Message = "<div class='alert alert-block alert-danger fade in'>";
                    Message = Message + "<button type='button'class='close' onclick='' data-dismiss='alert'></button>";
                    Message = Message + "<h4 class='alert-heading'>Error!</h4>";
                    Message = Message + "<p>" + Display_Message + "</p></br>";
                    if (MultiMode == true)
                    {
                        Message = Message + "<a class='btn red' data-toggle='modal' href='#" + Modal + "' onclick=''>View Error Log</a>&nbsp;";
                    }
                    else
                    {
                        Message = Message + "<a class='btn red' href='#' onclick='BLL.Retry();'>Try Again</a>&nbsp;";
                    }
                    Message = Message + "<a class='btn blue' href='#' onclick='' data-dismiss='alert' >Cancel</a>";
                    Message = Message + "</div>";
                    break;
                case "1":
                    Message = "<div class='alert alert-block alert-success fade in'>";
                    Message = Message + "<button type='button' class='close' onclick='' data-dismiss='alert'></button>";
                    Message = Message + "<h4 class='alert-heading'>Success!</h4>";
                    Message = Message + "<p>" + Display_Message + "</p>";
                    Message = Message + "</div>";
                    break;
                case "0":
                    Message = "<div class='alert alert-block alert-warning fade in'>";
                    Message = Message + "<button type='button' class='close' onclick='' data-dismiss='alert'></button>";
                    Message = Message + "<h4 class='alert-heading'>Warning!</h4>";
                    Message = Message + "<p>" + Display_Message + "</p>";
                    Message = Message + "</div>";
                    break;
                case "-1":
                    Message = "<div class='alert alert-block alert-danger fade in'>";
                    Message = Message + "<button type='button' class='close' onclick='' data-dismiss='alert'></button>";
                    Message = Message + "<h4 class='alert-heading'>Error!</h4>";
                    Message = Message + "<p>" + Display_Message + "</p>";
                    Message = Message + "</div>";
                    break;
                default:
                    Message = "";
                    break;
            }
            return Message;
        }


        public static void ToDataTable(GridView grid)
        {
            if (grid.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                grid.UseAccessibleHeader = true;

                //This will add the <thead> and <tbody> elements
                grid.HeaderRow.TableSection = TableRowSection.TableHeader;

                //This adds the <tfoot> element. 
                //Remove if you don't have a footer row
                grid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }
        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public static string PopulateBody(string sname, string oname, string npin, string email, string category, string uname, string gender, string phone)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(/*Server.MapPath("~/EmailTemplate.htm")*/@"EmailTemplates/Welcome.html"))
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