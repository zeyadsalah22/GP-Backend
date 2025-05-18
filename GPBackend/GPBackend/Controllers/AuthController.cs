using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GPBackend.DTOs.Auth;
using GPBackend.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using GPBackend.Models;
using AutoMapper;

namespace GPBackend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly ITokenBlacklistService _tokenBlacklistService;
        private readonly IMapper _mapper;

        public AuthController(
            IUserService userService,
            IJwtService jwtService,
            ITokenBlacklistService tokenBlacklistService,
            IMapper mapper)
        {
            _userService = userService;
            _jwtService = jwtService;
            _tokenBlacklistService = tokenBlacklistService;
            _mapper = mapper;
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
        {
            var user = await _userService.AuthenticateAsync(loginDto.Email, loginDto.Password);
            
            if (user == null)
                return Unauthorized(new { message = "Invalid email or password" });

            var token = _jwtService.GenerateToken(user);
            
            return Ok(new AuthResponseDto 
            { 
                Token = token,
                UserId = user.UserId,
                Email = user.Email,
                FullName = $"{user.Fname} {user.Lname}"
            });
        }

        // POST api/auth/register
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto registerDto)
        {
            // Check if email already exists
            if (await _userService.EmailExistsAsync(registerDto.Email))
                return BadRequest(new { message = "Email is already in use" });

            // Register new user
            var userId = await _userService.RegisterUserAsync(registerDto);
            
            // Get created user
            var userDto = await _userService.GetUserByIdAsync(userId);
            if (userDto == null)
                return StatusCode(500, new { message = "Error retrieving created user" });

            // Map DTO to entity for token generation
            var userEntity = _mapper.Map<User>(userDto);
            
            // Generate token
            var token = _jwtService.GenerateToken(userEntity);
            
            return Ok(new AuthResponseDto 
            { 
                Token = token,
                UserId = userDto.UserId,
                Email = userDto.Email,
                FullName = $"{userDto.Fname} {userDto.Lname}"
            });
        }

        // POST api/auth/logout
        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            
            // Get token expiry from the JWT
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var expiry = jwtToken.ValidTo;
            
            // Add token to blacklist until its expiry
            _tokenBlacklistService.BlacklistToken(token, expiry);
            
            return Ok(new { message = "Successfully logged out" });
        }
    }
} 