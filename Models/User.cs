using System;

namespace DiscountCodes.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public String? Name { get; set; }
        public String? Email { get; set; }
    }
}
