using E_Prescribing_API.CollectionModel;
using E_Prescribing_API.Data.Services;
using E_Prescribing_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace E_Prescribing_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        private readonly ILogger<AdminController> _logger;

        public AdminController(ApplicationDbContext db,ILogger<AdminController> logger)
        {
            _db = db;
            _logger = logger;
        }
        [HttpPost("AddProvince")]
        public async Task<IActionResult> AddProvince(Province model)
        {
            try
            {
                if(model == null || string.IsNullOrEmpty(model.Name))
                    return BadRequest("Invalid province");

                if (await _db.Provinces.AnyAsync(a => a.Name == model.Name))
                {
                    return BadRequest("A province with this name already exist");
                }

                var province = new Province
                {
                    Name = model.Name
                };
                _db.Provinces.Add(province);
                await _db.SaveChangesAsync();

                return Ok(new
                {
                    message = "Province added successfully.",
                    name = province.Name
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding province {ProvinceName}", model?.Name);
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPost("AddCity")]
        public async Task<IActionResult> AddCity(City model)
        {
            try
            {
                if (model == null || string.IsNullOrEmpty(model.Name))
                    return BadRequest("Invalid city");

                if (await _db.Cities.AnyAsync(a => a.Name == model.Name))
                {
                    return BadRequest("A city with this name already exist");
                }

                var city = new City
                {
                    Name = model.Name,
                    ProvinceId = model.ProvinceId
                };
                _db.Cities.Add(city);
                await _db.SaveChangesAsync();

                return Ok(new
                {
                    message = "City added successfully.",
                    name = city.Name
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding city {Name}", model?.Name);
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPost("AddSuburb")]
        public async Task<IActionResult> AddSuburb(Suburb model)
        {
            try
            {
                if (model == null || string.IsNullOrEmpty(model.Name))
                    return BadRequest("Invalid suburb");

                if (await _db.Suburbs.AnyAsync(a => a.Name == model.Name))
                {
                    return BadRequest("A city with this name already exist");
                }

                var suburb = new Suburb
                {
                    Name = model.Name,
                    PostalCode = model.PostalCode,
                    CityId = model.CityId
                };
                _db.Suburbs.Add(suburb);
                await _db.SaveChangesAsync();

                return Ok(new
                {
                    message = "Suburb added successfully.",
                    name = suburb.Name
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding suburb {Name}", model?.Name);
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPost("AddFacility")]
        public async Task<IActionResult> AddFacility(Facility model)
        {
            try
            {
                if (model == null || string.IsNullOrEmpty(model.Name))
                    return BadRequest("Invalid suburb");

                if (await _db.Facilities.AnyAsync(a => a.Name == model.Name))
                {
                    return BadRequest("A facility with this name already exist");
                }

                var facility = new Facility
                {
                    Name = model.Name,
                    AddressLine1 = model.AddressLine1,
                    AddressLine2 = model.AddressLine2,
                    ContactNumber = model.ContactNumber,
                    SuburbId = model.SuburbId,
                    FacilityTypeId = model.FacilityTypeId
                };
                _db.Facilities.Add(facility);
                await _db.SaveChangesAsync();

                return Ok(new
                {
                    message = "Facility added successfully.",
                    name = facility.Name
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding facility {Name}", model?.Name);
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }


    }
}
