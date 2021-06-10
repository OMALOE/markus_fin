using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kursach.Shared.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace kursach.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ApplicationContext context;

        public CustomersController(ApplicationContext ctx)
        {
            context = ctx;
        }

        /// <summary>
        /// Post new customer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="400"></response>
        /// <response code="400"></response>
        /// <response code="409"></response>
        /// <response code="200"></response>
        /// <response code="500"></response>
        [HttpPost]
        public ActionResult PostCustomer([FromBody] JObject request)
        {
            if (!request.ContainsKey("customer"))
                return BadRequest();

            Customer customer = request["customer"].ToObject<Customer>();
            
            var results = new List<ValidationResult> { };
            if (!Validator.TryValidateObject(customer, new ValidationContext(customer), results))
                return BadRequest(results);

            try
            {
                if (context.Customers.FirstOrDefault(c => c.Id == customer.Id) != null)
                    return Conflict();
                context.Customers.Add(customer);
                context.SaveChanges();
                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Get customer data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="404"></response>
        /// <response code="200">Возвращает объект типа customer</response>
        /// <response code="500"></response>
        [HttpGet("{id}")]
        public ActionResult GetCustomerData(string id)
        {
            try
            {
                Customer customer = context.Customers.AsNoTracking().FirstOrDefault(u => u.Id == id);
                if (customer == null)
                    return NotFound();

                return Ok(customer);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Put customer data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="400"></response>
        /// <response code="400"></response>
        /// <response code="404"></response>
        /// <response code="200"></response>
        /// <response code="500"></response>
        [HttpPut("{id}")]
        public ActionResult PutCustomerData(string id, [FromBody] JObject request)
        {
            if (!request.ContainsKey("customer"))
                return BadRequest();

            Customer customer = request["customer"].ToObject<Customer>();

            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(customer, new ValidationContext(customer), results))
                return BadRequest(results);
            try
            {
                Customer foundCustomer = context.Customers.Find(customer.Id); //find
                if (foundCustomer == null)
                    return NotFound();

                context.Entry(foundCustomer).CurrentValues.SetValues(customer);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="404"></response>
        /// <response code="200"></response>
        /// <response code="500"></response>
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(string id)
        {
            try
            {
                Product product = context.Products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                    return NotFound();
                context.Products.Remove(product);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Patch
        /// </summary>
        /// <param name="id"></param>
        /// <param name="property"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="400"></response>
        /// <response code="404"></response>
        /// <response code="200"></response>
        /// <response code="500"></response>
        [HttpPatch("{id}/{property}")]
        public ActionResult Patch(string id, string property, [FromBody] JObject request)
        {
            if (!request.ContainsKey(property) || request[property]?.ToString() == "")
                return BadRequest();

            try
            {
                Customer customer = context.Customers.FirstOrDefault(c => c.Id == id);
                if (customer == null)
                    return NotFound();
                customer.GetType().GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).SetValue(customer, request[property].ToString());
                context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }



        }

        /// <summary>
        /// Get customer companies
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="404"></response>
        /// <response code="200"></response>
        /// <response code="500"></response>
        [HttpGet("{id}/companies")]
        public ActionResult GetCustomerCompanies(string id)
        {
            try
            {
                Customer customer = context.Customers.Include(c => c.CompanyAdmins).ThenInclude(ca => ca.Company).AsNoTracking().FirstOrDefault(c => c.Id == id);
                if (customer == null)
                    return NotFound();

                return Ok(customer.CompanyAdmins);

            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            

        }

    }
}
