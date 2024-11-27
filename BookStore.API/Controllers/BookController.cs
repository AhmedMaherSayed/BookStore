using AutoMapper;
using BookStore.API.DTOs.BooksDtos;
using BookStore.Core;
using BookStore.Core.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Select All Book", Description = "Example: https://localhost:7021/api/Book")]
        [SwaggerResponse(200, "return all Books", typeof(List<BookDto>))]
        public async Task<IActionResult> GetAll()
        {
            var entites = await _unitOfWork.Repository<Book, int>()
                .GetAllAsync(false);
            if(entites is null || !entites.Any())
                return NotFound("No Books exists!");

            var dtos = _mapper.Map<List<BookDto>>(entites);
            return Ok(dtos);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "Can Search By Id", Description = "Example: https://localhost:7021/api/Book/1")]
        [SwaggerResponse(200, "return Book", typeof(List<BookDto>))]
        [SwaggerResponse(404, "If no book is founded")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _unitOfWork.Repository<Book, int>()
                .GetWithCriteriaAsync(e => e.Id == id);
            if (entity is null)
                return BadRequest($"No Book with id = {id}");

            var dto = _mapper.Map<BookDto>(entity);
            return Ok(dto);
        }


        [HttpPost]
        [SwaggerOperation(Summary = "Add new Book")]
        [SwaggerResponse(200, "If a Book Created Successfully", typeof(List<BookDto>))]
        [SwaggerResponse(400, "If Invalid Book Data")]
        public async Task<IActionResult> Add(AddBookDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = _mapper.Map<Book>(dto);
            if (entity is null)
                return BadRequest();

            await _unitOfWork.Repository<Book, int>()
                .AddAsync(entity);

            await _unitOfWork.SaveChangesAsync();
            //return CreatedAtAction(nameof(GetById), new {id = entity.Id});
            return Ok(entity);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(UpdateBookDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var entity = await _unitOfWork.Repository<Book, int>()
                .GetByIdAsync(dto.Id);

            _mapper.Map(dto, entity);
            await _unitOfWork.SaveChangesAsync();
            return Ok(dto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            var entity = await _unitOfWork.Repository<Book, int>()
                .GetByIdAsync(id);

            if(entity is null)
                return NotFound($"No Book was found was id = {id}");

            _unitOfWork.Repository<Book, int>().SoftDelete(entity);
            await _unitOfWork.SaveChangesAsync();
            return Ok(entity);
        }
    }
}
