using System;
using System.Threading.Tasks;
using Chimera_v2.Business;
using Chimera_v2.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Chimera_v2.Controllers
{
    [ApiController]
    [Route("api/[controller]/v2")]
    public class ClientController : ControllerBase
    {
        private readonly IClientBusiness _clientBusiness;

        public ClientController(IClientBusiness clientBusiness)
        {
            _clientBusiness = clientBusiness;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_clientBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var client = _clientBusiness.FindById(id);
            if (client == null) return NotFound();
            return Ok(client);
        }

        [HttpPost]
        public ActionResult Post([FromBody] ClientDTO clientDto)
        {
            if (clientDto == null) return BadRequest();
            return Ok(_clientBusiness.Create(clientDto));
        }

        [HttpPut]
        public ActionResult Put([FromBody] ClientDTO clientDto)
        {
            if (clientDto.Adress.Guid == null) return BadRequest();
            var client = _clientBusiness.Update(clientDto);
            return Ok(client);
        }
        
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            _clientBusiness.Delete(id);
            return NoContent();
        }
    }
}