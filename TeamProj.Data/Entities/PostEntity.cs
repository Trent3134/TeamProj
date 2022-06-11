public class PostEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Text { get; set; }
    //public virtual List<Comments> Comments {get; set;}
}