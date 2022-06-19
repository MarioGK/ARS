namespace ARS.Common.Models;

public class Token
{
    public Token()
    {
        TokenUID = Guid.NewGuid();
    }
    public Guid? TokenUID { get; set; }
    public DateTime Expiration { get; set; }
}