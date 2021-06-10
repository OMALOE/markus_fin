using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using kursach.Shared.Models;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using kursach.Server.Helpers;
using Swashbuckle.AspNetCore.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kursach.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ApplicationContext context;
        private readonly Utils utils;

        public CompaniesController(ApplicationContext ctx)
        {
            utils = new Utils();
            this.context = ctx;
        }

        /// <summary>
        /// Get all copmanies
        /// </summary>
        /// <returns>Array of companies</returns>
        /// <response code='200'>Retuns array of all companies</response>
        [HttpGet]
        public ActionResult GetAllCompnanies()
        {
            return Ok(context.Companies.ToList());
        }

        /// <summary>
        /// Get copmany by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="404">Не найдено данных компанни</response>
        /// <response code="200">Возвращает обьект типа Company</response>
        /// <response code="500">Внутренняя ошибка</response>
        [HttpGet("{id}")]
        public ActionResult GetCompanyPublicDataByID(string id)
        {
            try
            {
                Company company = context.Companies.FirstOrDefault(c => c.Id == id);

                if (company == null)
                    return NotFound($"Unable to find compnay with id {id}");

                return Ok(company);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong.");
                throw;
            }
        }

        /// <summary>
        /// Create new company
        /// </summary>
        /// <param name="request"></param>     
        /// <remarks>
        /// Примерный запроc
        /// 
        ///     Post /
        ///     {
        ///         "company": {...}, (класс Company)
        ///         "owner": {...} (класс Customer)
        ///     }
        /// </remarks>
        /// 
        /// <returns></returns>
        /// <response code="400">Плохой запрос</response>
        /// <response code="400">Плохой запрос</response>
        /// <response code="409">Конфликт</response>
        /// <response code="200"></response>
        /// <response code="500"></response>
        [HttpPost]
        public ActionResult PostNewCompany([FromBody] JObject request)
        {
            if (!request.ContainsKey("company") || !request.ContainsKey("owner"))
                return BadRequest();
            try
            {
                Company company = JsonConvert.DeserializeObject<Company>(request["company"].ToString());
                var results = new List<ValidationResult>();
                if (!Validator.TryValidateObject(company, new ValidationContext(company), results))
                    return BadRequest(results);

                if (context.Companies.FirstOrDefault(c => c.Id == company.Id || c.PublicName == company.PublicName) != null)
                    return Conflict();

                context.Companies.Add(company);

                CompanyAdmin owner = JsonConvert.DeserializeObject<CompanyAdmin>(request["owner"].ToString());
                context.CompanyAdmins.Add(owner);

                context.SaveChanges();
                
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        /// <summary>
        /// Get all company products
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="404">Не удалось найти такую компанию</response>
        /// <response code="200">Возвращает объект типа products</response>
        /// <response code="500">Ошибка. Невозможно получить все продукты</response>
        [HttpGet("{id}/products")]
        public ActionResult GetAllCompanyProductsByID(string id)
        {
            try
            {
                if (context.Companies.FirstOrDefault(c => c.Id == id) == null)
                    return NotFound("Unable to find such company!");
                List<Product> products = context.Products.Where(p => p.Companyid == id).Include(p => p.ProductImages).ThenInclude(pi => pi.Image).Include(p => p.Company).AsNoTracking().ToList();
                return Ok(products);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error! Unable to get all products. {e.Message}");
                throw;
            }


        }

        /// <summary>
        /// Add product to company
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="400">Неверные данные почтового запроса. Не удалось найти продукт</response>
        /// <response code="400"></response>
        /// <response code="400"></response>
        /// <response code="404"></response>
        /// <response code="409"></response>
        /// <response code="200">Продукт успешно создан</response>
        /// <response code="500">Неудалось создать продукт</response>
        [HttpPost("{id}/products")]
        public ActionResult PostProduct(string id, [FromBody] JObject request)
        {
            Console.WriteLine(request.ToString());
            if (!request.ContainsKey("product"))
                return BadRequest("Invalind post request data! Unable to find 'product'!");

            Product product = JsonConvert.DeserializeObject<Product>(request["product"].ToString());
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(product, new ValidationContext(product), results))
                return BadRequest(results);
            try
            {
                if (product.Companyid != id)
                    return BadRequest();

                Company company = context.Companies.Include(p => p.Products).FirstOrDefault<Company>(c => c.Id == product.Companyid);
                if (company == null)
                    return NotFound("company");

                if (company.Products.FirstOrDefault<Product>(p => p.Id == product.Id) != null)
                    return Conflict();


                if(string.IsNullOrEmpty(product.Referenceid))
                {
                    var references = context.ReferenceProducts.AsNoTracking().ToList();
                    ReferenceProduct reference = new ReferenceProduct();
                    foreach (ReferenceProduct item in references)
                    {
                        if (utils.TryCompareStrings(item.Name, product.Fullname))
                        {
                            reference = item;
                            break;
                        }
                    }
                    product.Referenceid = reference?.Id;
                }

                if(string.IsNullOrEmpty(product.Referenceid))
                {
                    string refId = Guid.NewGuid().ToString();
                    context.ReferenceProducts.Add(new ReferenceProduct() { Id = refId, Description = product.Description, Name = product.Fullname, Tagid = product.Tagid, Options="" });
                    product.Referenceid = refId;
                }
                product.CreationDate = DateTime.Now;
                context.Products.Add(product);
                if(product.VariationOptions.Count > 0)
                {
                    context.VariationOptions.AddRange(product.VariationOptions);
                }
                context.SaveChanges();
                return Ok("Product successfully created!");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Unable to create product, {e.ToString()}");
            }
        }

        /// <summary>
        /// Create test data (just for fun)
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Возвращает объект типа companies</response>
        /// <response code="500">Не удалось создать тестовые данные</response>
        [HttpGet("test_data")]
        public ActionResult CreateTestData()
        {
            try
            {
                List<Company> companies = new List<Company> {
                    new Company{Id=Guid.NewGuid().ToString(), PublicName="bimba", LegalName="bomba", City="Moscow", Country="Russia", DeliveryDelay=1, DeliveryRegion="Country", Description="cool", LegalAddress="ddd", RealAddress="bbb", LegalType="OOO", Psrn="11", Tin="12", Trrc="13", WorkHoursEnd=DateTime.Now, WorkHoursStart=DateTime.Now},
                    new Company{Id=Guid.NewGuid().ToString(), PublicName="bimba", LegalName="bomba", City="Moscow", Country="Russia", DeliveryDelay=1, DeliveryRegion="Country", Description="cool", LegalAddress="ddd", RealAddress="bbb", LegalType="OOO", Psrn="11", Tin="12", Trrc="13", WorkHoursEnd=DateTime.Now, WorkHoursStart=DateTime.Now},
                    new Company{Id=Guid.NewGuid().ToString(), PublicName="bimba", LegalName="bomba", City="Moscow", Country="Russia", DeliveryDelay=1, DeliveryRegion="Country", Description="cool", LegalAddress="ddd", RealAddress="bbb", LegalType="OOO", Psrn="11", Tin="12", Trrc="13", WorkHoursEnd=DateTime.Now, WorkHoursStart=DateTime.Now},
                };
                context.Companies.AddRange(companies);
                context.SaveChanges();
                return Ok(companies);
            }
            catch (Exception)
            {
                return StatusCode(500, "Unable to create test data!");
                throw;
            }
        }

        /// <summary>
        /// Get All copmany orders
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="404">Компания не найдена</response>
        /// <response code="200">Возвращает объект типа orders</response>
        /// <response code="500">Внутренняя ошибка</response>
        [HttpGet("{id}/orders")]
        public ActionResult GetAllOrdersById(string id)
        {
            try
            {
                Company company = context.Companies.FirstOrDefault<Company>(c => c.Id == id);
                if (company == null)
                    return NotFound("Company not found!");
                //var orderProducts = context.OrderProducts.Include(op => op.Product).AsNoTracking().Where(op => op.Product.Companyid == id).ToList();
                var orders = context.Orders.Include(o => o.OrderProducts.Where(op => op.Product.Companyid == id)).ThenInclude(op => op.Product).AsNoTracking().ToList();
                orders = orders.Except(orders.Where(o => o.OrderProducts.Count == 0)).ToList(); //дичь полная но почему-то появляются заказы не этой компании...
                Console.WriteLine(orders);
                Console.WriteLine(id);
                return Ok(orders);
                
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error! Something went wrong! {e.Message}");
            }


        }

        /// <summary>
        /// Get one order (just for testing)
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet("orders/test")]
        public ActionResult GetTestOrder()
        {
            return Ok(context.Products.Where(p => p.Companyid == "b871e094-4acf-45ba-a00f-96070a45af39").ToList());
        }

        /// <summary>
        /// Get all company promocodes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="404">Ошибка</response>
        /// <response code="200">Возвращает объект типа promocodes</response>
        /// <response code="500"></response>
        [HttpGet("{id}/promocodes")]
        public ActionResult GetAllPromocodes(string id)
        {
            try
            {

                Company company = context.Companies.Include(c => c.Promocodes).FirstOrDefault(c => c.Id == id);
                if (company == null)
                    return NotFound();
                List<Promocode> promocodes = context.Promocodes.Where(p => p.Companyid == id).AsNoTracking().ToList();
                return Ok(promocodes);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Add promocode to company
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="400"></response>
        /// <response code="400"></response>
        /// <response code="404"></response>
        /// <response code="409"></response>
        /// <response code="200"></response>
        /// <response code="500"></response>
        [HttpPost("{id}/promocodes")]
        public ActionResult PostNewPromocode(string id, [FromBody] JObject request)
        {
            if (!request.ContainsKey("promocode"))
                return BadRequest();
            Promocode promocode = request["promocode"].ToObject<Promocode>();
            List<ValidationResult> results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(promocode, new ValidationContext(promocode), results))
                return BadRequest(results);

            try
            {
                Company company = context.Companies.Include(c => c.Promocodes).FirstOrDefault(c => c.Id == id);
                if (company == null)
                    return NotFound();
                if (company.Promocodes.ToList().Exists(p => p.Name.ToLower() == promocode.Name.ToLower()))
                    return Conflict();

                context.Promocodes.Add(promocode);
                context.SaveChanges();

                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        /// <summary>
        /// Update promocode all data
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="promocodeId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="400"></response>
        /// <response code="400"></response>
        /// <response code="404"></response>
        /// <response code="404"></response>
        /// <response code="200"></response>
        /// <response code="500"></response>
        [HttpPut("{companyId}/promocodes/{promocodeId}")]
        public ActionResult PutUpdatePromocode(string companyId, string promocodeId, [FromBody] JObject request)
        {
            if (!request.ContainsKey("promocode"))
                return BadRequest();
            Promocode promocode = request["promocode"].ToObject<Promocode>();
            List<ValidationResult> results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(promocode, new ValidationContext(promocode), results) || promocode?.Id != promocodeId || promocode?.Companyid != companyId)
                return BadRequest(results);

            try
            {
                Company company = context.Companies.Include(c => c.Promocodes).FirstOrDefault(c => c.Id == companyId);
                if (company == null)
                    return NotFound();

                if (!company.Promocodes.ToList().Exists(p => p.Id == promocode.Id))
                    return NotFound();
                Promocode foundPromo = company.Promocodes.FirstOrDefault(p => p.Id == promocode.Id);
                context.Entry(foundPromo).CurrentValues.SetValues(promocode);
                context.SaveChanges();
                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }
        /// <summary>
        /// Delete promocode
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="promocodeId"></param>
        /// <returns></returns>
        /// <response code="404"></response>
        /// <response code="200"></response>
        /// <response code="500"></response>
        [HttpDelete("{companyId}/promocodes/{promocodeId}")]
        public ActionResult DeletePromocode(string companyId, string promocodeId)
        {

            try
            {
                Promocode promocode = context.Promocodes.Where(p => p.Id == promocodeId && p.Companyid == companyId).FirstOrDefault();

                if (promocode == null)
                    return NotFound();

                context.Promocodes.Remove(promocode);
                context.SaveChanges();
                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        /// <summary>
        /// Get all company admins
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="404"></response>
        /// <response code="200"></response>
        /// <response code="500"></response>
        [HttpGet("{id}/admins")]
        public ActionResult GetAllCompanyAdmins(string id)
        {
            try
            {
                Company company = context.Companies.FirstOrDefault(c => c.Id == id);
                if (company == null)
                    return NotFound();

                List<CompanyAdmin> admins = context.CompanyAdmins.Where(ca => ca.Companyid == id).AsNoTracking().ToList();
                return Ok(admins);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        /// <summary>
        /// Post new admin
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="400"></response>
        /// <response code="404"></response>
        /// <response code="409"></response>
        /// <response code="200"></response>
        /// <response code="500"></response>
        [HttpPost("{id}/admins")]
        public ActionResult PostNewAdmin(string id, [FromBody] JObject request)
        {
            if (!request.ContainsKey("customerId") || !request.ContainsKey("role"))
                return BadRequest();
            CompanyAdmin admin = new CompanyAdmin() { Companyid = id, Customerid = request["customerId"].ToString(), Role = request["role"].ToString() };

            try
            {
                Company foundCompany = context.Companies.Include(c => c.CompanyAdmins).AsNoTracking().FirstOrDefault(c => c.Id == id);
                if (foundCompany == null)
                    return NotFound();
                if (foundCompany.CompanyAdmins.FirstOrDefault(ca => ca.Customerid == admin.Customerid) != null)
                    return Conflict();

                context.CompanyAdmins.Add(admin);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        /// <summary>
        /// Update admin role
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="adminId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="400"></response>
        /// <response code="404"></response>
        /// <response code="200"></response>
        /// <response code="500"></response>
        [HttpPatch("{companyId}/admins/{adminId}")]
        public ActionResult UpdateAdminRole(string companyId, string adminId, [FromBody] JObject request)
        {
            if (!request.ContainsKey("role"))
                return BadRequest();
            try
            {
                CompanyAdmin admin = context.CompanyAdmins.Where(ca => ca.Companyid == companyId && ca.Customerid == adminId).FirstOrDefault();

                if (admin == null)
                    return NotFound();


                admin.Role = request["role"].ToString();
                context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        /// <summary>
        /// Delete admin from company
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="adminId"></param>
        /// <returns></returns>
        /// <response code="404"></response>
        /// <response code="200"></response>
        /// <response code="500"></response>
        [HttpDelete("{companyId}/admins/{adminId}")]
        public ActionResult DeleteAdminFromCompany(string companyId, string adminId)
        {
            try
            {
                CompanyAdmin admin = context.CompanyAdmins.FirstOrDefault(ca => ca.Companyid == companyId && ca.Customerid == adminId);
                if (admin == null)
                    return NotFound();

                context.CompanyAdmins.Remove(admin);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        
    }
}
