using AutoMapper;
using BookStore.API.DTOs.CustomerDtos;
using BookStore.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public CustomerController(UserManager<IdentityUser> userManger, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManger = userManger;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCustomerDto dto)
        {
           if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = _mapper.Map<Customer>(dto);
            
            var result = await _userManger.CreateAsync(entity, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var resultFinal = await _userManger.AddToRoleAsync(entity, "Customer");

            if (!resultFinal.Succeeded)
                return BadRequest(resultFinal.Errors);

            return Ok(resultFinal);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users =  _userManger.GetUsersInRoleAsync("Customer")
                                    .Result
                                    .OfType<Customer>()
                                    .ToList();

            if (!users.Any())
                return NotFound("No users with Customer Role!");

            var dtos = _mapper.Map<List<DisplayCustomerDto>>(users);
            return Ok(dtos);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(string id)
        {
            var entity = (Customer) _userManger.GetUsersInRoleAsync("Customer")
                            .Result
                            .FirstOrDefault(customer => customer.Id == id);
            if (entity is null)
                return BadRequest($"No Customer with Id = {id}");

            var dto = _mapper.Map<DisplayCustomerDto>(entity);
            return Ok(dto);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(EditCustomerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = (Customer) await _userManger.FindByIdAsync(dto.Id);

            if (entity is null)
                return NotFound($"No Customer With ID = {dto.Id}");

            _mapper.Map(dto, entity);
            await _userManger.UpdateAsync(entity);
            return NoContent();
        }

        [HttpPatch("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = (Customer) await _userManger.FindByIdAsync(dto.Id);

            if (entity is null)
                return NotFound($"Customer with ID = {dto.Id} is not found.");

            var result = await _userManger.ChangePasswordAsync(entity, dto.OldPassword, dto.NewPassword);
            if (!result.Succeeded)
                return BadRequest();

            return Ok("Password Updated Successfully");
        }
    }
}
