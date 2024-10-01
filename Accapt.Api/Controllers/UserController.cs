using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        public UserController(IRegisterUserServies registerUserServies,
            ILoginUserServies loginUserServies,
            IFindUserServies findUserServies,
            IMapper mapper,
            IUserServies userServies,
            IAuthenticationJwtServies authenticationJwtServies)
        {
            _registerServies = registerUserServies ?? throw new ArgumentException(nameof(registerUserServies));
            _loginUserServies = loginUserServies ?? throw new AbandonedMutexException(nameof(loginUserServies));
            _findUserServies = findUserServies ?? throw new ArgumentException(nameof(findUserServies));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _userServies = userServies ?? throw new ArgumentException(nameof(userServies));
            _authenticationJwtServies = authenticationJwtServies ?? throw new ArgumentException(nameof(authenticationJwtServies));
        }

        #endregion

        #region Register User

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(RegisterUserDTO register)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var resgiterUser = await _registerServies.RegisterUser(register);

            if (register == null)
                return BadRequest(resgiterUser.Message);

            if (!resgiterUser.ISuucess)
                return BadRequest(resgiterUser.Message);

            return Ok(resgiterUser);

        }

        #endregion

        #region Login User

        [HttpPost("Login")]
        public async Task<IActionResult> Loginuser(LoginUserDTO loginUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var loginUserStatuce = await _loginUserServies.LoginUser(loginUser);

            if (!loginUserStatuce.ISuucess)
                return BadRequest(loginUserStatuce.Message);

            var token = await _authenticationJwtServies.AuthenticatJwtToken(loginUser);

            return Ok(new
            {
                Token = token,
                LoginStatuce = loginUserStatuce
            });
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
