using System;
using System.Threading.Tasks;
using Chimera_v2.Business;
using Chimera_v2.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Chimera_v2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientBusiness _clientBusiness;

        public ClientController(IClientBusiness clientBusiness)
        {
            _clientBusiness = clientBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_clientBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var client = _clientBusiness.FindById(id);
            if (client == null) return NotFound();
            return Ok(_clientBusiness.FindAll());
        }

        [HttpPost]
        public IActionResult Post([FromBody] ClientDTO client)
        {
            if (client == null) return BadRequest();
            return Ok(_clientBusiness.Create(client));
        }

        [HttpPut]
        public IActionResult Put([FromBody] ClientDTO client)
        {
            if (client == null) return BadRequest();
            return Ok(_clientBusiness.Update(client));
        }

        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            _clientBusiness.Delete(name);
            return NoContent();
        }
    }
}