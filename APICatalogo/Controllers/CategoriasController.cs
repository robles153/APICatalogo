using APICatalogo.Domain;
using APICatalogo.Infra.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly ApiDbContext _context;

    public CategoriasController(ApiDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
    {
        try
        {
            var categorias = _context.Categorias.Include(x => x.Produtos).ToList();
            if (categorias is null)
            {
                return NotFound();
            }

            return categorias;
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar recuperar as categorias");
        }


    }

    [HttpGet("produtos")]
    public ActionResult<IEnumerable<Categoria>> GetCategorias()
    {
        try
        {
            return _context.Categorias.Include(x => x.Produtos).ToList();

        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar recuperar as categorias");
        }

    }

    [HttpGet("{id:int}")]
    public ActionResult<Categoria> GetCategoria(int id)
    {
        try
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
            if (categoria is null)
            {
                return NotFound($" Categoria com id = {id} não encontrada");
            }

            return categoria;
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar recuperar a categoria");
        }

    }

    [HttpPost]
    public ActionResult PostCategoria(Categoria categoria)
    {
        if (categoria is null)
        {
            return BadRequest();
        }

        _context.Categorias.Add(categoria);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetCategoria), new { id = categoria.CategoriaId }, categoria);
    }

    [HttpPut("{id:int}")]
    public ActionResult PutCategoria(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId)
        {
            return BadRequest();
        }

        _context.Entry(categoria).State = EntityState.Modified;
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public ActionResult<Categoria> DeleteCategoria(int id)
    {
        var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
        if (categoria is null)
        {
            return NotFound();
        }

        _context.Categorias.Remove(categoria);
        _context.SaveChanges();

        return categoria;
    }
}
