namespace updateApi.Models
{

public class Assiment
{
    public int Id { get; set; }
    public int IdTask { get; set; }
    public string Name{ get; set; }
    public string Description{ get; set; }
    public bool Done{ get; set; }
    public string Deadline{get;set;}

}
}