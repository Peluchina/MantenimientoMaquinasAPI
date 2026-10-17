using Microsoft.Extensions.AI;

namespace MantenimientoMaquinasAPI.Application.Agents;

    public class AgentService(IChatClient chatClient,
    MachinesAgentTools machinesTools) : IAgentService
    {
    public async Task<string> AskAsync(
    string message,
    CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException(
                "La pregunta no puede estar vacía.",
                nameof(message));

        var tool = AIFunctionFactory.Create(
            machinesTools.GetMachinesAtRiskAsync);

        var messages = new List<ChatMessage>
        {
            new(ChatRole.System,
                """
                Eres un asistente de mantenimiento industrial.
                Responde siempre en español.
                Consulta las herramientas cuando necesites
                información de las máquinas.
                No inventes temperaturas ni estados.
                No modifiques ni elimines registros.
                Explica los riesgos con claridad.
                """),

            new(ChatRole.User, message)
        };

        var options = new ChatOptions
        {
            Tools = [tool]
        };

        var response = await chatClient.GetResponseAsync(
            messages,
            options,
            cancellationToken);

        return response.Text;
    }
}

