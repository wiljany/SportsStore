﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete
{
	public class EmailOrderProcessor
	{
		public class EmailSettings
		{
			public string MailToAddress = "orders@nel.com";
			public string MailFromAddress = "admin@SportsStore.com";
			public bool UseSsl = true;
			public string Username = "MySmtpUsername";
			public string Password = "MySmtpPassword";
			public string ServerName = "Smtp.SportsStore.com";
			public int ServerPort = 587;
			public bool WriteAsFile = true;
			public string FileLocation = @"c:\sport_store_emails";
		}

		public class EmailOrderProcessors : IOrderProcessor
		{ 
			private EmailSettings emailSettings;

			public EmailOrderProcessors(EmailSettings settings)
			{
				emailSettings = settings;
			}

			public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
			{
				using (var smtpClient = new SmtpClient())
				{
					smtpClient.EnableSsl = emailSettings.UseSsl;
					smtpClient.Host = emailSettings.ServerName;
					smtpClient.Port = emailSettings.ServerPort;
					smtpClient.UseDefaultCredentials = false;
					smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

					if (emailSettings.WriteAsFile)
					{
						smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
						smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
						smtpClient.EnableSsl = false;
					}

					StringBuilder body = new StringBuilder()
					.AppendLine("A new order has been submitted.")
					.AppendLine("---------------------")
					.Append("Items");

					foreach (var line in cart.Lines)
					{
						var subtotal = line.Product.Price * line.Quantity;
						body.AppendFormat("{0} X {1} (subtotal: {2:c}", line.Quantity, line.Product.Name, subtotal);
					}

					body.AppendFormat("Total order value: {0:c}", cart.ComputeTotalValue())
						.AppendLine("-------------------")
						.AppendLine("Ship to:")
						.AppendLine(shippingInfo.Name)
						.AppendLine(shippingInfo.Line1)
						.AppendLine(shippingInfo.Line2 ?? "")
						.AppendLine(shippingInfo.Line3 ?? "")
						.AppendLine(shippingInfo.City)
						.AppendLine(shippingInfo.State)
						.AppendLine(shippingInfo.Country)
						.AppendLine(shippingInfo.zip)
						.AppendLine("-------------------")
						.AppendFormat("Gift wrap: {0}", shippingInfo.GiftWrap ? "Yes" : "No");

					MailMessage mailMessage = new MailMessage(
						emailSettings.MailFromAddress,
						emailSettings.MailToAddress,
						"New Order Submitted!",
						body.ToString());

					if (emailSettings.WriteAsFile)
					{
						mailMessage.BodyEncoding = Encoding.ASCII;
					}
					smtpClient.Send(mailMessage);
				}
			}

			
		}
	}
}
