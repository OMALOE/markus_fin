using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using kursach.Shared.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using kursach.Server.Helpers;

namespace kursach.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationContext context;
        private readonly Utils utils;

        public ProductsController(ApplicationContext ctx)
        {
            this.context = ctx;
            utils = new Utils();
        }
        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        /// <respnse code="200">возвращает объект типа products</respnse>
        /// <respnse code="500"></respnse>
        [HttpGet]
        public ActionResult GetAllProducts()
        {
            try
            {
                List<Product> products = context.Products.Include(p => p.Company).Include(p => p.ProductImages).ThenInclude(pi => pi.Image).AsNoTracking().Take(100).ToList();

                return Ok(products);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        /// <summary>
        /// Get product data by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <respnse code="404">Невозможно найти продукт</respnse>
        /// <respnse code="200">возвращает объект типа product</respnse>
        /// <respnse code="500">Ошибка при поиске товара</respnse>
        [HttpGet("{id}")]
        public ActionResult GetProductDataByID(string id)
        {
            try
            {
                Product product = context.Products.Include(p => p.Tag).Include(p => p.ProductImages).
                    ThenInclude(pi => pi.Image).Include(p => p.VariationOptions).AsNoTracking().FirstOrDefault<Product>(p => p.Id == id);
                if (product == null)
                    return NotFound("Unable to find such product");
                if(product.VariationOptions.Count > 0)
                {
                    var variations = context.VariationOptions.Where(vo => vo.Id == product.VariationOptions.FirstOrDefault().Id).Include(vo => vo.Product).ToList();
                    product.VariationOptions = variations;
                }
                return Ok(product);
            }
            catch (Exception)
            {

                return StatusCode(500, "Error occured when searching for product!");
            }

        }

        /// <summary>
        /// Put product Data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <respnse code="400"></respnse>
        /// <respnse code="500">Не можем найти данные продукта</respnse>
        /// <respnse code="200"></respnse>
        /// <respnse code="500"></respnse>
        [HttpPut("{id}")]
        public ActionResult PutProductData(string id, [FromBody] JObject request)
        {
            
            if (!request.ContainsKey("product"))
                return BadRequest("No 'product' key!");

            Product product = JsonConvert.DeserializeObject<Product>(request["product"].ToString());

            try
            {

                Product foundProduct = context.Products.FirstOrDefault<Product>(p => p.Id == id);

                if (foundProduct == null)
                    return StatusCode(500, "Cannot find product data to change!");
            
                //Update all properties of object
                context.Entry(foundProduct).CurrentValues.SetValues(product);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }


        }
        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <respnse code="404"></respnse>
        /// <respnse code="200"></respnse>
        /// <respnse code="500"></respnse>
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(string id)
        {
            try
            {
                Product product = context.Products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                    return NotFound();
                context.Products.Remove(product);
                context.SaveChanges();
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
        /// <respnse code="400"></respnse>
        /// <respnse code="404"></respnse>
        /// <respnse code="200"></respnse>
        /// <respnse code="500"></respnse>
        [HttpPatch("{id}/{property}")]
        public ActionResult Patch(string id, string property, [FromBody] JObject request)
        {
            if (!request.ContainsKey(property) || request[property]?.ToString() == "")
                return BadRequest();

            try
            {
                Product product = context.Products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                    return NotFound();
                product.GetType().GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).SetValue(product, request[property].ToObject(product.GetType().GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(product).GetType()));
                context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }



        }
        /// <summary>
        /// Delete all products
        /// </summary>
        /// <returns></returns>
        /// <respnse code="200">Удаление завершено</respnse>
        /// <respnse code="500">Ошибка. Невозможно удалить.</respnse>
        [HttpDelete]
        public ActionResult DeleteAllProducts()
        {
            try
            {
                var products = context.Products;
                foreach (var product in products)
                {
                    context.Products.Remove(product);
                }
                context.SaveChanges();
                return Ok("Delete complete!");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error! Unable to delete!");
            }
            

        }

        /// <summary>
        /// Post product images
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <respnse code="400"></respnse>
        /// <respnse code="200"></respnse>
        [HttpPost("{id}/images")]
        public ActionResult PostProductImages(string id, [FromBody] JObject request)
        {
            if (!request.ContainsKey("images"))
                return BadRequest();

            List<Image> images = request["images"].ToObject<List<Image>>();

            foreach (var image in images)
            {
                context.ProductImages.Add(new ProductImage() { Imageid = image.Id, Productid = id });
            }
            context.SaveChanges();
            return Ok();
        }

        //Categories
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        /// <respnse code="200">возвращает объект типа categories</respnse>
        /// <respnse code="500"></respnse>
        [HttpGet("categories")]
        public ActionResult GetAllCategories()
        {
            try
            {
                List<Category> categories = context.Categories.Include(c => c.Subcategories).ThenInclude(s => s.TagsSubcategories).ThenInclude(ts => ts.Tag).AsNoTracking().ToList();

                return Ok(categories);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Post new category
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <respnse code="400"></respnse>
        /// <respnse code="400"></respnse>
        /// <respnse code="409"></respnse>
        /// <respnse code="200"></respnse>
        /// <respnse code="500"></respnse>
        [HttpPost("categories")]
        public ActionResult PostNewCategory([FromBody] JObject request)
        {
            if (!request.ContainsKey("category"))
                return BadRequest();

            Category category = request["category"].ToObject<Category>();
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(category, new ValidationContext(category), results))
                return BadRequest(results);

            try
            {
                Category foundCategory = context.Categories.FirstOrDefault(c => c.Id == category.Id && c.Name == category.Name);
                if (foundCategory != null)
                    return Conflict();

                context.Categories.Add(category);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Get category data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <respnse code="404"></respnse>
        /// <respnse code="200">возвращает объект типа category</respnse>
        /// <respnse code="500"></respnse>
        [HttpGet("categories/{id}")]
        public ActionResult GetCategoryData(string id)
        {
            try
            {
                Category category = context.Categories.Include(c => c.Subcategories).ThenInclude(s => s.TagsSubcategories).ThenInclude(ts => ts.Tag).AsNoTracking().FirstOrDefault(c => c.Id == id);
                if (category == null)
                    return NotFound();

                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Put update category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <respnse code="400"></respnse>
        /// <respnse code="400"></respnse>
        /// <respnse code="200"></respnse>
        /// <respnse code="500"></respnse>
        [HttpPut("categories/{id}")]
        public ActionResult PutUpdateCategory(string id, [FromBody] JObject request)
        {
            if (!request.ContainsKey("category"))
                return BadRequest();
            Category category = request["category"].ToObject<Category>();
            List<ValidationResult> results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(category, new ValidationContext(category), results) || category?.Id != id )
                return BadRequest(results);

            try
            {


                Category foundCategory = context.Categories.FirstOrDefault(c => c.Id == category.Id);
                context.Entry(foundCategory).CurrentValues.SetValues(category);
                context.SaveChanges();
                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <respnse code="404"></respnse>
        /// <respnse code="200"></respnse>
        /// <respnse code="500"></respnse>
        [HttpDelete("categories/{id}")]
        public ActionResult DeleteCategory(string id)
        {
            try
            {
                Category category = context.Categories.FirstOrDefault(c => c.Id == id);
                if (category == null)
                    return NotFound();
                context.Categories.Remove(category);
                context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Get all sub categories
        /// </summary>
        /// <returns></returns>
        /// <respnse code="200">возвращает объект типа subcategory</respnse>
        /// <respnse code="500"></respnse>
        [HttpGet("categories/subcategories")]
        public ActionResult GetAllSubcategories()
        {
            try
            {
                List<Subcategory> subcategories = context.Subcategories.Include(s => s.TagsSubcategories).ThenInclude(ts => ts.Tag).AsNoTracking().ToList();

                return Ok(subcategories);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Post new subcategory
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <respnse code="400"></respnse>
        /// <respnse code="400"></respnse>
        /// <respnse code="409"></respnse>
        /// <respnse code="200"></respnse>
        /// <respnse code="500"></respnse>
        [HttpPost("categories/subcategories")]
        public ActionResult PostNewSubcategory([FromBody] JObject request)
        {
            if (!request.ContainsKey("subcategory"))
                return BadRequest();

            Subcategory subcategory = request["subcategory"].ToObject<Subcategory>();
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(subcategory, new ValidationContext(subcategory), results))
                return BadRequest(results);

            try
            {
                Subcategory foundCategory = context.Subcategories.FirstOrDefault(c => c.Id == subcategory.Id && c.Name == subcategory.Name);
                if (foundCategory != null)
                    return Conflict();

                context.Subcategories.Add(subcategory);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        /// <summary>
        /// Get subcategory data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <respnse code="404"></respnse>
        /// <respnse code="200">возвращает объект типа subcategory</respnse>
        /// <respnse code="500"></respnse>
        [HttpGet("categories/subcategories/{id}")]
        public ActionResult GetSubcategoryData(string id)
        {
            try
            {
                Subcategory subcategory = context.Subcategories.Include(s => s.TagsSubcategories).ThenInclude(ts => ts.Tag).AsNoTracking().FirstOrDefault(c => c.Id == id);
                if (subcategory == null)
                    return NotFound();

                return Ok(subcategory);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Put update subcategory
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <respnse code="400"></respnse>
        /// <respnse code="400"></respnse>
        /// <respnse code="200"></respnse>
        /// <respnse code="500"></respnse>
        [HttpPut("categories/subcategories/{id}")]
        public ActionResult PutUpdateSubcategory(string id, [FromBody] JObject request)
        {
            if (!request.ContainsKey("subcategory"))
                return BadRequest();
            Subcategory subcategory = request["subcategory"].ToObject<Subcategory>();
            List<ValidationResult> results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(subcategory, new ValidationContext(subcategory), results) || subcategory?.Id != id)
                return BadRequest(results);

            try
            {


                Subcategory foundCategory = context.Subcategories.FirstOrDefault(c => c.Id == subcategory.Id);
                context.Entry(foundCategory).CurrentValues.SetValues(subcategory);
                context.SaveChanges();
                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Delete subcategory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <respnse code="404"></respnse>
        /// <respnse code="200"></respnse>
        /// <respnse code="500"></respnse>
        [HttpDelete("categories/subcategories/{id}")]
        public ActionResult DeleteSubcategory(string id)
        {
            try
            {
                Subcategory subcategory = context.Subcategories.FirstOrDefault(c => c.Id == id);
                if (subcategory == null)
                    return NotFound();
                context.Subcategories.Remove(subcategory);
                context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Get all tags
        /// </summary>
        /// <returns></returns>
        /// <respnse code="200">возвращает объект типа tags</respnse>
        /// <respnse code="500"></respnse>
        [HttpGet("categories/subcategories/tags")]
        public ActionResult GetAllTags()
        {
            try
            {
                List<Tag> tags = context.Tags.ToList();

                return Ok(tags);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Get all category tags
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <respnse code="200"></respnse>
        /// <respnse code="500"></respnse>
        [HttpGet("categories/subcategories/{id}/tags")]
        public ActionResult GetAllSubcategoryTags(string id)
        {
            try
            {
                List<TagsSubcategory> tagSub = context.TagsSubcategories.Include(ts => ts.Tag).AsNoTracking().Where(ts => ts.SubcategoryId == id).ToList() ;

                return Ok(tagSub.Select(ts => ts.Tag).ToList());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Post new tag
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <respnse code="400"></respnse>
        /// <respnse code="400"></respnse>
        /// <respnse code="404"></respnse>
        /// <respnse code="409"></respnse>
        /// <respnse code="200"></respnse>
        /// <respnse code="500"></respnse>
        [HttpPost("categories/subcategories/{id}/tags")]
        public ActionResult PostNewTag(string id, [FromBody] JObject request)
        {
            if (!request.ContainsKey("tag"))
                return BadRequest();

            Tag tag = request["tag"].ToObject<Tag>();
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(tag, new ValidationContext(tag), results))
                return BadRequest(results);

            try
            {
                if (context.Subcategories.FirstOrDefault(s => s.Id == id) == null)
                    return NotFound();

                Tag foundTag = context.Tags.FirstOrDefault(t => t.Id == tag.Id && t.Name == tag.Name);
                if (foundTag != null)
                    return Conflict();
                context.Tags.Add(tag);
                context.TagsSubcategories.Add(new TagsSubcategory() {TagId=tag.Id, SubcategoryId=id});
                context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Delete subcategory tag
        /// </summary>
        /// <param name="subcategoryId"></param>
        /// <param name="tagId"></param>
        /// <returns></returns>
        /// <respnse code="404"></respnse>
        /// <respnse code="200"></respnse>
        /// <respnse code="500"></respnse>
        [HttpDelete("categories/subcategories/{subcategoryId}/tags/{tagId}")]
        public ActionResult DeleeteSubcategoryTag(string subcategoryId, string tagId)
        {
            try
            {
                TagsSubcategory tagSub = context.TagsSubcategories.FirstOrDefault(ts => ts.TagId == tagId && ts.SubcategoryId == subcategoryId);
                if (tagSub == null)
                    return NotFound();
                context.TagsSubcategories.Remove(tagSub);
                context.SaveChanges();
                
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
            
        }

        /// <summary>
        /// Get tag data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <respnse code="404"></respnse>
        /// <respnse code="200">возвращает объект типа tag</respnse>
        /// <respnse code="500"></respnse>
        [HttpGet("categories/subcategories/tags/{id}")]
        public ActionResult GetTagData(string id)
        {
            try
            {
                Tag tag = context.Tags.Include(t => t.TagsSubcategories).ThenInclude(ts => ts.Subcategory).AsNoTracking().FirstOrDefault(t => t.Id == id);
                if (tag == null)
                    return NotFound();

                return Ok(tag);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Put update tag
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <respnse code="400"></respnse>
        /// <respnse code="400"></respnse>
        /// <respnse code="200"></respnse>
        /// <respnse code="500"></respnse>
        [HttpPut("categories/subcategories/tags/{id}")]
        public ActionResult PutUpdateTag(string id, [FromBody] JObject request)
        {
            if (!request.ContainsKey("tag"))
                return BadRequest();
            Tag tag = request["tag"].ToObject<Tag>();
            List<ValidationResult> results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(tag, new ValidationContext(tag), results) || tag?.Id != id)
                return BadRequest(results);

            try
            {


                Tag foundTag = context.Tags.FirstOrDefault(c => c.Id == tag.Id);
                context.Entry(foundTag).CurrentValues.SetValues(tag);
                context.SaveChanges();
                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Patch add tag view
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <respnse code="404"></respnse>
        /// <respnse code="200"></respnse>
        /// <respnse code="500"></respnse>
        [HttpPatch("categories/subcategories/tags/{id}/views")]
        public ActionResult PatchAddTagView(string id, [FromBody] JObject request)
        {
            try
            {
                Tag tag = context.Tags.FirstOrDefault(t => t.Id == id);
                if (tag == null)
                    return NotFound();
                tag.Views += 1;

                context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Delete tag
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <respnse code="404"></respnse>
        /// <respnse code="200"></respnse>
        /// <respnse code="500"></respnse>
        [HttpDelete("categories/subcategories/tags/{id}")]
        public ActionResult DeleteTag(string id)
        {
            try
            {
                Tag tag = context.Tags.FirstOrDefault(t => t.Id == id);
                if (tag == null)
                    return NotFound();
                context.Tags.Remove(tag);
                context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Get all tags products
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <respnse code="200">возвращает объект типа products</respnse>
        /// <respnse code="500"></respnse>
        [HttpGet("categories/subcategories/tags/{id}/products")]
        public ActionResult GetAllTagsProducts(string id)
        {
            try
            {
                List<Product> products = context.Products.Include(p => p.Company).Include(p => p.ProductImages).ThenInclude(pi => pi.Image).Where(p => p.Tagid == id).AsNoTracking().ToList();
                return Ok(products);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        //references
        /// <summary>
        /// Get all references
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        /// <respnse code="200">возвращает объект типа result</respnse>
        /// <respnse code="200">возвращает объект типа allReferences</respnse>
        /// <respnse code="500"></respnse>
        [HttpGet("references")]
        public ActionResult GetAllReferences([FromQuery(Name = "productName")] string productName)
        {
            try
            {
                var allReferences = context.ReferenceProducts.ToList();
                if(productName != null)
                {
                    List<ReferenceProduct> result = new List<ReferenceProduct>();
                    foreach (var item in allReferences)
                    {
                        if (utils.TryCompareStrings(item.Name, productName))
                            result.Add(item);
                    }
                    return Ok(result);
                }
                return Ok(allReferences);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Create new reference
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <respnse code="400"></respnse>
        /// <respnse code="400"></respnse>
        /// <respnse code="409"></respnse>
        /// <respnse code="200"></respnse>
        /// <respnse code="500"></respnse>
        [HttpPost("references")]
        public ActionResult CreateNewReference([FromBody] JObject request)
        {
            if (!request.ContainsKey("reference"))
                return BadRequest();

            ReferenceProduct reference = request["reference"].ToObject<ReferenceProduct>();

            List<ValidationResult> results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(reference, new ValidationContext(reference), results))
                return BadRequest(results);
            try
            {
                var foundReference = context.ReferenceProducts.FirstOrDefault(rp => rp.Id == reference.Id || rp.Name == reference.Name || rp == reference);
                if (foundReference != null)
                    return Conflict();

                context.ReferenceProducts.Add(reference);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Delete reference
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <respnse code="404"></respnse>
        /// <respnse code="200"></respnse>
        /// <respnse code="500"></respnse>
        [HttpDelete("references/{id}")]
        public ActionResult DeleteReference(string id)
        {
            try
            {
                ReferenceProduct reference = context.ReferenceProducts.FirstOrDefault(rp => rp.Id == id);
                if (reference == null)
                    return NotFound();
                context.ReferenceProducts.Remove(reference);
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
