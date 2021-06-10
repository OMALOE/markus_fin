using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using kursach.Shared.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace kursach.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationContext context;

        public OrdersController(ApplicationContext ctx)
        {
            this.context = ctx;
        }

        /// <summary>
        /// Get order info
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        /// <response code="404"></response>
        /// <response code="200">Возвращает объект типа order</response>
        /// <response code="500"></response>
        [HttpGet("{id}")]
        public ActionResult GetOrderInfo(string id, [FromQuery] string  companyId)
        {
            try
            {
                Order order = context.Orders.Include(o => o.OrderProducts).Include(o => o.Customer).AsNoTracking().FirstOrDefault(o => o.Id == id);
                if (order == null)
                    return NotFound();
                return Ok(order);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get order products
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        /// <response code="404">Компания не найдена</response>
        /// <response code="404"></response>
        /// <response code="404"></response>
        /// <response code="200">Возвращает объект типа products</response>
        /// <response code="200">Возвращает объект типа products</response>
        /// <response code="500">Внутренняя ошибка</response>
        [HttpGet("{id}/products")]
        public ActionResult GetOrderProducts(string id, [FromQuery] string companyId)
        {
            Company company = null;
            List<OrderProduct> products = new List<OrderProduct>();
            try
            {

                if(!string.IsNullOrEmpty(companyId))
                {
                    Console.WriteLine(companyId);
                    company = context.Companies.FirstOrDefault<Company>(c => c.Id == companyId);
                    if (company == null)
                        return NotFound("Company not found!");
                    if (context.Orders.FirstOrDefault(o => o.Id == id) == null)
                        return NotFound();
                    products = context.OrderProducts.Where(op => op.Product.Companyid == companyId && op.Orderid == id).Include(op => op.Product).AsNoTracking().ToList();
                    if (products.Count == 0)
                        return NotFound();
                    return Ok(products);
                }
                Console.WriteLine(companyId);
                products = context.OrderProducts.Where(op => op.Orderid == id).Include(op => op.Product).AsNoTracking().ToList();
                return Ok(products);

            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error! Something went wrong! {e.Message}");
            }
        }

        /// <summary>
        /// Post new order
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="400">Неверные данные запроса. Невозможно найти заказ.</response>
        /// <response code="400"></response>
        /// <response code="400"></response>
        /// <response code="200">Заказ успешно создан</response>
        /// <response code="500"></response>
        [HttpPost]
        public ActionResult PostNewOrder([FromBody] JObject request)
        {
            if (!request.ContainsKey("order"))
                return BadRequest("Invalind post request data! Unable to find 'order'!");

            Order order = JsonConvert.DeserializeObject<Order>(request["order"].ToString());
            if (order.OrderProducts.Count == 0)
                return BadRequest();

            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(order, new ValidationContext(order), results) || order.OrderProducts.Count == 0)
                return BadRequest(results);
            try
            {
                context.Orders.Add(order);

                foreach (var item in order.OrderProducts)
                {
                    context.Products.First(p => p.Id == item.Productid).Available -= 1;
                }
                context.SaveChanges();
                return Ok("Order successfully created!");
            }
            catch (Exception e)
            {

                return StatusCode(500, e);
            }
        }

        /// <summary>
        /// Update order data
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="400">Неверные данные запроса. Невозможно найти заказ</response>
        /// <response code="404">Заказ не найден</response>
        /// <response code="200">Успешно</response>
        /// <response code="500">Внутренняя ошибка</response>
        [HttpPut("{orderid}")]
        public ActionResult UpdateOrderData(string orderid, [FromBody] JObject request)
        {

            if (!request.ContainsKey("order"))
                return BadRequest("Invalind post request data! Unable to find 'order'!");

            Order order = JsonConvert.DeserializeObject<Order>(request["order"].ToString());

            try
            {
                Order foundOrder = context.Orders.FirstOrDefault(o => o.Id == orderid);
                if (foundOrder == null)
                    return NotFound("No such order found!");

                context.Entry(foundOrder).CurrentValues.SetValues(order);
                
                if(order.OrderProducts.Count > 0)
                {
                    foreach (var item in order.OrderProducts)
                    {
                        OrderProduct foundOrderProduct = context.OrderProducts.FirstOrDefault(op => op.Orderid == item.Orderid && op.Productid == item.Productid);
                        context.Entry(foundOrderProduct).CurrentValues.SetValues(item);
                    }
                }

                context.SaveChanges();

                return Ok("Successfully!");

            }
            catch (Exception)
            {
                return StatusCode(500, "Error! Somthing went wrong!");
            }
        }
        /// <summary>
        /// Update order data
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="orderProductId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="400">Неверные данные запроса. Невозможно найти заказ</response>
        /// <response code="404">Заказ не найден</response>
        /// <response code="200">Успешно</response>
        /// <response code="500">Внутренняя ошибка</response>
        [HttpPut("{orderid}/product")]
        public ActionResult UpdateOrderData(string orderid, string orderProductId, [FromBody] JObject request)
        {

            if (!request.ContainsKey("orderPproduct"))
                return BadRequest("Invalind post request data! Unable to find 'order'!");

            OrderProduct orderProduct = JsonConvert.DeserializeObject<OrderProduct>(request["orderProduct"].ToString());

            try
            {
                OrderProduct foundOrderProduct = context.OrderProducts.FirstOrDefault(o => o.Orderid == orderid && o.Productid == orderProduct.Productid);
                if (foundOrderProduct == null)
                    return NotFound("No such order product found!");

                context.Entry(foundOrderProduct).CurrentValues.SetValues(orderProduct);

                context.SaveChanges();

                return Ok("Successfully!");

            }
            catch (Exception)
            {
                return StatusCode(500, "Error! Somthing went wrong!");
            }
        }

        /// <summary>
        /// Update order products
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPut("{orderId}/products")]
        public ActionResult UpdateOrderProducts(string orderId, string productId)
        {
            

            return Ok();

        }
    }
}
