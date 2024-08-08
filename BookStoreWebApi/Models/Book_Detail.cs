using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWebApi.Models;

public partial class Book_Detail
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(30)]
    [Unicode(false)]
    public string Publisher { get; set; }

    [Required]
    [StringLength(30)]
    [Unicode(false)]
    public string Title { get; set; }

    [Required]
    [StringLength(30)]
    [Unicode(false)]
    public string Author_LastName { get; set; }

    [Required]
    [StringLength(30)]
    [Unicode(false)]
    public string Author_FirstName { get; set; }

    [Column(TypeName = "decimal(10, 0)")]
    public decimal Price { get; set; }
}
