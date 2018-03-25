using System.Diagnostics;
using KillProcess.Infrastructure.Business.Services.Implementation;

namespace KillProcess.Tests.Unit.Helpers
{
    public class ProcessServiceTestable : ProcessService
    {
        public ProcessServiceTestable(bool withData)
        {
            if(withData)
            {
                var processInfo = new ProcessStartInfo("cmd.exe");
                processStubs = new Process[]
                {
                    Process.Start(processInfo),
                    Process.Start(processInfo),
                    Process.Start(processInfo)
                };
            }
        }

        private readonly Process[] processStubs;

        protected override Process[] GetProcessesInfo()
        {
            return processStubs;
        }
    }
}
