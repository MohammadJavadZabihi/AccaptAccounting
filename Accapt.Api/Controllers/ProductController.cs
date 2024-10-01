using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public ProductController(IProductServies productServies,
            IFindeProductServies findeProductServies,
            IMapper mapper)
        {
            _productServies = productServies ?? throw new ArgumentException(nameof(productServies));
            _findeProductServies = findeProductServies ?? throw new ArgumentException(nameof(findeProductServies));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }

        #endregion

        #region AddProduct

        [HttpPost("Add")]
        public async Task<IActionResult> AddProduct(AddProductDTO addProductDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productServies.AddProduct(addProductDTO);

            if (product == null)
                return BadRequest("Null Exeption");

            return Ok(addProductDTO);
        }

        #endregion

        #region RemoveProduc

        [HttpDelete("Delet")]
        public async Task<IActionResult> DeletProductByName(SingleProductNameDTO productName)
        {
            if (!ModelState.IsValid || productName.ProductId == null)
                return BadRequest(ModelState);

            var product = await _findeProductServies.FindeProduct(productName.ProductId, productName.UserId);

            if (product == null)
                return BadRequest("null product");

            var deletProduct = await _productServies.DeletProduct(product);

            if (!deletProduct)
                return BadRequest();

            return Ok(deletProduct);
        }

        #endregion

        #region UpdateProduct

        [HttpPatch("Update/{userId}/{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, string userId, [FromBody] JsonPatchDocument<ProductUpdateDTO> patchDocument)
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
