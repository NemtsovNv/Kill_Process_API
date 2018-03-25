using System;
using Microsoft.AspNetCore.Mvc;
using KillProcess.Infrastructure.Business.Services;

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
        public IActionResult Post([FromBody]int id)
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