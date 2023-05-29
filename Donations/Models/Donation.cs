namespace Donations.Models
{
    public class Donation
    {
        //This would be our Donations domain model.
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BloodGroup { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public string Address { get; set; }

        //After this we need to create a class file that would act like a middle man towards the database
        //As we are using entity framewok core we can use DBContext,create a new folder called Data and load a class
    }
}
