using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class SaleDTO
    {
        public int Id { get; set; }
        public int IdBuyer { get; set; }
        public string BuyerName { get; set; }
        public int IdSeller { get; set; }
        public string SellerName { get; set; }
        public int IdProduct { get; set; }
        public string ProductName { get; set; }
        public DateTime Date { get; set; }
    }
}
