using ProductWebAPI.Models;

namespace ProductWebAPI.Services
{
    public interface IBrandService
    {
        Task<List<Brand>> GetAllBrands();

        Task<Brand> GetBrandById(int brdId);

        Task<int> AddBrand(Brand brd);
    }
}