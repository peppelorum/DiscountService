using System;

namespace DiscountCodes.Models
{
    public class DiscountUse
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Used { get; set; }
        public Guid UserId { get; set; }
        public Guid DiscountCodeId { get; set; }
    }
}
