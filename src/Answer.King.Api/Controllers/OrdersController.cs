﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Answer.King.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Order = Answer.King.Api.RequestModels.Order;

namespace Answer.King.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        public OrdersController(IOrderService orders)
        {
            this.Orders = orders;
        }

        private IOrderService Orders { get; }
        

        /// <summary>
        /// Get all orders.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">When all the orders have been returned.</response>
        // GET api/orders
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Domain.Orders.Order>), 200)]
        public async Task<IActionResult> GetAll()
        {
            return this.Ok(await this.Orders.GetOrders());
        }

        /// <summary>
        /// Get a single order.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">When the order with the provided <paramref name="id"/> has been found.</response>
        /// <response code="404">When the order with the given <paramref name="id"/> does not exist</response>
        // GET api/orders/{GUID}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Domain.Orders.Order), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var order = await this.Orders.GetOrder(id);
            if (order == null)
            {
                return this.NotFound();
            }

            return this.Ok(order);
        }

        /// <summary>
        /// Create a new order.
        /// </summary>
        /// <param name="createOrder"></param>
        /// <response code="201">When the order has been created.</response>
        /// <response code="400">When invalid parameters are provided.</response>
        // POST api/orders
        [HttpPost]
        [ProducesResponseType(typeof(Domain.Orders.Order), 201)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        public async Task<IActionResult> Post([FromBody] Order createOrder)
        {
            try
            {
                var order = await this.Orders.CreateOrder(createOrder);

                return this.CreatedAtAction(nameof(this.GetOne), new { order.Id }, order);
            }
            catch (ProductInvalidException ex)
            {
                this.ModelState.AddModelError("LineItems.ProductId", ex.Message);
                return this.BadRequest(this.ModelState);
            }
        }

        /// <summary>
        /// Update an existing order.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateOrder"></param>
        /// <response code="200">When the order has been updated.</response>
        /// <response code="400">When invalid parameters are provided.</response>
        /// <response code="404">When the order with the given <paramref name="id"/> does not exist.</response>
        // PUT api/orders/{GUID}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Domain.Orders.Order), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put(Guid id, [FromBody] Order updateOrder)
        {

            try
            {
                var order = await this.Orders.UpdateOrder(id, updateOrder);

                if (order == null)
                {
                    return this.NotFound();
                }

                return this.Ok(order);
            }
            catch (ProductInvalidException ex)
            {
                this.ModelState.AddModelError("LineItems.ProductId", ex.Message);
                return this.BadRequest(this.ModelState);
            }
        }

        /// <summary>
        /// Cancel an existind order.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">When the order has been cancelled.</response>
        /// <response code="404">When the order with the given <paramref name="id"/> does not exist.</response>
        // DELETE api/orders/{GUID}
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Domain.Orders.Order), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var order = await this.Orders.CancelOrder(id);

            if (order == null)
            {
                return this.NotFound();
            }

            return this.Ok(order);
        }
    }
}

