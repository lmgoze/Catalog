using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controller
{
    [ApiController]
    [Route("items")]
    public class ItemController : ControllerBase
    {
        private readonly IItemsRepository repository;
         
        public ItemController(IItemsRepository repository) {
            this.repository = repository;
        }
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = (await repository.GetItemsAsync())
                        .Select(item => item.AsDto());
            return items;
        }

        // Get /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item = await repository.GetItemAsync(id);

            if (item is null)
            {
                return NotFound();
            }

            return item.AsDto();
        }
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                FirstName = itemDto.FirstName,
                LastName = itemDto.LastName,
                Email = itemDto.Email,
                PhoneNumber = itemDto.PhoneNumber,
                Address = itemDto.Address,
                ZipCode = itemDto.ZipCode,
                CreatedDate = DateTimeOffset.UtcNow 
            };

            await repository.CreateItemAsync(item);
            return CreatedAtAction(nameof(GetItemAsync), new {id = item.Id}, item.AsDto());
        }
        // PUT /items/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto )
        {
           var existingItem = await repository.GetItemAsync(id);

           if(existingItem is null)
           {
                return NotFound();
           }

           Item updatedItem = existingItem with {
            FirstName = itemDto.FirstName,
            LastName = itemDto.LastName,
            Email = itemDto.Email,
            PhoneNumber = itemDto.PhoneNumber,
            Address = itemDto.Address,
            ZipCode = itemDto.ZipCode
           
           };

           await repository.UpdateItemAsync(updatedItem);
           return NoContent();
        }
        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var existingItem = await repository.GetItemAsync(id);

            if(existingItem is null)
           {
                return NotFound();
           }

           await repository.DeleteItemAsync(id);
           return NoContent();
        }

    }

}