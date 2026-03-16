using ECommerce.Application.Authentication.Interfaces;
using ECommerce.Application.Users.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.API.Controllers.Users
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            try
            {
                var result = await _service.RegisterAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private void SetTokenCookies(string accessToken, string refreshToken)
        {
            Response.Cookies.Append("jwt", accessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddHours(2)
            });

            Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7)
            });
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _service.LoginAsync(dto);

            SetTokenCookies(result.AccessToken, result.RefreshToken);


            return Ok(new AuthResponseDto
            {
                AccessToken = result.AccessToken,
                RefreshToken = result.RefreshToken,
                Role=result.Role
            });
        }



        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized();

            var result = await _service.RefreshTokenAsync(refreshToken);

            SetTokenCookies(result.AccessToken, result.RefreshToken);

            return Ok();
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            Response.Cookies.Delete("refreshToken");

            return Ok(new
            {
                message = "Logged out successfully"
            });
        }



    }
}
