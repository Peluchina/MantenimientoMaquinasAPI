namespace MantenimientoMaquinasAPI.Application.Agents
{
    public interface IAgentService
    {
        Task<string> AskAsync(string message,CancellationToken cancellationToken = default);
    }
}
