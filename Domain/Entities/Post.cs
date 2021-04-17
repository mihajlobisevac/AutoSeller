using Domain.Common;
using Domain.Enums;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Post : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public string Engine { get; set; }
        public Drivetrain Drivetrain { get; set; }
        public Transmission Transmission { get; set; }
        public BodyStyle Body { get; set; }
        public string Equipment { get; set; }
        public Model Model { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
