namespace fly_server.Models;

public partial class ResponseModel
{
    public int StatusCode { get; set; }
    public string StatusMessage { get; set; } = "";
    public object Body { get; set; } = new(){};
}