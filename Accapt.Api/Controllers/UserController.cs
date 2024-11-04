using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
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

        private readonly IRegisterUserServies _registerServies;
        private readonly ILoginUserServies _loginUserServies;
        private readonly IFindUserServies _findUserServies;
        private readonly IMapper _mapper;
        private readonly IUserServies _userServies;
        private readonly IAuthenticationJwtServies _authenticationJwtServies;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        public UserController(IRegisterUserServies registerUserServies,
            ILoginUserServies loginUserServies,
            IFindUserServies findUserServies,
            IMapper mapper,
            IUserServies userServies,
            IAuthenticationJwtServies authenticationJwtServies,
            UserManager<IdentityUser> userManager,
            IEmailSender emailSender,
            SignInManager<IdentityUser> signInManager)
        {
            _registerServies = registerUserServies ?? throw new ArgumentException(nameof(registerUserServies));
            _loginUserServies = loginUserServies ?? throw new AbandonedMutexException(nameof(loginUserServies));
            _findUserServies = findUserServies ?? throw new ArgumentException(nameof(findUserServies));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _userServies = userServies ?? throw new ArgumentException(nameof(userServies));
            _authenticationJwtServies = authenticationJwtServies ?? throw new ArgumentException(nameof(authenticationJwtServies));
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));
            _emailSender = emailSender ?? throw new ArgumentException(nameof(emailSender));
            _signInManager = signInManager ?? throw new ArgumentException(nameof(signInManager)); ;
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
        public async Task<IActionResult> GetSingleUser(UserDTO userName)
        {
            if (userName == null)
                return BadRequest("کاربری وجود ندارد");

            var user = await _findUserServies.FindUserByUserName(userName.UserName);

            if(user == null)
                return NotFound();

            return Ok(user);
        }

        #endregion

        #region Update User

        [Authorize]
        [HttpPatch("Update/{userName}")]
        public async Task<IActionResult> UpdatedUser(string userName, [FromBody] JsonPatchDocument<UserUpdateDTO> patchDocument)
        {
            if (patchDocument == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var isExixstUser = await _findUserServies.IsExsistUserName(patchDocument.Operations[0].value.ToString());

            if(isExixstUser)
                return BadRequest("This User Name is not Avablable");

            var user = await _findUserServies.FindUserByUserName(userName);

            if (user == null)
                return NotFound();

            var usertToPatch = _mapper.Map<UserUpdateDTO>(user);

            patchDocument.ApplyTo(usertToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _mapper.Map(usertToPatch, user);
            await _userServies.SaveChanges();

            return Ok(user);
        }

        #endregion
    }
}
