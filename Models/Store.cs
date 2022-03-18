using System;

namespace DiscountCodes.Models
{
    public class Store
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Guid UserId { get; set; }
        public string ShortName { get; set; }
    }
}
