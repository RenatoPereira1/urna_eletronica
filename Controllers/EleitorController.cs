using Microsoft.AspNetCore.Mvc;
using ProjetoMySQL.Models;
 
namespace ProjetoMySQL.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class EleitorController : ControllerBase
    {
        private BDContexto contexto;
 
        public EleitorController(BDContexto bdContexto)
        {
            contexto = bdContexto;
        }
 
        [HttpGet]
        public List<Eleitor> Listar()
        {
            return contexto.Eleitors.ToList();
        }

        [HttpPost]
        public string Cadastrar([FromBody] Eleitor novoEleitor)
        {
            contexto.Add(novoEleitor);
            contexto.SaveChanges();
            return "Eleitor(a) cadastrado(a) com sucesso!";
        }

        [HttpDelete]
        public string Excluir([FromBody] int id)
        {
            Eleitor dados = contexto.Eleitors.FirstOrDefault(p => p.Id == id);

            if (dados == null)
            {
                return "Não foi encontrado Eleitor para o ID informado";
            }
            else
            {
                contexto.Remove(dados);
                contexto.SaveChanges();

                return "Eleitor(a) removido(a) com sucesso!";
            }
        }


        [HttpPut]
        public string Alterar([FromBody] Eleitor eleitorAtualizado)
        {
            contexto.Update(eleitorAtualizado);
            contexto.SaveChanges();

            return "Eleitor atualizado com sucesso!";
        }


        [HttpGet] 
        public Eleitor Visualizar(int id)
        {
            return contexto.Eleitors.FirstOrDefault(p => p.Id == id);
        }
    }
}