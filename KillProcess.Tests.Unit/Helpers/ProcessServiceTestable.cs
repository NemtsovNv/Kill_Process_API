using System;
using System.Diagnostics;
using KillProcess.Infrastructure.Business.Services.Implementation;

namespace KillProcess.Tests.Unit.Helpers
{
    public class ProcessServiceTestable : ProcessService
    {
        public ProcessServiceTestable(bool withData, bool withSuccessSearchResult)
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

                successSearchResult = withSuccessSearchResult;
            }
        }

        private readonly Process[] processStubs;
        private readonly bool successSearchResult;

        protected override Process[] GetProcessesInfo()
        {
            return processStubs;
        }

        protected override Process GetProcessesInfoById(int id)
        {
            if(successSearchResult)
            {
                return processStubs[0];
            }

            throw new ArgumentException();
        }
    }
}
