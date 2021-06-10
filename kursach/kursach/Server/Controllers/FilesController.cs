using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using kursach.Shared.Models;
using System.IO;
using Newtonsoft.Json.Linq;

namespace kursach.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly ApplicationContext context;

        public FilesController(ApplicationContext ctx)
        {
            this.context = ctx;

        }
        /// <summary>
        /// Get image
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <response code="404"></response>
        /// <response code="206"></response>
        [HttpGet("images/{folder}/{name}")]
        public ActionResult GetImage(string folder, string name)
        {
            string path = $"./files/images/{folder}/{name}";
            if (!System.IO.File.Exists(path))
                return NotFound();

            byte[] image = System.IO.File.ReadAllBytes(path);
            
            return File(image, "image/jpeg");
        }

        /// <summary>
        /// Post new image file
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        /// <response code="400"></response>
        /// <response code="409"></response>
        /// <response code="200">Возвращает объект типа image</response>
        /// <response code="500"></response>
        [HttpPost("images")]
        public ActionResult PostNewImageFile([FromForm] IFormFile files)
        {
            if (Request.Form.Files.Count == 0)
                return BadRequest();
            var file = Request.Form.Files[0];
            string fileName = DateTime.Now.ToFileTime().ToString();
            string fileExt = file.FileName.Split(".")[1];
            string fullFileName = $"{fileName}.{fileExt}";
            string path = $"./files/images/1/{fullFileName}";
            if (System.IO.File.Exists(path))
                return Conflict();

            //here should be a processing and compression

            using(var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            path = $"{Path.GetDirectoryName(path).Replace(".", "")}/{Path.GetFileName(path)}";
            try
            {
                string imageId = Guid.NewGuid().ToString();
                Image image = new Image() { Id = imageId, Path = path };
                context.Images.Add(image);
                context.SaveChanges();
                return Ok(image);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Delete image
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <response code="404"></response>
        /// <response code="200"></response>
        /// <response code="500"></response>
        [HttpDelete("images/{folder}/{name}")]
        public ActionResult DeleteImage(string folder, string name)
        {
            string path = $"./files/images/{folder}/{name}";
            try
            {
                if (!System.IO.File.Exists(path))
                    return NotFound();

                System.IO.File.Delete(path);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

    }
}
