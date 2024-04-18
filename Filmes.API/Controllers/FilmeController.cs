using AutoMapper;
using Filmes.API.Data;
using Filmes.API.DTO;
using Filmes.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filmes.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona um filme ao banco de dados
        /// </summary>
        /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AdicionaFilmes([FromBody] CreateFilmesDTO filmeDTO)
        {
            Models.Filmes filme = _mapper.Map<Models.Filmes>(filmeDTO);
            _context.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaFilmesPorId), new { id = filme.Id }, filme);
        }

        /// <summary>
        ///Lista todos os filmes no banco de dados
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso  seja feita com sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<ReadFilmesDTO> RecuperaFilmes([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return _mapper.Map<List<ReadFilmesDTO>>(_context.Filmes.Skip(skip).Take(take));
        }


        /// <summary>
        /// Lista os filmes pelo id
        /// </summary>
        /// <param name="id">Objeto com o campos necessário para listar um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a listagem seja feita com sucesso</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult RecuperaFilmesPorId(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme == null) return NotFound();
            var filmeDTO = _mapper.Map<ReadFilmesDTO>(filme);
            return Ok(filmeDTO);
        }


        /// <summary>
        /// Realiza a atualização dos dados de um filme
        /// </summary>
        /// <param name="id">Objeto com o campos necessário para alterar um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a atualização seja feita com sucesso</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AtualizarFilmes(int id, [FromBody] UpdateFilmesDTO filmesDTO)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme == null) return NotFound(); _mapper.Map(filmesDTO, filme);
            _context.SaveChanges();
            return NoContent();
        }


        /// <summary>
        /// Realiza a atualização de uma parte dos dados de um filme
        /// </summary>
        /// <param name="id">Objeto com o campos necessário para alterar um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a atualização seja feita com sucesso</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AtualizarFilmesParcial(int id, JsonPatchDocument<UpdateFilmesDTO> patch)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();
            var filmeParaAtualizar = _mapper.Map<UpdateFilmesDTO>(filme);

            patch.ApplyTo(filmeParaAtualizar, ModelState);

            if (!TryValidateModel(filmeParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(filmeParaAtualizar, filme);
            _context.SaveChanges();
            return NoContent();
        }



        /// <summary>
        /// Deleta um filme ao banco de dados
        /// </summary>
        /// <param name="id">Objeto com o campos necessário para deletar um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso o delete seja feita com sucesso</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeletarFilmes(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();

            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
