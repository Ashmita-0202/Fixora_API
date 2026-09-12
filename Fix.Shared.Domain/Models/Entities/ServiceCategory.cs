using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fix.Shared.Domain.Primitive;

namespace Fix.Shared.Domain.Models.Entities
{
    public class ServiceCategory:Entity
    {
        public ServiceCategory(
           string id,
           string categoryName,
           string? description,
           string? iconUrl,
           decimal baseServiceCharge,
           string? estimatedDuration,
           bool isActive
           ) : base(id)
        {
            CategoryName = categoryName;
            Description = description;
            IconUrl = iconUrl;
            BaseServiceCharge = baseServiceCharge;
            EstimatedDuration = estimatedDuration;
            IsActive = isActive;
            //Technicians = new List<Technician>();
            //Bookings = new List<Booking>();
        }

        public string CategoryName { get; set; }
        public string? Description { get; set; }
        public string? IconUrl { get; set; }
        public decimal BaseServiceCharge { get; set; }
        public string? EstimatedDuration { get; set; }
        public bool IsActive { get; set; }

       

        //public ICollection<Technician> Technicians { get; set; }
        //public ICollection<Booking> Bookings { get; set; }
    }
}
