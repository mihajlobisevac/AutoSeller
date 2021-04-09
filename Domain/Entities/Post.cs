using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Post : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public Vehicle Vehicle { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
