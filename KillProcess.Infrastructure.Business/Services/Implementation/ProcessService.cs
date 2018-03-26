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
            catch(Win32Exception ex)
            {
                throw new Win32Exception("User has no permissions to read process info. Please, contact your administrator.", ex);
            }
            catch(Exception ex)
            {
                throw new Exception("Something went wrong. Please try again or contact your administrator.", ex);
            }

            return resultData;
        }

        public int KillProcess(int id)
        {
            try
            {
                Process process = GetProcessesInfoById(id);

                if(process != null)
                {
                    using (process)
                    {
                        process.Kill();
                        process.WaitForExit();
                    }

                    return id;
                }
            }
            catch (Win32Exception)
            {
                throw new Win32Exception("User has no permissions to kill process. Please, contact your administrator to grant specific permissions.");
            }
            catch(ArgumentException)
            {
                throw new ArgumentException($"No process was found with specified id : {id}.");
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong. Please try again or contact your administrator.", ex);
            }

            throw new ArgumentException($"No process was found with specified id : {id}.");
        }

        protected virtual Process[] GetProcessesInfo()
        {
            return Process.GetProcesses();
        }

        protected virtual Process GetProcessesInfoById(int id)
        {
            return Process.GetProcessById(id);
        }
    }
}
