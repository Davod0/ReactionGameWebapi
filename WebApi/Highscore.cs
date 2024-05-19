namespace WebApi;
public class Highscore
{
    public int Id { get; set; }
    public string PlayerName { get; set; }
    public long Score { get; set; }
    public string GameName { get; set; }
    public DateTime CreatedAt { get; set; }
}
