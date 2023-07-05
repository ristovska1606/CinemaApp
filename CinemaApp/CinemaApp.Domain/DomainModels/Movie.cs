using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.Domain.DomainModels
{
    public class Movie : BaseEntity
    {
        public string MovieTitle { get; set; }
        public string MovieCover { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
