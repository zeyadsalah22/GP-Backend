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
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IMapper _mapper;

        public AuthController(
            IUserService userService,
            IJwtService jwtService,
            ITokenBlacklistService tokenBlacklistService,
            IRefreshTokenService refreshTokenService,
            IMapper mapper)
        {
            _userService = userService;
            _jwtService = jwtService;
            _tokenBlacklistService = tokenBlacklistService;
            _refreshTokenService = refreshTokenService;
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
            var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user);
            
            return Ok(new AuthResponseDto 
            { 
                Token = token,
                RefreshToken = refreshToken,
                UserId = user.UserId,
                Email = user.Email,
                FullName = $"{user.Fname} {user.Lname}",
                ExpiresAt = _jwtService.GetTokenExpiry(token)
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
            var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(userEntity);
            
            return Ok(new AuthResponseDto 
            { 
                Token = token,
                RefreshToken = refreshToken,
                UserId = userDto.UserId,
                Email = userDto.Email,
                FullName = $"{userDto.Fname} {userDto.Lname}",
                ExpiresAt = _jwtService.GetTokenExpiry(token)
            });
        }

        // POST api/auth/refresh
        [HttpPost("refresh")]
        public async Task<ActionResult<AuthResponseDto>> RefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result = await _refreshTokenService.RefreshTokenAsync(refreshTokenDto.RefreshToken);
            
            if (result == null)
                return Unauthorized(new { message = "Invalid or expired refresh token" });
            
            return Ok(result);
        }

        // POST api/auth/revoke
        [Authorize]
        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeToken(RefreshTokenDto refreshTokenDto)
        {
            var result = await _refreshTokenService.RevokeRefreshTokenAsync(refreshTokenDto.RefreshToken);
            
            if (!result)
                return BadRequest(new { message = "Token not found" });
            
            return Ok(new { message = "Token revoked successfully" });
        }

        // POST api/auth/logout
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(RefreshTokenDto? refreshTokenDto = null)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            
            // Get token expiry from the JWT
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var expiry = jwtToken.ValidTo;
            
            // Add access token to blacklist until its expiry
            _tokenBlacklistService.BlacklistToken(token, expiry);
            
            // Revoke refresh token if provided
            if (refreshTokenDto != null && !string.IsNullOrEmpty(refreshTokenDto.RefreshToken))
            {
                await _refreshTokenService.RevokeRefreshTokenAsync(refreshTokenDto.RefreshToken);
            }
            
            return Ok(new { message = "Successfully logged out" });
        }
    }
} 