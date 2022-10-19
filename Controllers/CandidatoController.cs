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
        public bool Excluir([FromBody] int id)
        {
            Candidato dados = contexto.Candidatos.FirstOrDefault(p => p.Id == id);

            if (dados == null)
            {
                return false;
            }
            else
            {
                try
                {
                    contexto.Remove(dados);
                    contexto.SaveChanges();

                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }

            }
        }

        [HttpPut]
        public string Alterar([FromBody] Candidato candidatoAtualizado)
        {
            contexto.Update(candidatoAtualizado);
            contexto.SaveChanges();

            return "Candidato atualizado com sucesso!";
        }


        [HttpGet] 
        public Candidato Visualizar(int id)
        {
            return contexto.Candidatos.FirstOrDefault(p => p.Id == id);
        }
    }
}