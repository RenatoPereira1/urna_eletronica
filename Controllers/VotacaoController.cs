using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ProjetoMySQL.Models;
 
namespace ProjetoMySQL.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class VotacaoController : ControllerBase
    {
        private BDContexto contexto;
 
        public VotacaoController(BDContexto bdContexto)
        {
            contexto = bdContexto;
        }

        [HttpGet]
        public List<Votacao> Listar()
        {
            return contexto.Votacoes.Include(v => v.IdEleitorNavigation).Include(v => v.IdCandidatoNavigation).OrderBy(v => v.Id).Select
                (
                    v => new Votacao 
                    { 
                        Id = v.Id,
                        IdCandidato = v.IdCandidato,
                        IdEleitor = v.IdEleitor,
                        Zona = v.Zona,
                        Secao = v.Secao,
                        IdEleitorNavigation = new Eleitor 
                        { 
                            Id = v.IdEleitorNavigation.Id, 
                            Nome = v.IdEleitorNavigation.Nome,
                            Cpf = v.IdEleitorNavigation.Cpf
                        }, 
                        IdCandidatoNavigation = new Candidato 
                        { 
                            Id = v.IdCandidatoNavigation.Id, 
                            Nome = v.IdCandidatoNavigation.Nome,
                            Numero = v.IdCandidatoNavigation.Numero,
                            Partido = v.IdCandidatoNavigation.Partido,
                        } 
                    }
                ).ToList();
        }
    }
}

