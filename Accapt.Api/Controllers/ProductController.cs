using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Accapt.Api.Controllers
{
    [Route("api/MangeProduct")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        #region Injection

        private IProductServies _productServies;
        private IFindeProductServies _findeProductServies;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtHelper _jwtHelper;
        public ProductController(IProductServies productServies,
            IFindeProductServies findeProductServies,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            IJwtHelper jwtHelper)
        {
            _productServies = productServies ?? throw new ArgumentException(nameof(productServies));
            _findeProductServies = findeProductServies ?? throw new ArgumentException(nameof(findeProductServies));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));
            _jwtHelper = jwtHelper ?? throw new ArgumentException(nameof(jwtHelper));
        }

        #endregion

        #region AddProduct

        [HttpPost("Add")]
        public async Task<IActionResult> AddProduct(AddProductDTO addProductDTO)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var userId = _jwtHelper.GetUserIdFromToken(token);

            if(!string.IsNullOrEmpty(userId))
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var product = await _productServies.AddProduct(addProductDTO, userId);

                if (product == null)
                    return BadRequest("کاربری یافت نشد");

                return Ok();
            }

            return Unauthorized("توکن نامعتبر است");
        }

        #endregion

        #region RemoveProduc

        [HttpDelete("Delet")]
        public async Task<IActionResult> DeletProductByName(SingleProductNameDTO productName)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var userId = _jwtHelper?.GetUserIdFromToken(token);

            if(!string.IsNullOrEmpty(userId))
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var product = await _findeProductServies.FindeProduct(userId, productName.UserId);

                if (product == null)
                    return BadRequest("محصولی یافت نشد");

                var deletProduct = await _productServies.DeletProduct(product);

                if (!deletProduct)
                    return BadRequest();

                return Ok(deletProduct);
            }

            return Unauthorized();
        }

        #endregion

        #region UpdateProduct

        [HttpPatch("Update/{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, ProductUpdateDTO productUpdateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _findeProductServies.FindeProduct(productId, userId);

            if (product == null)
                return NotFound();

            var productToPatch = _mapper.Map<ProductUpdateDTO>(product);

            patchDocument.ApplyTo(productToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _mapper.Map(productToPatch, product);
            await _productServies.Save();

            return Ok(product);
        }

        #endregion

        #region GetProducts

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProducts([FromQuery] int pageNumber, [FromQuery] int pageSize,
            [FromQuery]string filter = "", [FromQuery] string userId = "")
        {
            var product = await _productServies.GetProducts(pageNumber, pageSize, filter, userId);

            if (product == null)
                return NotFound();

            return Ok(new
            {
                Products = product,
                PageNumber = pageNumber,    
                PageSize = pageSize
            });
        }

        #endregion

        #region GetSingleProduct

        [HttpGet("GetSingle/{userId}/{productId}")]
        public async Task<IActionResult> GetSingleProduct(int productId, string userId)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _findeProductServies.FindeProduct(productId, userId);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        #endregion

        #region IsExistProduct

        [HttpPost("ExistProduct")]
        public async Task<IActionResult> IsExistProduct(ChekIsExistProduct chekIsExistProduct)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var isExistProduct = await _findeProductServies.IsExistProduct(chekIsExistProduct);

            return Ok(isExistProduct);
        }

        #endregion
    }
}
