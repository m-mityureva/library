using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public virtual void AddItem(Book book, int quantity)
        {
            CartLine line = lineCollection
                .Where(l => l.Book.Id == book.Id)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Book = book,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Book book) => lineCollection.RemoveAll(l => l.Book.Id == book.Id);
        public virtual int ComputeTotalQuantity() => lineCollection.Sum(l => l.Quantity);
        public virtual void Clear() => lineCollection.Clear();
        public virtual IEnumerable<CartLine> Lines => lineCollection;
        
    }
}
