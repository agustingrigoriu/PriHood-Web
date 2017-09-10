using System;
using Microsoft.AspNetCore.Mvc;
using PriHood.Models;
using Microsoft.EntityFrameworkCore;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MimeKit.Utils;

namespace PriHood.Auth
{
  public class EmailService
  {
    private PrihoodContext _db;
    public EmailService(PrihoodContext db)
    {
      this._db = db;
    }

    public void SendEmailPwdChanged(string To, string msg)
    {
      var message = new MimeMessage();
      message.From.Add(new MailboxAddress("PriHood", "no-reply@prihood.com"));
      message.To.Add(new MailboxAddress(To));
      message.Subject = "PriHood - Cambio de Contraseña";

      var builder = new BodyBuilder();

      // Set the plain-text version of the message text
      builder.TextBody = String.Format(@"Estimado Usuario, 

           Gracias por comunicarse con nosotros para solicitarnos una nueva contraseña.

           Hemos generado la siguiente contraseña para usted: {0}

           Si desea cambiarla por una que le resulte más sencilla de recordar, 
           puede hacerlo luego de haber ingresado al sistema con su nombre de usuario y  
           la nueva contraseña. 

           Cordialmente,

            El equipo de PriHood
          ", msg);

      // Set the html version of the message text
      builder.HtmlBody = String.Format(@"<!DOCTYPE html><html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /></head>
      <body style=""font-family: sans-serif;""><p style=""text-align:left;""></p>
      <b>Estimado Usuario,</b><p>Gracias por comunicarse con nosotros para solicitarnos una nueva contraseña.</p> 
      <p>Hemos generado la siguiente contraseña para usted: <div style=""color: blue;"" >{0}</div></p> 
      <p>Si desea cambiarla por una que le resulte más sencilla de recordar, 
      puede hacerlo luego de haber ingresado al sistema con su nombre de usuario y  
      la nueva contraseña. 
      </p> 
      <p style=""text-align:left;""><a href=""http://prihood.com/Login"" style=""background-color: #003777;color: white;padding: 14px 25px;text-align: center; text-decoration: none;display: inline-block;"">Inicie Sesión</a></p> 
      <p> Cordialmente,<br><span style=""color:#222222;""><i>El equipo de PriHood</i></span> 
      </body> 
      </html>", msg);




      // Now we just need to set the message body and we're done
      message.Body = builder.ToMessageBody();

      SendEmail(message);
    }

    private void SendEmail(MimeMessage message)
    {
      using (var client = new SmtpClient())
      {
        // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
        client.ServerCertificateValidationCallback = (s, c, h, e) => true;

        client.Connect("smtppro.zoho.com", 465, true);

        // Note: since we don't have an OAuth2 token, disable
        // the XOAUTH2 authentication mechanism.
        client.AuthenticationMechanisms.Remove("XOAUTH2");

        // Note: only needed if the SMTP server requires authentication
        client.Authenticate("no-reply@prihood.com", "prihood2017");

        client.Send(message);
        client.Disconnect(true);
      }
    }
  }
}