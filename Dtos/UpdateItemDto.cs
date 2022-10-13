using System.ComponentModel.DataAnnotations;

namespace Catalog.Dtos 
{
    public record UpdateItemDto
    {
       [Required]
       public string FirstName { get; init; }

       [Required]
       public string LastName { get; init; } 
       public string Email { get; init; }
       public string PhoneNumber { get; init; }
       public string Address { get; init; }
       public string ZipCode { get; init; }
    }

}