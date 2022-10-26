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
        public List<Candidato> Listar(string? order = "padrao")
        {
            if(order == "c"){
                return contexto.Candidatos.OrderBy(c => c.Numero).ToList();
            }

            else if(order == "d"){
                return contexto.Candidatos.OrderByDescending(c => c.Numero).ToList();
            }

            else{
                return contexto.Candidatos.OrderBy(c => c.Nome).ToList();
            }
            
        }

        [HttpPost]
        public string Cadastrar([FromBody] Candidato novoCandidato)
        {
            contexto.Add(novoCandidato);
            contexto.SaveChanges();
            return "Candidato(a) cadastrado(a) com sucesso!";
        }


        [HttpDelete]
        public bool Excluir([FromBody]int id)
        {
            try
            {
                List<Votacao> votacoes = contexto.Votacoes.Where(v => v.IdCandidato == id).ToList();

                if (votacoes.Count() == 0)
                {
                    Candidato? dados = contexto.Candidatos.FirstOrDefault(p => p.Id == id);

                    if (dados == null)
                    {
                        Console.WriteLine("Candidato não encontrado!");
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
                        catch (Exception)
                        {
                            Console.WriteLine("Erro ao realizar a remoção!");
                            return false;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("O candidato selecionado possui votações registradas!");
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        [HttpDelete]
        public bool ExcluirLogico([FromBody]int id)
        {
            Candidato? dados = contexto.Candidatos.FirstOrDefault(p => p.Id == id);

            if (dados == null)
            {
                Console.WriteLine("Candidato não encontrado!");
                return false;
            }
            else
            {
                try
                {
                    dados.Excluido = true;
                    contexto.Update(dados);
                    contexto.SaveChanges();

                    return true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Erro ao realizar a remoção!");
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
        public Candidato? Visualizar(int id)
        {
            Candidato? dado = contexto.Candidatos.FirstOrDefault(p => p.Id == id);
            
            return dado;
        }


        [HttpGet]
        public List<Candidato> ListarPorPartido(string partido)
        {
            return contexto.Candidatos.Where(p => p.Partido == partido).Select
            (
                p => new Candidato 
                { 
                    Id = p.Id,
                    Numero = p.Numero,
                    Nome = p.Nome,
                    Partido = p.Partido 
                }).ToList();
        }

        [HttpGet]
        public List<string> ListarPartidos()
        {

            var consultaPartidos = (from candidato in contexto.Candidatos select candidato.Partido).Distinct().ToList();

            return consultaPartidos;
        }

        [HttpGet]
        public List<int> ListarNumeros()
        {                
            var consultaNumeros = (from candidato in contexto.Candidatos select candidato.Numero).Distinct();

            return consultaNumeros.OrderBy(n=>n).ToList();
        }

    }
}