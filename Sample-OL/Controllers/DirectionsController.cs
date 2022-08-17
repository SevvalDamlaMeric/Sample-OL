using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Sample_OL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sample_OL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectionsController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        private string fileName;

        public DirectionsController(IConfiguration configuration)
        {
            Configuration = configuration;
            fileName = Configuration.GetSection("FileName").Value;
        }

        [HttpGet]
        public List<Direction> GetDirection()
        {
            if (System.IO.File.Exists(fileName))
            {
                string json = System.IO.File.ReadAllText(fileName);
                var jsonObject = JsonConvert.DeserializeObject<List<Direction>>(json);

                return jsonObject;
            }

            return null;
        }

        [HttpPost]
        public IActionResult CreateDirection(Direction direction)
        {
            if (!System.IO.File.Exists(fileName))
            {
                using (FileStream fs = System.IO.File.Create(fileName)) ;
            }

            string json = System.IO.File.ReadAllText(fileName);  

            var jsonObject = JsonConvert.DeserializeObject<List<Direction>>(json);

            if (jsonObject == null)
            {
                jsonObject = new List<Direction>();
            }

            jsonObject.Add(direction);

            string jsonSerialize = System.Text.Json.JsonSerializer.Serialize(jsonObject);
            System.IO.File.WriteAllText(fileName, jsonSerialize);

            return Ok();
        }
    }
}
