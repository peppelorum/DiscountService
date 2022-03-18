using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace DiscountCodes.DTOs
{
    public class DiscountCodeCreateDTO
    {
        public Guid StoreId { get; set; }
        public int NumberOfCodes { get; set; }
    }
}
