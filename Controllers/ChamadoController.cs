using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiServico.Models.Duo;
using ApiServico.Models;

namespace ApiServico.Controllers
{
    [Route("/chamados")]
    [ApiController]
    public class ChamadoController : ControllerBase
    {

        private static List<Chamado> _listaChamados = new List<Chamado>
        {
            new Chamado() {
                Id = 1, Titulo = "Erro na Tela de Acesso", Descricao = "O usuário não conseguiu logar" },
            new Chamado() {
                Id = 2, Titulo = "Sistema com lentidão", Descricao = "Demora no carregamento das telas"}
        };

        private static int _proximoId = 3;

        [HttpGet]
        public IActionResult BuscarTodos()
        {
            return Ok(_listaChamados);
        }

        [HttpGet("{id}")]

        public IActionResult BuscarPorId(int id)
        {
            var chamado = _listaChamados.FirstOrDefault(item => item.Id == id);

            if (chamado == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult Criar([FromBody] ChamadoDuo novoChamado)
        {

            var chamado = new Chamado() { Titulo = novoChamado.Titulo, Descricao = novoChamado.Descricao };
            chamado.Id = _proximoId++;
            chamado.Status = "Aberto";

            _listaChamados.Add(chamado);

            return Created("", chamado);
        }

        [HttpDelete("{id}")]

        public IActionResult Remover(int id)
        {
            var chamado = _listaChamados.FirstOrDefault(item => item.Id == id);

            if (chamado == null)
            {
                return NotFound();
            }

            _listaChamados.Remove(chamado);

            return NoContent();
        }

        [HttpPut("{id}")]

        public IActionResult Atualizar(int id, [FromBody] ChamadoDuo AtualizarChamado)
        {
            var chamado = _listaChamados.FirstOrDefault(item => item.Id == id);

            if (chamado == null)
            {
                return NotFound();
            }

            chamado.Titulo = AtualizarChamado.Titulo;
            chamado.Descricao = AtualizarChamado.Descricao;

            return Ok(chamado);
        }

        [HttpPut("{id}/Encerramento")]

        public IActionResult Fechar(int id)
        {
            var chamado = _listaChamados.FirstOrDefault(item => item.Id == id);

            if (chamado == null)
            {
                return NotFound();
            }

            chamado.Status = "Fechado";
            

            return Ok(chamado);
        }
    }
}