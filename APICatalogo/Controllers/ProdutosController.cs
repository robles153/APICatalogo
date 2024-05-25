using APICatalogo.Domain;
using APICatalogo.Infra.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly ApiDbContext _context;

    public ProdutosController(ApiDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> GetProdutos()
    {
        var produtos = _context.Produtos.ToList();
        if (produtos is null)
        {
            return NotFound();
        }

        return produtos;
    }

    [HttpGet("{id:int}")]
    public ActionResult<Produto> GetProduto(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        if (produto is null)
        {
            return NotFound();
        }

        return produto;
    }

    [HttpPost]
    public ActionResult PostProduto(Produto produto)
    {
        if (produto is null)
        {
            return BadRequest();
        }

        _context.Produtos.Add(produto);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetProduto), new { id = produto.ProdutoId }, produto);
    }

    [HttpPut("{id:int}")]
    public ActionResult PutProduto(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
        {
            return BadRequest();
        }

        _context.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(produto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<Produto> DeleteProduto(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        if (produto is null)
        {
            return NotFound("Produto não localizado");
        }

        _context.Produtos.Remove(produto);
        _context.SaveChanges();

        return Ok(produto);
    }
}
