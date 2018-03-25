using System.Collections.Generic;
using KillProcess.Domain.Model.Command;

namespace KillProcess.Infrastructure.Business.Services
{
    public interface IProcessService
    {
        IList<ProcessData> GetProcesses();

        int KillProcess(int id);
    }
}
