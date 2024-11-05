using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Accapt.Api.Controllers
{
    [Route("api/ManageUsers")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Injection

        private readonly IAuthenticationJwtServies _authenticationJwtServies;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IJwtHelper _jwtHelper;
        public UserController(IAuthenticationJwtServies authenticationJwtServies,
                              UserManager<IdentityUser> userManager,
                              IEmailSender emailSender,
                              SignInManager<IdentityUser> signInManager,
                              IJwtHelper jwtHelper)

        {
            _authenticationJwtServies = authenticationJwtServies ?? throw new ArgumentException(nameof(authenticationJwtServies));
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));
            _emailSender = emailSender ?? throw new ArgumentException(nameof(emailSender));
            _signInManager = signInManager ?? throw new ArgumentException(nameof(signInManager));
            _jwtHelper = jwtHelper ?? throw new ArgumentException(nameof(jwtHelper));
        }

        #endregion

        #region Register User

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(RegisterUserDTO register)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new IdentityUser
            {
                UserName = register.UserName,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
            };
            
            var registerResult = await _userManager.CreateAsync(user, register.Password);

            if(!registerResult.Succeeded)
            {
                foreach (var error in  registerResult.Errors)
                {
                    return BadRequest(error);
                }
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var confirmationLink = Url.Action(nameof(ConfirmEmail), "User", new {userId = user.Id, token}, Request.Scheme);

            var emailModel = new EmailViewModel(user.Email, "تایید حساب کاربری", confirmationLink);

            await _emailSender.SendEmailAsync(emailModel);

            return Ok(true);
        }

        #endregion

        #region Confrim Email

        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return BadRequest("کاربری یافت نشد");

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
                return Ok();
            else
                return BadRequest("خطای ثبت ایمیل");
        }

        #endregion

        #region Login User

        [HttpPost("Login")]
        public async Task<IActionResult> Loginuser(LoginUserDTO loginUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(loginUser.UserName);
            if (user == null)
                return BadRequest("کاربری یافت نشد");

            if (!user.EmailConfirmed)
                return Unauthorized();

            var result = await _signInManager.PasswordSignInAsync(user, loginUser.Password, true, false);

            if(result.Succeeded)
            {
                var token = await _authenticationJwtServies.AuthenticatJwtToken(user.Id, user.UserName);

                return Ok(new
                {
                    Token = token,
                    LoginStatuce = result
                });
            }
            else
            {
                return BadRequest(new
                {
                    Token = "",
                    LoginStatuce = ""
                });
            }
        }

        #endregion

        #region Get Single User

        [HttpGet("GetSingle")]
        [Authorize]
        public async Task<IActionResult> GetSingleUser()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var userId = _jwtHelper.GetUserIdFromToken(token);

                if(!string.IsNullOrEmpty(userId))
                {
                    var user = await _userManager.FindByIdAsync(userId);

                    if (user != null)
                        return Ok(new
                        {
                            UserName = user.UserName,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber
                        });

                    return BadRequest("کاربری یافت نشد");
                }

                return Unauthorized();
            }

            return Unauthorized();
        }

        #endregion

        //#region Update User

        //[Authorize]
        //[HttpPost("Update")]
        //public async Task<IActionResult> UpdatedUser(UserUpdateDTO userUpdateDTO)
        //{

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);


        //    var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        //    if(!string.IsNullOrEmpty(token))
        //    {
        //        var userName = _jwtHelper.GetUserNameFromToken(token);
        //        var userId = _jwtHelper.GetUserIdFromToken(token);

        //        if(!string.IsNullOrEmpty(userId) )
        //        {
        //            var user = await _userManager.FindByIdAsync(userId);

        //            if(user!= null)
        //            {
        //                user.EmailConfirmed = false;

        //                var token = _userManager.C();

        //                var statuceOfUpdate = await _userManager.UpdateAsync(user);

        //                if(statuceOfUpdate.Succeeded)
        //                {

        //                }
        //            }

        //            return BadRequest("کاربری یافت نشد");
        //        }

        //        return Unauthorized();

        //    }

        //    return Unauthorized();

        //    //if (patchDocument == null || !ModelState.IsValid)
        //    //    return BadRequest(ModelState);

        //    //var isExixstUser = await _findUserServies.IsExsistUserName(patchDocument.Operations[0].value.ToString());

        //    //if(isExixstUser)
        //    //    return BadRequest("This User Name is not Avablable");

        //    //var user = await _findUserServies.FindUserByUserName(userName);

        //    //if (user == null)
        //    //    return NotFound();

        //    //var usertToPatch = _mapper.Map<UserUpdateDTO>(user);

        //    //patchDocument.ApplyTo(usertToPatch, ModelState);

        //    //if (!ModelState.IsValid)
        //    //    return BadRequest(ModelState);

        //    //_mapper.Map(usertToPatch, user);
        //    //await _userServies.SaveChanges();

        //    //return Ok(user);
        //}

        //#endregion
    }
}
