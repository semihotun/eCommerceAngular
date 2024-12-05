using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels;

public class MailInfo : IElasticEntity
{
    public MailInfo(string fromAddress, string fromPassword, string host, int port)
    {
        FromAddress = fromAddress;
        FromPassword = fromPassword;
        Host = host;
        Port = port;
    }
    public Guid Id { get; private set; }
    public string FromAddress { get; private set; } 
    public string FromPassword { get; private set; } 
    public string Host { get; private set; }
    public int Port { get; private set; }
    public void SetId(Guid id)
    {
        Id = id;
    }
}
