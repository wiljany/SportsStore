using System;
using System.Collections.Generic;
using System.Linq;
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
			public string ServerPort = "587";
			public bool WriteAsFile = true;
			public string FileLocation = @"c:\sport_store_emails";
		}
	}
}
