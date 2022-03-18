using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace DiscountCodes.DTOs
{
    public class DiscountCodeCreateDTO
    {
        public Guid id { get; set; }
        public Guid StoreId { get; set; }
        public List<String> Codes { get; set; }

        [DefaultValue("")]
        public List<String>? CodesCreated { get; set; }

        [DefaultValue("")]
        public List<String>? CodesFailed { get; set; }
    }
}
