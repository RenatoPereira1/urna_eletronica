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
        public List<Eleitor> Listar(string? order = "padrao")
        {
            if(order == "c"){
                return contexto.Eleitores.OrderBy(c => c.Nome).ToList();
            }

            else if(order == "d"){
                return contexto.Eleitores.OrderByDescending(c => c.Nome).ToList();
            }

            else{
                return contexto.Eleitores.OrderBy(c => c.Id).ToList();
            }
            
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
            Eleitor dados = contexto.Eleitores.FirstOrDefault(p => p.Id == id);

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
            return contexto.Eleitores.FirstOrDefault(p => p.Id == id);
        }


        [HttpGet] 
        public Eleitor BuscaPorCpf(string cpf)
        {
            return contexto.Eleitores.FirstOrDefault(p => p.Cpf == cpf);
        }
        [HttpGet]
        public List<Eleitor> ListarPorNome(string nome)
        {
            return contexto.Eleitores.Where(p => p.Nome == nome).Select
            (
                p => new Eleitor 
                { 
                    Id = p.Id,
                    Cpf = p.Cpf,
                    Nome = p.Nome
                     
                }).ToList();
        }

        [HttpGet]
        public List<Eleitor> ListarPorCpf(string cpf)
        {
            return contexto.Eleitores.Where(p => p.Cpf == cpf).Select
            (
                p => new Eleitor 
                { 
                    Id = p.Id,
                    Cpf = p.Cpf,
                    Nome = p.Nome
                     
                }).ToList();
        }

        [HttpGet]
        public List<string> ListarNomes()
        {

            var consultaNomes = (from eleitor in contexto.Eleitores select eleitor.Nome).Distinct().ToList();

            return consultaNomes;
        }

        [HttpGet]
        public List<string> ListarCpf()
        {                
            var consultaCpf = (from eleitor in contexto.Eleitores select eleitor.Cpf).Distinct();

            return consultaCpf.OrderBy(n=>n).ToList();
        }
    }
}