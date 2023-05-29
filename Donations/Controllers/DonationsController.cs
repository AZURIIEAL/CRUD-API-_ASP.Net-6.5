using Donations.Data;
using Donations.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Xml.Linq;

namespace Donations.Controllers
{
    //As this is an API controller and not an MVC Controller we need to annotate first that its an API Controller.
    [ApiController]
    //Now we need a Route for our Api.
    [Route("APIs/[Controller]")]

    public class DonationsController : Controller
    {
        private readonly DonationsAPIDbContext dbContext;

        //We have all the required connections except a connection from the controller to the DB Context
        public DonationsController(DonationsAPIDbContext dbContext)
        {
            //We need a private property,to contact the inmmemory database
            this.dbContext = dbContext;
        }

        //We would be first making a post control as we dont have any data in our DB.
        //We need to annotate that this is a post by saying [HTTPPOST] as we are using swagger.
        [HttpPost]
        
        //We will need another model as a domain as we are taking inputs.
        public IActionResult PostData(AddDonationData addDonationData)
        {
            //Need to create a new Donation object
            var donationNEW = new Donation()
            {
                Id=Guid.NewGuid(), 
                Name=addDonationData.Name,
                BloodGroup=addDonationData.BloodGroup,
                Email=addDonationData.Email,
                Phone=addDonationData.Phone,
                Address = addDonationData.Address
            };
            dbContext.Add(donationNEW);
            return Ok(dbContext.SaveChanges());
        }
        [HttpGet]
        //Using Ienumerable to aoid the Ok wrapping while using IActionResult
        public IActionResult GetAllData()
        {
            return Ok(dbContext.Donations.ToList());
        }
        //Now to edit/Update the data,we can use Put for that.
        [HttpPut]
        //but we would require a ID here.
        [Route("{id:guid}")]

        public IActionResult UpdateDonations(Guid Id ,AddDonationData addDonationDataUpdation) 
        {
            var don2 = dbContext.Donations.Find(Id);
            if (don2 == null)
            {
                return NotFound();
            }
            else 
            { 
                don2.Name = addDonationDataUpdation.Name;
                don2.Email=addDonationDataUpdation.Email;
                don2.BloodGroup = addDonationDataUpdation.BloodGroup;
                don2.Phone=addDonationDataUpdation.Phone;
                don2.Address= addDonationDataUpdation.Address;
                dbContext.SaveChanges();
                return Ok(don2);
            }
        }
        //now for the deletion part
        [HttpDelete]
        public IActionResult DeleteDonations(Guid Id)
        {
            var dondele = dbContext.Donations.Find(Id);
            if(dondele == null)
            {
                return NotFound();
            }
            else 
            {
                dbContext.Donations.Remove(dondele);
                dbContext.SaveChanges();
                return Ok("Deleted");
            }
        }
        //Get a single response
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetDonation(Guid Id)
        {
            var donone = dbContext.Donations.Find(Id);
            if (donone== null)
            {
                return NotFound();
            }
            else 
            {
                return Ok(donone);
            }

        }
    }
}
