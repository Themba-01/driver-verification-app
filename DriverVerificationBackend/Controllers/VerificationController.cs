using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using DriverVerificationBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace DriverVerificationBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VerificationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            try
            {
                var name = Request.Form["name"];
                var surname = Request.Form["surname"];
                var orderNumber = Request.Form["orderNumber"];
                var vehicleRegistration = Request.Form["vehicleRegistration"];
                var pickupTime = DateTime.Parse(Request.Form["pickupTime"]);
                var selfie = Request.Form.Files["selfie"];

                if (selfie != null && selfie.Length > 0)
                {
                    // Save the selfie file
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", $"{Guid.NewGuid()}.jpg");

                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await selfie.CopyToAsync(stream);
                    }

                    // Store file path in the database
                    var verification = new Verification
                    {
                        Name = name,
                        Surname = surname,
                        OrderNumber = orderNumber,
                        VehicleRegistration = vehicleRegistration,
                        PickupTime = pickupTime,
                        SelfieFilePath = filePath
                    };

                    _context.Verifications.Add(verification);
                    await _context.SaveChangesAsync();
                }

                return Ok("Form submitted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
