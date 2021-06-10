using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kursach.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using kursach.Server.Helpers;

namespace kursach.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromocodesController : ControllerBase
    {
        private readonly ApplicationContext context;
        private readonly Utils utils;

        public PromocodesController(ApplicationContext ctx)
        {
            utils = new Utils();
            context = ctx;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <response code="404"></response>
        /// <response code="200">Возвращает объект типа promocode</response>
        /// <response code="500"></response>
        [HttpGet]
        public ActionResult GetPromocodeByName([FromQuery] string name)
        {
            try
            {
                var promocode = context.Promocodes.FirstOrDefault(p => p.Name == name);
                if(promocode == null)
                    return NotFound();
                return Ok(promocode);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
