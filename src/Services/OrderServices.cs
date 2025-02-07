

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using services_order_add.Database;
using services_order_add.Models;
using System.Net.Http;
using NewtonsoftJson = Newtonsoft.Json.JsonConvert;

namespace services_order_add.Services
{
    public class OrderServices
    {
        private readonly DBContext _context;
        private readonly HttpClient _httpClient;

        public OrderServices(DBContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        public async Task<Orders> CreateOrderAsync(Orders order)
        {
            order.Status = true;
            order.CreatedAt = DateTime.Now;
            order.UpdatedAt = DateTime.Now;

            var createOrder = _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            var idOrder = createOrder.Entity.Id;

            var result =  await _context.Orders.FirstOrDefaultAsync(x => x.Id == idOrder);

            return order;
        }

        public async Task<List<Orders>> GetAllOrdersAsync()
        {
            return await _context.Orders.Where(x => x.Status == true).ToListAsync();
        }

        public async Task<Orders> GetOrderByCodeAsync(string orderCode)
        {
            return await _context.Orders.FirstOrDefaultAsync(order => order.OrderCode == orderCode && order.Status == true);
        }

        public async Task<string> GenerateNextOrderCodeAsync()
        {
            var quantity = await _context.Orders.CountAsync();
            var nextCode = $"ORD_{quantity + 1}";
            return nextCode;
        }

        public async Task<bool> CheckIfProductExistsAsync(string codice)
        {
            //var url = $"http://localhost:3000/products/byCodiceProducto?codice={codice}"; // Local
            var url = $"http://app_producto_search:3000/products/byCodiceProducto?codice={codice}";
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var apiResponse = NewtonsoftJson.DeserializeObject<ApiResponse<JArray>>(responseContent);
                    if (apiResponse != null && apiResponse.Status == "success")
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> CheckIfWorkTeamExistsAsync(string codice)
        {
            //var url = $"http://localhost:8180/work-team/{codice}"; // Local
            var url = $"http://app_work_team_search:8080/work-team/{codice}";
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var apiResponse = NewtonsoftJson.DeserializeObject<ApiResponse<JObject>>(responseContent);
                    if (apiResponse != null && apiResponse.Status == "success")
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}

