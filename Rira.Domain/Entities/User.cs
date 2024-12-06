using CleanArchitecture.Domain.Common;

namespace Rira.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public DateTime BirthDate { get; set; }

        public void Update(string firstName, string lastName, string nationalCode, DateTime birthDate) 
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.NationalCode = nationalCode;
            this.BirthDate = birthDate;
        }

        public void Delete()
        {
            this.IsDeleted = true;
        }
    }
}
