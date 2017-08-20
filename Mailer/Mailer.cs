using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriHood.Models;
using Newtonsoft.Json.Linq;
using PriHood.Auth;
using System.Security.Cryptography;
using System.Text;
using FluentEmail.Core;

namespace PriHood.Mailer
{
  public class Mailer
  {
    // Variable estática para la instancia, se necesita utilizar una función lambda ya que el constructor es privado
    private static readonly Lazy<Mailer> instance = new Lazy<Mailer>(() => new Mailer());

    // Constructor privado para evitar la instanciación directa
    private Mailer()
    {
    }

    // Propiedad para acceder a la instancia
    public static Mailer Instance
    {
      get
      {
        return instance.Value;
      }
    }
    public void SendEmail(string From, string To, string Subject, string Body)
    {
      var email = Email
                  .From(From)
                  .To(To)
                  .Subject(Subject)
                  .Body(Body);

      email.Send();
    }
  }
}