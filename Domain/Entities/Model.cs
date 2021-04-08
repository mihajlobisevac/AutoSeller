using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Model : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Brand Brand { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
