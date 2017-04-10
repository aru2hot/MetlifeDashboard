using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

public partial class Email_Send : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void SendEmail(object sender, EventArgs e)
    {
        using (MailMessage mm = new MailMessage(txtEmail.Text, txtTo.Text))
        {
            mm.Subject = txtSubject.Text;
            mm.Body = txtBody.Text;
            if (fuAttachment.HasFile)
            {
                string FileName = Path.GetFileName(fuAttachment.PostedFile.FileName);
                mm.Attachments.Add(new Attachment(fuAttachment.PostedFile.InputStream, FileName));
            }
            mm.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.live.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential(txtEmail.Text, txtPassword.Text);
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mm);
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);
        }

        //try
        //{
        //    MailMessage mailMessage = new MailMessage();
        //    mailMessage.To.Add("KarthikeyanNS.SrinivasanNK@cognizant.com");
        //    mailMessage.From = new MailAddress("KarthikeyanNS.SrinivasanNK@cognizant.com");
        //    mailMessage.Subject = "ASP.NET e-mail test";
        //    mailMessage.Body = "Hello world,\n\nThis is an ASP.NET test e-mail!";
        //    SmtpClient smtpClient = new SmtpClient("smtp.your-isp.com");
        //    smtpClient.Send(mailMessage);
        //    Response.Write("E-mail sent!");
        //}
        //catch (Exception ex)
        //{
        //    Response.Write("Could not send the e-mail - error: " + ex.Message);
        //}


        //string to = "KarthikeyanNS.SrinivasanNK@cognizant.com"; //To address    
        //string from = "KarthikeyanNS.SrinivasanNK@cognizant.com"; //From address    
        //MailMessage message = new MailMessage(from, to);

        //string mailbody = "In this article you will learn how to send a email using Asp.Net & C#";
        //message.Subject = "Sending Email Using Asp.Net & C#";
        //message.Body = mailbody;
        //message.BodyEncoding = Encoding.UTF8;
        //message.IsBodyHtml = true;
        //SmtpClient client = new SmtpClient("smtp.live.com", 587); //Gmail smtp    
        //System.Net.NetworkCredential basicCredential1 = new
        //System.Net.NetworkCredential("KarthikeyanNS.SrinivasanNK@cognizant.com", "");
        //client.EnableSsl = true;
        //client.UseDefaultCredentials = false;
        //client.Credentials = basicCredential1;
        //try
        //{
        //    client.Send(message);
        //}

        //catch (Exception ex)
        //{
        //    Response.Write(ex);
        //    //throw ex;
        //}  
    }
}