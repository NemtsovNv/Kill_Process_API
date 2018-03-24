using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security;
using KillProcess.Domain.Model.Command;

namespace KillProcess.Infrastructure.Business.Services.Implementation
{
    public class ProcessService : IProcessService
    {
        public IList<ProcessData> GetProcesses()
        {
            IList<ProcessData> resultData = null;
            try
            {
                Process[] processes = Process.GetProcesses();

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

        public bool KillProcess(int id)
        {
            try
            {
                Process[] processes = Process.GetProcesses();

                foreach(var i in processes)
                {
                    if(i.Id == id)
                    {
                        i.Kill();
                        i.WaitForExit();

                        return true;
                    }
                }
            }
            catch (Win32Exception)
            {
                throw new Win32Exception("User has no permissions to kill process. Please, contact your administrator to grant specific permissions.");
            }

            return false;
        }
    }
}
