using System.ComponentModel.DataAnnotations;

public class PostCreate
{
    [Required]
    public string Title {get; set;}
    [Required]
    public string Text { get; set; }
}