using System.Collections.Generic;
using KillProcess.Domain.Core.Models;

namespace KillProcess.Infrastructure.Business.Services
{
    public interface IProcessService
    {
        IList<ProcessData> GetProcesses();

        int KillProcess(int id);
    }
}
