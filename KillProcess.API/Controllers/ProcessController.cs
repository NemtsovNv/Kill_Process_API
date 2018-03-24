using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KillProcess.Domain.Model.Command;
using KillProcess.Infrastructure.Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KillProcess.API.Controllers
{
    [Produces("application/json")]
    [Route("api/process")]
    public class ProcessController : Controller
    {
        public ProcessController(IProcessService processServiceInstance)
        {
            processService = processServiceInstance;
        }

        readonly IProcessService processService;

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = processService.GetProcesses();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(int id)
        {
            try
            {
                var result = processService.KillProcess(id);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}