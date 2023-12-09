﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace H1Store.Catalogo.Infra.EmailService
{
	public static class Email
	{
		public static void Enviar(string assunto, string corpoemail, string destinatario, EmailConfig emailConfig)
		{

			var client = new SmtpClient(emailConfig.Host, 2525)
			{
				Credentials = new NetworkCredential(emailConfig.Usuario, emailConfig.Senha),
				EnableSsl = true
			};
			client.Send("no-reply@example.com",destinatario,assunto, corpoemail);
		}
	}
}
