using CommonLibrary.Models;
using CommonLibrary.Models.DBModelsHelpers;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TradesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly RabbitMQHelper _rabbitMQHelper;

        public TradesController(DataContext context, RabbitMQHelper rabbitMQHelper)
        {
            _context = context;
            _rabbitMQHelper = rabbitMQHelper;
        }

        [HttpPost("retry/{id}")]
        public async Task<IActionResult> RetryTrade(string id)
        {
            bool validAppUser = await TradeHelper.ValidTradeAsync(context: _context, identifier: id);
            if (!validAppUser) { return NotFound(); }

            await TradeHelper.UpdateTradeToRetryAsync(context: _context, rabbitMQHelper: _rabbitMQHelper, identifier: id);

            return Ok(new { Message = "Retring Trade." });
        }

        [HttpPost("cancel/{id}")]
        public async Task<IActionResult> CancelTrade(string id)
        {
            bool validAppUser = await TradeHelper.ValidTradeAsync(context: _context, identifier: id);
            if (!validAppUser) { return NotFound(); }

            await TradeHelper.UpdateTradeToCanceledAsync(context: _context, rabbitMQHelper: _rabbitMQHelper, identifier: id);

            return Ok(new { Message = "Trade Canceled." });
        }
    }
}
