using NUnit.Framework;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProductWebAPI.Models;

namespace NUnitTestProject
{
    [TestFixture]
    public class ProductControllerTests
    {
        private HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:8080"; // Change to your API URL

        [SetUp]
        public void Setup()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }

        [TearDown]
        public void Cleanup()
        {
            _httpClient?.Dispose();
        }

        [Test]
        public async Task Product_CRUD_Test()
        {
            // ARRANGE: Create a new product
            var newProduct = new Product
            {
                ProductName = "Test Product",
                BrandId = 1,
                CategoryId = 1,
                ModelYear = 2026,
                ListPrice = 99.99M
            };

            var newJson = JsonConvert.SerializeObject(newProduct);
            var newContent = new StringContent(newJson, Encoding.UTF8, "application/json");

            // ACT: Create the product
            var createResponse = await _httpClient.PostAsync("api/Product/AddProduct", newContent);
            createResponse.EnsureSuccessStatusCode();

            var createdContent = await createResponse.Content.ReadAsStringAsync();
            var createdProduct = JsonConvert.DeserializeObject<Product>(createdContent);
            Assert.IsNotNull(createdProduct);

            // ARRANGE: Update the product
            var updatedProduct = new Product
            {
                ProductId = createdProduct.ProductId,  // Keep same ID
                ProductName = "Updated Product",
                BrandId = 1,
                CategoryId = 1,
                ModelYear = 2026,
                ListPrice = 149.99M
            };

            var updatedJson = JsonConvert.SerializeObject(updatedProduct);
            var updatedContent = new StringContent(updatedJson, Encoding.UTF8, "application/json");

            // ACT: Update the product
            var updateResponse = await _httpClient.PutAsync($"api/Product/{createdProduct.ProductId}", updatedContent);
            updateResponse.EnsureSuccessStatusCode();

            // ACT: Retrieve the updated product
            var getResponse = await _httpClient.GetAsync($"api/Product/{createdProduct.ProductId}");
            getResponse.EnsureSuccessStatusCode();

            var getContent = await getResponse.Content.ReadAsStringAsync();
            var retrievedProduct = JsonConvert.DeserializeObject<Product>(getContent);

            // ASSERT: Verify updated product
            Assert.IsNotNull(retrievedProduct);
            Assert.AreEqual(updatedProduct.ProductName, retrievedProduct.ProductName);
            Assert.AreEqual(updatedProduct.ListPrice, retrievedProduct.ListPrice);
        }
    }
}