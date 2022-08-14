using System;
using System.ComponentModel.DataAnnotations;

namespace SevenWestMedia.Common.Models
{
    public class User
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [Range(0, 150)]
        public int Age { get; set; }
        [StringLength(1, MinimumLength = 1, ErrorMessage = "The Gender must be 1 characters.")]
        [RegularExpression("M|F", ErrorMessage = "The Gender must be either 'M' or 'F' only.")]
        public string Gender { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, FirstName: {FirstName}, LastName: {LastName}, Age: {Age}, Gender: {Gender}";
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User) obj);
        }
        
        protected bool Equals(User other)
        {
            return Id == other.Id && FirstName == other.FirstName && LastName == other.LastName && Age == other.Age && Gender == other.Gender;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FirstName, LastName, Age, Gender);
        }
    }
}