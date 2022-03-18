using System;

namespace DiscountCodes.DTOs
{
    public class DiscountCodeDTO
    {
        public Guid Id { get; set; }
        public Guid StoreId { get; set; }
        public string? Code { get; set; }
    }
}
