using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities
{
	public class CartLine
	{
		public Product Product { get; set; }
		public int Quantity { get; set; }

	}

	public class Cart
	{
		private List<CartLine> lineCollection = new List<CartLine>();
		public IEnumerable<CartLine> Lines
		{
			get { return lineCollection; }
		}

		// add item
		public void AddItem(Product myProduct, int myQuantity)
		{
			CartLine line = lineCollection
				.Where(p => p.Product.ProductId == myProduct.ProductId)
				.FirstOrDefault();

			// add new item
			if (line == null)
			{
				lineCollection.Add(new CartLine
				{
					Product = myProduct,
					Quantity = myQuantity
				});
			}

			// if item exists in cart, increast qty
			else
			{
				line.Quantity += myQuantity; 
			}
		}

		// remove item
		public void RemoveLine(Product myProduct)
		{
			lineCollection.RemoveAll(l => l.Product.ProductId == myProduct.ProductId);
		}

		// compute total cost of items in cart
		public decimal ComputeTotalValue()
		{
			return lineCollection.Sum(e => e.Product.Price * e.Quantity);
		}

		// empty cart
		public void Clear()
		{
			lineCollection.Clear();
		}
	}
}
