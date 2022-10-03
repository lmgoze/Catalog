using System;

namespace Catalog.Entities
{
    public record Item
    {
        //init use a creator expression to contruct this item object, you can no longer modify the init property
        public Guid Id {get; init;}
        public string Name { get; init; }
        
        public decimal Price { get; init; }

        public DateTimeOffset CreatedDate {get; init;}

    }
}