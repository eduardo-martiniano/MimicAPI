using Microsoft.AspNetCore.Mvc;
using MimicAPI.Database;
using MimicAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimicAPI.Repositories.Contracts
{
    public class PalavraRepository : IPalavraRepository
    {
        private MimicContext _banco;

        public PalavraRepository(MimicContext banco)
        {
            _banco = banco;
        }

        public void Cadastrar(Palavra palavra)
        {
            palavra.Criado = DateTime.Now.Date;
            _banco.Palavras.Add(palavra);
            _banco.SaveChanges();
        }

        public Palavra Deletar(int id)
        {
            var p = _banco.Palavras.Find(id);
            p.Ativo = false;
            _banco.Palavras.Update(p);
            _banco.SaveChanges();
            return p;
            
        }
        public Palavra Ativar(int id)
        {
            var p = _banco.Palavras.Find(id);
            p.Ativo = true;
            _banco.Palavras.Update(p);
            _banco.SaveChanges();
            return p;
        }

        public Palavra Editar(int id, Palavra palavra)
        {
            palavra.Id = id;
            _banco.Palavras.Update(palavra);
            _banco.SaveChanges();
            return palavra;
        }

        public List<Palavra> ObterTodas(DateTime? data, int? limit, int? offset)
        {
            var query = _banco.Palavras.AsQueryable();
            
            if(data.HasValue)
                query = query.Where(a => a.Criado >= data.Value || a.Atualizado >= data.Value);
            
            if(!(limit.HasValue && offset.HasValue))
                return query.ToList();            
            
            return query.Skip((int)(offset * limit)).Take((int)limit).OrderByDescending(o => o.Atualizado).ToList();        
        }

        public Palavra ObterUma(int id)
        {
            return _banco.Palavras.Find(id);
        }
    }
}