﻿using Answer.King.Api.RequestModels;
using Answer.King.Api.Services;
using Answer.King.Domain.Repositories.Models;
using Microsoft.AspNetCore.Mvc;
using Order = Answer.King.Domain.Orders.Order;

namespace Answer.King.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class PaymentsController : ControllerBase
{
    public PaymentsController(
        IPaymentService payments,
        IOrderService orders)
    {
        this.Payments = payments;
        this.Orders = orders;
    }

    private IPaymentService Payments { get; }

    private IOrderService Orders { get; }

    /// <summary>
    /// Gets all payments
    /// </summary>
    /// <returns></returns>
    /// <response code="200">When all the payments have been returned.</response>
    // GET: api/payments
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Payment>), 200)]
    public async Task<IActionResult> GetAll()
    {
        var payments = await this.Payments.GetPayments();
        return this.Ok(payments);
    }

    /// <summary>
    /// Get a single payment.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">When the payment with the provided <paramref name="id"/> has been found.</response>
    /// <response code="404">When the payment with the given <paramref name="id"/> does not exist</response>
    // GET: api/payments/5
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Payment), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetOne(Guid id)
    {
        var payment = await this.Payments.GetPayment(id);

        if (payment == null)
        {
            return this.NotFound();
        }

        return this.Ok(payment);
    }

    /// <summary>
    /// Create a new payment.
    /// </summary>
    /// <param name="makePayment"></param>
    /// <response code="201">When the payment has been created.</response>
    /// <response code="400">When invalid parameters are provided.</response>
    // POST api/payments
    [HttpPost]
    [ProducesResponseType(typeof(Payment), 201)]
    [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
    public async Task<IActionResult> Post([FromBody] MakePayment makePayment)
    {
        try
        {
            var payment = await this.Payments.MakePayment(makePayment);

            return this.CreatedAtAction(nameof(this.GetOne), new { payment.Id }, payment);
        }
        catch (PaymentServiceException ex)
        {
            this.ModelState.AddModelError("error", ex.Message);
            return this.BadRequest(this.ModelState);
        }
    }

    /// <summary>
    /// Get order for payment.
    /// </summary>
    /// <param name="id"></param>
    /// <response code="200">When the order has been returned.</response>
    /// <response code="404">When the payment with the given <paramref name="id"/> does not exist.</response>
    // GET api/payments/{GUID}/order
    [HttpGet("{id}/order")]
    [ProducesResponseType(typeof(IEnumerable<Order>), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        var payment = await this.Payments.GetPayment(id);

        if (payment == null)
        {
            return this.NotFound();
        }

        var order = await this.Orders.GetOrder(payment.OrderId);

        return this.Ok(order);
    }
}