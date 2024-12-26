using BookSession.Models;
using BookSession.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookSession.Controllers
{
    [Authorize]
    public class ManageBookingsController : Controller
    {
        private readonly ApplicationDbContext context;
        public ManageBookingsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Dashboard()
        {
            var events = context.Events.OrderByDescending(e => e.Id).ToList();
            return View(events);
        }

        public IActionResult AcceptBooking(int id)
        {
            var booking = context.Events.Find(id);

            if (booking == null)
            {
                return RedirectToAction("Dashboard", "ManageBookings");
            }

            booking.EventStatus = "Approved";
            context.SaveChanges();

            return RedirectToAction("BookingStatus", "Booking");
        }

        public IActionResult RejectBooking(int id)
        {
            var booking = context.Events.Find(id);

            if ( booking == null )
            {
                return RedirectToAction("Dashboard", "ManageBookings");
            }

            booking.EventStatus = "Rejected";
            context.SaveChanges();

            return RedirectToAction("BookingStatus", "Booking");
        }

        public IActionResult Edit(int id)
        {
            var booking = context.Events.Find(id);

            if (booking == null)
            {
                return RedirectToAction("Dashboard", "ManageBookings");
            }

            EventDetailsDto detailsDto = new EventDetailsDto()
            {
                EventCategory = booking.EventCategory,
                EventPayment = booking.EventPayment
            };

            ViewData["ProductId"] = booking.Id;
            return View(detailsDto);
        }

        [HttpPost]
        public IActionResult Edit(int id, EventDetailsDto eventDetails)
        {
            var booking = context.Events.Find(id);

            if (booking == null)
            {
                return RedirectToAction("Dashboard", "ManageBookings");
            }

            booking.EventPayment = eventDetails.EventPayment;
            
            context.SaveChanges();

            return RedirectToAction("Dashboard", "ManageBookings");
        }

        public IActionResult DeleteBooking(int id)
        {
            var booking = context.Events.Find(id);

            if (booking == null)
            {
                return RedirectToAction("Dashboard", "ManageBookings");
            }

            //Delete a booking
            context.Events.Remove(booking);
            context.SaveChanges();

            return RedirectToAction("Dashboard", "ManageBookings");
        }

        public IActionResult DatesBooked(int? month, int? year)
        {
            // Use current month and year if not provided
            var currentMonth = month ?? DateTime.Now.Month;
            var currentYear = year ?? DateTime.Now.Year;

            // Retrieve all booked dates from the database
            var bookings = context.Events.Select(e => e.EventDate.Date).ToList();

            // Pass the current month, year, and bookings to the view
            ViewBag.CurrentMonth = currentMonth;
            ViewBag.CurrentYear = currentYear;
            ViewBag.BookedDates = bookings;

            return View();
        }
    }
}
