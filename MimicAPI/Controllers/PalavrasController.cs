using Microsoft.AspNetCore.Mvc;
using MimicAPI.Database;
using MimicAPI.Models;
using MimicAPI.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimicAPI.Controllers
{
    [Route("api/palavras")]
    public class PalavrasController : ControllerBase
    {
        private IPalavraRepository _repository;
        public PalavrasController(IPalavraRepository repository )
        {
            _repository = repository;
           
        }
        [Route("")]
        [HttpGet]
        public ActionResult ObterTodas(DateTime? data)
        {
            return Ok(_repository.ObterTodas(data));
        }

       [Route("{id}")]
        [HttpGet]
        public ActionResult ObterUma(int id)
        { 
           var palavra = _repository.ObterUma(id);
            if (palavra == null)
            {
                return NotFound("A palavra não existe");
            }
           return Ok(palavra); 
        }
        
        [Route("")]
        [HttpPost]
        public ActionResult Cadastrar([FromBody] Palavra palavra)
        {
            _repository.Cadastrar(palavra);
            return Ok("Palavra cadastrada com sucesso!");
        }
        
        [Route("{id}")]
        [HttpPut]
        public ActionResult Atualizar(int id,[FromBody] Palavra palavra)
        {
            var p = _repository.ObterUma(id);
            if (p == null)
            {
                return NotFound("A palavra não existe!");
            }
            _repository.Editar(id, palavra);
            return Ok("Palavra editada com sucesso!");
        }
        
        [Route("{id}")]
        [HttpDelete]
        public ActionResult Deletar(int id)
        {
            var p = _repository.ObterUma(id);
            if (p == null)
            {
                return NotFound("A palavra não existe!");
            }
            _repository.Deletar(id);
            return Ok("Palavra desativada com sucesso!");
        }
         
        [Route("ativar/{id}")]
        [HttpPut]
        public ActionResult Ativar(int id)
        {
            var p = _repository.ObterUma(id);
            if (p == null)
            {
                return NotFound("A palavra não existe!");
            }
            _repository.Ativar(id);
            return Ok("Palavra ativada com sucesso!");
        }
        
    }
}
