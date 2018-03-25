using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using KillProcess.Domain.Core.Models;

namespace KillProcess.Infrastructure.Business.Services.Implementation
{
    public class ProcessService : IProcessService
    {
        public IList<ProcessData> GetProcesses()
        {
            IList<ProcessData> resultData = null;
            try
            {
                Process[] processes = GetProcessesInfo() ?? new Process[0];

                resultData = processes.Select(x => new ProcessData
                {
                    Id = x.Id,
                    Name = x.ProcessName
                }).ToList();
            }
            catch(Win32Exception)
            {
                throw new Exception("User has no permissions to read process info. Please, contact your administrator.");
            }

            return resultData;
        }

        public int KillProcess(int id)
        {
            try
            {
                Process[] processes = GetProcessesInfo();

                foreach(var i in processes)
                {
                    if(i.Id == id)
                    {
                        i.Kill();
                        i.WaitForExit();

                        return id;
                    }
                }
            }
            catch (Win32Exception)
            {
                throw new Win32Exception("User has no permissions to kill process. Please, contact your administrator to grant specific permissions.");
            }

            throw new ArgumentException($"No process was found with specified id : {id}");
        }

        protected virtual Process[] GetProcessesInfo()
        {
            return Process.GetProcesses();
        }
    }
}
