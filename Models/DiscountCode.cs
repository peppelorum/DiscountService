using System;

namespace DiscountCodes.Models
{
    public class DiscountCode
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public Guid StoreId { get; set; }
        public string Code { get; set; }
    }
}
