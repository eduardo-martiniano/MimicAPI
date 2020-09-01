using Microsoft.AspNetCore.Mvc;
using MimicAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimicAPI.Repositories.Contracts
{
    public interface IPalavraRepository
    {
        List<Palavra> ObterTodas(DateTime? data, int limit, int offset);
        Palavra ObterUma(int id);
        void Cadastrar(Palavra palavra);
        Palavra Editar(int id, [FromBody] Palavra palavra);
        Palavra Deletar(int id);
        Palavra Ativar(int id);
        
    }
}
