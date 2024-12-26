using BookSession.ClientSideValidater;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BookSession.Models
{
    public class EventDetails
    {
        public int Id { get; set; }
        public string EventCategory { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        [Precision(16, 2)]
        public double EventPayment {  get; set; }
        public string EventDescription { get; set; } = "";
        public string EventStatus { get; set; } = "";

        public string EventOwner { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}
