using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Domain;

[Table("Categorias")]
public class Categoria
{
    #region EFCORE
#pragma warning disable CS8618
    public Categoria()
    {
        Produtos = new Collection<Produto>();
    }
#pragma warning restore CS8618
    #endregion EFCORE

    [Key]
    public int CategoriaId { get; set; }

    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }
    public ICollection<Produto>? Produtos { get; set; }
}





