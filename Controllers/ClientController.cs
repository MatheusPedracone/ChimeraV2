using System;
using Chimera_v2.Business;
using Chimera_v2.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chimera_v2.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientBusiness _clientBusiness;

        public ClientController(IClientBusiness clientBusiness)
        {
            _clientBusiness = clientBusiness;
        }

        // busca todos os clients
        [HttpGet]
        [Authorize]
        public ActionResult Get()
        {
            var clients = _clientBusiness.FindAll();
            if (clients == null)
            {
                return NotFound(new { erro = "Nenhum cliente encontrado!" });
            }
            return Ok(clients);
        }

        // busca um client pelo id
        [HttpGet("{id}")]
        [Authorize]

        public ActionResult Get(Guid id)
        {
            var client = _clientBusiness.FindById(id);
            if (client == null)
            {
                return NotFound(new { erro = "Cliente não encontrado!" });
            }
            return Ok(client);
        }

        //criação de um novo client
        [HttpPost]
        [Authorize]
        public ActionResult Post([FromBody] ClientDTO clientDto)
        {
            if (clientDto == null) return BadRequest();
            try
            {
                var newClientDto = _clientBusiness.Create(clientDto);
                return Ok(newClientDto);
            }
            catch (Exception)
            {
                return BadRequest(new { Erro = "Já existe um Cliente cadastrado com esse nome!" });
            }
        }

        // atualização de um client
        [HttpPut]
        [Authorize]

        public ActionResult Put([FromBody] ClientDTO clientDto)
        {
            if (clientDto.Adress.Guid == null) return BadRequest();
            try
            {
                var client = _clientBusiness.Update(clientDto);
                return Ok(client);
            }
            catch (Exception)
            {
                return BadRequest(new { Erro = "Erro ao tentar atualizar as informações do cliente!" });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]

        public ActionResult Delete(Guid id)
        {
            _clientBusiness.Delete(id);
            return NoContent();
        }
    }
}