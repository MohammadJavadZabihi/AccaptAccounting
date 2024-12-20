﻿using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accapt.Api.Controllers
{
    [Route("api/ProviderMnager")]
    [ApiController]
    [Authorize]
    public class ServiceProviderController : ControllerBase
    {
        private readonly IProviderService _providerService;
        private readonly IAuthenticationJwtServies _authenticationJwtServies;
        public ServiceProviderController(IProviderService providerService, 
            IAuthenticationJwtServies authenticationJwtServies)
        {
            _providerService = providerService ?? throw new ArgumentException(nameof(providerService));
            _authenticationJwtServies = authenticationJwtServies;
        }

        #region Register Provider

        [HttpPost("Register")]
        public async Task<IActionResult> Add(AddServiceProviderDTO addServiceProviderDTO)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var addProvider = await _providerService.Add(addServiceProviderDTO);

            if(addProvider == false)
                return NotFound();

            return Ok(addProvider);
        }

        #endregion

        #region Remove Provider

        [HttpDelete("Delet")]
        public async Task<IActionResult> Remove([FromQuery]int providerId, [FromQuery]string userId)
        {
            if(providerId == 0 || userId == null)
                return BadRequest();

            var removeProvider = await _providerService.Remove(providerId, userId);

            if(removeProvider == false)
                return NotFound();

            return Ok(removeProvider);
        }

        #endregion

        #region Get All Providers

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get([FromQuery] int pageNumber, [FromQuery] int pageSize,
            [FromQuery] string filter = "", [FromQuery] string userId = "")
        {
            var providers = await _providerService.GetAll(pageNumber, pageSize, filter, userId);

            return Ok(new
            {
                Providers = providers,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
        }

        #endregion

        #region Update Password

        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> Update(UpdatePasswordProviderDTO updateDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatePassword = await _providerService.Update(updateDTO.providerId, updateDTO.userId, updateDTO.currentPassword, updateDTO.newPassword);

            if(updatePassword == false) 
                return NotFound();

            return Ok(updatePassword);
        }

        #endregion

        #region Login Provider

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginProviderServiceDTO login)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var loginStatuce = await _providerService.Login(login);

            if(loginStatuce.ISuucess == false)
                return NotFound();

            var token = await _authenticationJwtServies.AuthenticatJwtTokenForMobileApp(login);

            return Ok(new
            {
                token = token,
                loginStatuce = loginStatuce,
            });
        }

        #endregion
    }
}
