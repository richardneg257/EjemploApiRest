using EjemploApiRest.Services;
using EjemploApiRest.WebApi.Configuration;
using EjemploApiRest.WebApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EjemploApiRest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenHandlerService _service;

        public AuthController(UserManager<IdentityUser> userManager, ITokenHandlerService service)
        {
            _userManager = userManager;
            _service = service;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDto user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if (existingUser != null)
                {
                    return BadRequest("El correo electrónico indicado ya existe!");
                }

                var isCreated = await _userManager.CreateAsync(new IdentityUser() { Email = user.Email, UserName = user.Email }, user.Password);
                if (isCreated.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(isCreated.Errors.Select(x => x.Description).ToList());
                }
            }
            else
            {
                return BadRequest("Se produjo algún error al registrar el usuario!");
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto user)
        {
            if (!ModelState.IsValid)
                return BadRequest(new UserLoginResponseDto()
                {
                    Login = false,
                    Errors = new List<string>() { "Usuario o contraseña incorrecta!" }
                });

            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser == null)
                return BadRequest(new UserLoginResponseDto()
                {
                    Login = false,
                    Errors = new List<string>() { "Usuario o contraseña incorrecta!" }
                });

            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);
            if (!isCorrect)
                return BadRequest(new UserLoginResponseDto()
                {
                    Login = false,
                    Errors = new List<string>() { "Usuario o contraseña incorrecta!" }
                });

            var pars = new TokenParameters()
            {
                Id = existingUser.Id,
                PasswordHash = existingUser.PasswordHash,
                UserName = existingUser.UserName
            };

            var jwtToken = _service.GenerateJwtToken(pars);
            return Ok(new UserLoginResponseDto()
            {
                Login = true,
                Token = jwtToken
            });

        }
    }
}
