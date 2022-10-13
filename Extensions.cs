using Catalog.Dtos;
using Catalog.Entities;

namespace Catalog
{
    public static class Extensions 
    {
        public static ItemDto AsDto(this Item item)
        {
           return new ItemDto
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Email = item.Email,
                PhoneNumber = item.PhoneNumber,
                Address = item.Address,
                ZipCode = item.ZipCode,               
                CreatedDate = item.CreatedDate
            };
        }
    }

}