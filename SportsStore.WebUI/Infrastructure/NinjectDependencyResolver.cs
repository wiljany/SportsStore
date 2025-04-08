using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
// using Ninject;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using Moq;
using SportsStore.Domain.Concrete;
using System.Configuration;
using static SportsStore.Domain.Concrete.EmailOrderProcessor;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Infrastructure.Concrete;

namespace SportsStore.WebUI.Infrastructure
{
	public class NinjectDependencyResolver : IDependencyResolver
	{
		private IKernel mykernel;

		public NinjectDependencyResolver(IKernel kernelParam)
		{
			mykernel = kernelParam;
			AddBinding();
		}

		public object GetService(Type myserviceType)
		{
			return mykernel.GetService(myserviceType);
		}

		public IEnumerable<object> GetServices(Type myserviceType)
		{
			return mykernel.GetAll(myserviceType);
		}

		private void AddBinding()
		{
			//Mock<IProductsRepository> myMock = new Mock<IProductsRepository>();
			//myMock.Setup(m => m.Products).Returns(new List<Product>
			//{
			//	new Product{ Name = "Football", Price = 25, Description = "This is a Team Sport" },
			//	new Product{ Name = "Surf board", Price = 179, Category = "Water sports" },
			//	new Product{ Name = "Running shoes", Price = 95, Category = "Athletic" }
			//});

			//mykernel.Bind<IProductsRepository>().ToConstant(myMock.Object);

			mykernel.Bind<IProductsRepository>().To<EFProductRepository>();
			EmailSettings emailSettings = new EmailSettings();
			{
				bool WriteAsFile = bool.Parse
								(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false");
			};
			mykernel.Bind<IOrderProcessor>()
				.To<EmailOrderProcessors>()
				.WithConstructorArgument("settings", emailSettings);

			mykernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
		}
	}
}