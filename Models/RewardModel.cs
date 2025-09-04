namespace fly_server.Models;

public partial class RewardModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int Points { get; set; }
    public string Badge { get; set; } = "";
}