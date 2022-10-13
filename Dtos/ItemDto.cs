using System;


namespace Catalog.Dtos
{
     public record ItemDto
    {
        //init use a creator expression to contruct this item object, you can no longer modify the init property
        public Guid Id {get; init;}
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
        public string Address { get; init; }
        public string ZipCode { get; init; }
        public DateTimeOffset CreatedDate {get; init;}

    }


}

