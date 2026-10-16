using MantenimientoMaquinasAPI.Domain.Agregates;
using MantenimientoMaquinasAPI.Domain.Contracts;

namespace MantenimientoMaquinasAPI.Application.Agents;

    public class MachinesAgentTools(
    IMachinesRespositoryWR machinesRepository)
    {
        public async Task<IReadOnlyList<MachineRiskResult>>
       GetMachinesAtRiskAsync(
           CancellationToken cancellationToken = default)
        {
            var machines = await machinesRepository.GetAll(
                cancellationToken);

            return machines
                .Where(m =>
                    (m.MaxTemperature > 0 &&
                     m.CurrentTemperature >= m.MaxTemperature)
                    || string.Equals(
                        m.MachineStatus,
                        "FALLA",
                        StringComparison.OrdinalIgnoreCase))
                .Select(m => new MachineRiskResult(
                    m.MachineId,
                    m.MachineName,
                    m.CurrentTemperature,
                    m.MaxTemperature,
                    m.MachineStatus,
                    GetReason(m)))
                .ToList();
        }

        private static string GetReason(Machines machine)
        {
            if (string.Equals(
                machine.MachineStatus,
                "FALLA",
                StringComparison.OrdinalIgnoreCase))
            {
                return "La máquina figura en estado FALLA.";
            }

            return "La temperatura actual alcanzó o superó " +
                   "la temperatura máxima configurada.";
        }
    }

    public sealed record MachineRiskResult(
        int MachineId,
        string MachineName,
        decimal CurrentTemperature,
        decimal MaxTemperature,
        string MachineStatus,
        string Reason);


