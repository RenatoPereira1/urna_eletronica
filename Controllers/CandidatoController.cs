using Microsoft.AspNetCore.Mvc;
using ProjetoMySQL.Models;
 
namespace ProjetoMySQL.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CandidatoController : ControllerBase
    {
        private BDContexto contexto;
 
        public CandidatoController(BDContexto bdContexto)
        {
            contexto = bdContexto;
        }
 
        [HttpGet]
        public List<Candidato> Listar()
        {
            return contexto.Candidatos.ToList();
        }

        [HttpPost]
        public string Cadastrar([FromBody] Candidato novoCandidato)
        {
            contexto.Add(novoCandidato);
            contexto.SaveChanges();
            return "Candidato(a) cadastrado(a) com sucesso!";
        }

        [HttpDelete]
        public string Excluir([FromBody] int id)
        {
            Candidato dados = contexto.Candidatos.FirstOrDefault(p => p.Id == id);

            if (dados == null)
            {
                return "Não foi encontrado Candidato para o ID informado";
            }
            else
            {
                contexto.Remove(dados);
                contexto.SaveChanges();

                return "Candidato(a) removido(a) com sucesso!";
            }
        }
    }
}