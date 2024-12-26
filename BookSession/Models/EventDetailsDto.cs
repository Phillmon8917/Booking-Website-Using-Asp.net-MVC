using BookSession.ClientSideValidater;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BookSession.Models
{
    public class EventDetailsDto
    {
        public string EventCategory { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please Provide An Event Date")]
        [FutureDate]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Please provide Amount You Want to Pay"), Precision(16, 2)]
        public double EventPayment { get; set; }

        public string EventStatus { get; set; } = "";

        [Required(ErrorMessage = "Please provide a description about your event")]
        public string EventDescription { get; set; } = "";
    }
}
