using System;
using Microsoft.AspNetCore.Mvc;
using PriHood.Models;
using Microsoft.EntityFrameworkCore;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;


namespace PriHood.Auth
{
  public class EmailService
  {
    private PrihoodContext _db;
    public EmailService(PrihoodContext db)
    {
      this._db = db;
    }

    public void SendEmail(string To, string Subject, string Msg)
    {
      var message = new MimeMessage();
      message.From.Add(new MailboxAddress("PriHood", "no-reply@prihood.com"));
      message.To.Add(new MailboxAddress(To));
      message.Subject = Subject;

      message.Body = new TextPart("plain")
      {
        Text = Msg
      };

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