using Microsoft.AspNetCore.Mvc;
using services_order_add.Services;
using services_order_add.Models;


[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderServices _orderService;
    private readonly HttpClient _httpClient;

    public OrdersController(OrderServices orderService, HttpClient httpClient)
    {
        _orderService = orderService;
        _httpClient = httpClient;
    }

    [HttpPost("add")]
    public async Task<IActionResult> CreateOrder([FromBody] Orders order)
    {
        if (order == null)
        {
            return BadRequest(new ApiResponse<string>("Error", null, "Invalid order data"));
        }

        var existProduct = await _orderService.CheckIfProductExistsAsync(order.ProductCode);

        if (existProduct == false) {
            return BadRequest(new ApiResponse<string>("Error", null, "The product code is incorrect"));
        }

        var existWorkTeam = await _orderService.CheckIfWorkTeamExistsAsync(order.TeamCode);

        if (existWorkTeam == false)
        {
            return BadRequest(new ApiResponse<string>("Error", null, "The work team code is incorrect"));
        }

        order.OrderCode = await _orderService.GenerateNextOrderCodeAsync();

        var createdOrder = await _orderService.CreateOrderAsync(order);

        if (createdOrder == null)
        {
            return StatusCode(500, new ApiResponse<string>("Error", null, "Order was created but could not be retrieved"));
        }

        return CreatedAtAction(nameof(CreateOrder),
            new { codice = createdOrder.OrderCode },
            new ApiResponse<Orders>("success", createdOrder, "Order created successfully"));
    }
}
