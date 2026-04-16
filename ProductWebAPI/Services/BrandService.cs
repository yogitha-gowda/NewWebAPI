using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Models;
namespace ProductWebAPI.Services
{
    public class BrandService : IBrandService
    {
        private readonly ProductContext productContext;
        public BrandService(ProductContext prdContext)
        {
           productContext = prdContext;
        }
        public async Task<List<Brand>> GetAllBrands()
        {
            return await productContext.Brands.ToListAsync();
        }
        public async Task<Brand> GetBrandById(int brdId)
        {
            return await productContext.Brands.FirstOrDefaultAsync(b => b.BrandId == brdId);
        }
        public async Task<int> AddBrand(Brand brd)
        {
            await productContext.Brands.AddAsync(brd);
            int result = await productContext.SaveChangesAsync();
            return result;
        }
    }
}
