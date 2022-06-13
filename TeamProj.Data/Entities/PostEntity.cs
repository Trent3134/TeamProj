using System.ComponentModel.DataAnnotations;
using System;
public class PostEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Text { get; set; }

    public DateTimeOffset CreatedUtc { get; set; }

    public int OwnerId {get; set;}
    //public virtual List<Comments> Comments {get; set;}
}