using KillProcess.Domain.Model.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace KillProcess.Infrastructure.Business.Services
{
    public interface IProcessService
    {
        IList<ProcessData> GetProcesses();

        bool KillProcess(int id);
    }
}
