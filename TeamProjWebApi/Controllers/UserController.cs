using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using TeamProj.Models.User;
using Microsoft.AspNetCore.Http;
using System.Text;

[ApiController]
[Microsoft.AspNetCore.Components.Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        //private readonly IAuthenticationManager _authManager;
        private readonly IUserService _userService;
        private readonly ITokenService _tService;

        public UserController(IUserService uService, ITokenService tService)
        {
            _userService = uService;
            _tService = tService;
        }

        

         [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegister model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var registerResult = await _userService.RegisterUserAsync(model);
            if (registerResult)
            {
                return Ok("User was registered");
            }
            return BadRequest("User could not be registered.");
        }
       // [HttpPost("Login")]
        //[ProducesErrorResponseType(StatusCodes.StatusCode401Unauthorized)]
        
        // public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
        // {
        //     var authResponse = await _userService.Login(loginDTO);

        //     if (authResponse == null)
        //     {
        //         return Unauthorized();
        //     }

        //     return Ok(authResponse);
        // }

        [HttpPost("~/api/Token")]
        public async Task<IActionResult> Token([FromBody] TokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tokenResponse = await _tService.GetTokenAsync(request);
            if (tokenResponse is null)
            {
                return BadRequest("invalid username or password");
            }
            return Ok(tokenResponse);
        }
    }
