using AutoMapper;
using BookStore.API.DTOs;
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
            //var customer = new Customer
            //{
            //    Email = dto.Email,
            //    Address = dto.Address,
            //    UserName = dto.Username,
            //    FullName = dto.FullName,
            //    PhoneNumber = dto.PhoneNumber,
            //};
            var customer = _mapper.Map<Customer>(dto);
            var r = await _userManger.CreateAsync(customer, dto.Password);
            if(r.Succeeded)
            {
                var role = await _roleManager.FindByNameAsync("Customer");

                if (role is null)
                    return BadRequest("Role isn't found");

                var rr =  await _userManger.AddToRoleAsync(customer, "Customer");
                if (rr.Succeeded)
                {
                    return Ok();
                }
                else
                    return BadRequest(rr.Errors);
            }
            return BadRequest(r.Errors);
        }
    }
}
