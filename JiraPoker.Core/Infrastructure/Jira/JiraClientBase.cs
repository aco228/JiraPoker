using JiraPoker.Core.Domain.HttpRequests;
using JiraPoker.Core.Infrastructure.HttpRequests;

namespace JiraPoker.Core.Infrastructure.Jira;

public abstract class JiraClientBase
{
    protected IRequestClient RequestClient;
    
    public JiraClientBase()
    {
        RequestClient = new RequestClient();
    }
}