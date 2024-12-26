using BookSession.Models;
using BookSession.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookSession.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public BookingController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Create a new bookinh
        public IActionResult Book()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Book(EventDetailsDto eventDetailsDto)
        {
            if (!ModelState.IsValid)
            {
                return View(eventDetailsDto);
            }

            var userName = "";

            if (signInManager.IsSignedIn(User))
            {
                userName =  User.Identity!.Name!;
            }

            //Creating a booking
            EventDetails eventDetail = new EventDetails();
            eventDetail.EventCategory = eventDetailsDto.EventCategory;
            eventDetail.EventDate = eventDetailsDto.EventDate;
            eventDetail.EventPayment = eventDetailsDto.EventPayment;
            eventDetail.EventDescription = eventDetailsDto.EventDescription;
            eventDetail.EventStatus = "Pending Approval"; //By default we have this
            eventDetail.EventOwner = userName;
            eventDetail.CreatedAt = DateTime.Now;

            context.Events.Add(eventDetail);
            context.SaveChanges();

            return RedirectToAction("BookingStatus", "Booking");
        }

        [Authorize]
        public IActionResult BookingStatus()
        {
            // Check the role of the logged-in user
            var userRole = User.IsInRole("admin"); 
            var userId = User.Identity!.Name; 

            List<EventDetails> bookings;

            if (userRole)
            {
                // If the user is an admin, fetch all bookings
                bookings = context.Events.OrderByDescending(x => x.Id).ToList();
            }
            else
            {
                // If the user is a client, fetch only their bookings
                
                bookings = context.Events
                    .Where(x => x.EventOwner == userId) // Adjust this to match your data model
                    .OrderByDescending(x => x.Id)
                    .ToList();
                
            }

            return View(bookings);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var details = context.Events.Find(id);

            if (details == null)
            {
                return RedirectToAction("BookingStatus", "Booking");
            }

            EventDetailsDto eventDetailsDto = new EventDetailsDto()
            {
                EventCategory = details.EventCategory,
                EventDate = details.EventDate,
                EventPayment = details.EventPayment,
                EventDescription = details.EventDescription,
            };

            return View(eventDetailsDto);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, EventDetailsDto eventDetailsDto)
        {
            var bookedEvent = context.Events.Find(id);

            if (bookedEvent == null)
            {
                return RedirectToAction("BookingStatus", "Booking");
            }

            bookedEvent.EventCategory = eventDetailsDto.EventCategory;
            bookedEvent.EventDate = eventDetailsDto.EventDate;
            bookedEvent.EventPayment = eventDetailsDto.EventPayment;
            bookedEvent.EventDescription = eventDetailsDto.EventDescription;
            bookedEvent.EventStatus = "Pending Approval";
            bookedEvent.CreatedAt = DateTime.Now;

            context.SaveChanges();

            return RedirectToAction("BookingStatus", "Booking");
        }

        [Authorize]
        public IActionResult CompleteEvent(int id)
        {
            var booking = context.Events.Find(id);

            if (booking == null)
            {
                return RedirectToAction("BookingStatus", "Booking");
            }

            booking.EventStatus = "Completed";
            context.SaveChanges();

            return RedirectToAction("BookingStatus", "Booking");
        }

        [Authorize]
        //This action needs to be available to only the user change it after adding user roles
        public IActionResult Cancel(int id)
        {
            var booking = context.Events.Find(id);

            if (booking == null)
            {
                return RedirectToAction("BookingStatus", "Booking");
            }

            context.Events.Remove(booking);
            context.SaveChanges();

            return RedirectToAction("BookingStatus", "Booking");
        }
    }
}
