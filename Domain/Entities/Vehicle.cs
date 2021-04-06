using Domain.Enums;

namespace Domain.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public string Engine { get; set; }
        public Drivetrain Drivetrain { get; set; }
        public Transmission Transmission { get; set; }
        public BodyStyle Body { get; set; }
        public string Equipment { get; set; }
        public Model Model { get; set; }
    }
}
