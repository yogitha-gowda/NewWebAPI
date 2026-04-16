using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.Services;

namespace ProductWebAPI.Controllers
{
    [ApiController]
    [Route("api/Brand")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService brandService;

        public BrandController(IBrandService brandService)
        {
            this.brandService = brandService;
        }

        // GET: api/Brand/GetAllBrands
        [HttpGet("GetAllBrands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await brandService.GetAllBrands();
            return Ok(brands);
        }

        // GET: api/Brand/GetBrandById/1
        [HttpGet("GetBrandById/{brdId}")]
        public async Task<IActionResult> GetBrandById(int brdId)
        {
            var brand = await brandService.GetBrandById(brdId);

            if (brand == null)
                return NotFound();

            return Ok(brand);
        }

        // POST: api/Brand/AddBrand
        [HttpPost("AddBrand")]
        public async Task<IActionResult> AddBrand([FromBody] Models.Brand brd)
        {
            var result = await brandService.AddBrand(brd);

            if (result > 0)
                return Ok(result);

            return BadRequest();
        }
    }
}