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
            return Ok(_clientBusiness.FindAll().Result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var client = await _clientBusiness.FindById(id);
            if (client == null) return NotFound();
            return Ok(client);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ClientDTO clientDto)
        {
            if (clientDto == null) return BadRequest();
            return Ok(_clientBusiness.Create(clientDto).Result);
        }

        [HttpPut]
        public IActionResult Put([FromBody] ClientDTO clientDto)
        {
            if (clientDto == null) return BadRequest();
            return Ok(_clientBusiness.Update(clientDto).Result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _clientBusiness.Delete(id);
            return NoContent();
        }
    }
}