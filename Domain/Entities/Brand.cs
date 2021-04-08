using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Brand : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public ICollection<Model> Models { get; set; }
    }
}
