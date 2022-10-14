using System.ComponentModel.DataAnnotations;

namespace Catalog.Dtos 
{
    public record CreateItemDto
    {
       [Display(Name = "First Name")]
       [Required(ErrorMessage = "First Name is Required")]
       public string FirstName { get; init; }

       [Required]
       public string LastName { get; init; }

       public string Email { get; init; }
       public string PhoneNumber { get; init; }
       [MaxLength(180)] 
       public string Address { get; init; }
       [MinLength(5, ErrorMessage = "Zip Code must be 5 numbers.")]
       public string ZipCode { get; init; }
      
    }

}