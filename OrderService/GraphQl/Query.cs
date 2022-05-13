﻿using HotChocolate.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using OrderService.Models;
using System.Security.Claims;

namespace OrderService.GraphQl
{
    public class Query
    {
        [Authorize]
        public IQueryable<Order> GetOrders([Service] ProductQLContext context, ClaimsPrincipal claimsPrincipal)
        {
            var userName = claimsPrincipal.Identity.Name;

            // check manager role ?
            var managerRole = claimsPrincipal.Claims.Where(o => o.Type == ClaimTypes.Role).FirstOrDefault();
            var user = context.Users.Where(o => o.Username == userName).FirstOrDefault();
            if (user != null)
            {
                if (managerRole.Value == "MANAGER")
                    return context.Orders.Include(o => o.OrderDetails);

                var orders = context.Orders.Where(o => o.UserId == user.Id);
                return orders.AsQueryable();
            }


            return new List<Order>().AsQueryable();
        }

        
    }
}
