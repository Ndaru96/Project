using HotChocolate.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OrderService;
using OrderService.GraphQl;
using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.GraphQL
{
    public class Mutation
    {
        //[Authorize]
        public async Task<StatusOrder> AddOrderAsync(
            OrderData input, ClaimsPrincipal claimsPrincipal,
            [Service] ProductQLContext context, [Service] IOptions<KafkaSettings> settings)
        {
            var dts = DateTime.Now.ToString();
            var key = "order-" + dts;
            var val = JsonConvert.SerializeObject(input);

            var result = await KafkaHelper.SendMessage(settings.Value, "order", key, val);

            StatusOrder resp = new StatusOrder
            {
                TransactionDate = dts,
                Message = "Order was submitted successfully"
            };

            if (!result)
                resp.Message = "Failed to submit data";

            return await Task.FromResult(resp);
        }
    }
}
