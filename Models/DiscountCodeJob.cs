using System;

namespace DiscountCodes.Models
{
    public class DiscountCodeJob
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public Guid StoreId { get; set; }
        public List<string> Codes { get; set; }
    }
}
