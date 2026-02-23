using System.ComponentModel.DataAnnotations;

namespace BookstoreRocketseat.Communications.Requests;

public class RequestNewBookJson
{
    
    [Required(ErrorMessage = "O título é obrigatório")]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "O título deve ter no máximo 120 caracteres")] 
    public string Title { get; set; }
    
    [Required(ErrorMessage = "O autor é obrigatório")]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "O autor deve ter no máximo 120 caracteres")] 
    public string Author { get; set; }
    
    [Required(ErrorMessage = "O gênero é obrigatório")]
    [AllowedValues("Terror","Mistério", "Ficção", "Romance")]
    public string Genre { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "O preço deve estar entre 0 e 999,99")] 
    public decimal Price { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "O estoque não pode ser negativo")] 
    public int Stock { get; set; }


}
