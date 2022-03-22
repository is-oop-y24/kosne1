using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Server.ReportsExceptions;
using Server.Services.Interfaces;

namespace Server.Controllers
{
    [ApiController]
    [Route("/report")]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly ISprintReportService _sprintReportService;

        public ReportController(IReportService reportService, ISprintReportService sprintReportService)
        {
            _reportService = reportService;
            _sprintReportService = sprintReportService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] Guid employeeId)
        {
            try
            {
                return Ok(_reportService.Create(employeeId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("sprint")]
        public async Task<IActionResult> CreateSprint([FromQuery] Guid employeeId)
        {
            try
            {
                return Ok(_sprintReportService.Create(employeeId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetReport([FromQuery] Guid reportId)
        {
            if (reportId != Guid.Empty)
            {
                var result = await _reportService.FindById(reportId);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return Ok(_reportService.FindById(reportId));
        }

        [HttpGet("sprint")]
        public async Task<IActionResult> GetSprintReport([FromQuery] Guid sprintReportId)
        {
            if (sprintReportId != Guid.Empty)
            {
                var result = await _sprintReportService.FindById(sprintReportId);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }
            
            return Ok(_sprintReportService.FindById(sprintReportId));
        }

        [HttpPatch]
        public async Task<IActionResult> AddTask([FromQuery] Guid reportId, [FromQuery] Guid taskId, [FromQuery] Guid employeeId)
        {
            try
            {
                var report = _reportService.AddTask(reportId, taskId, employeeId);
                return Ok(report);
            }
            catch (ReportsGlobalException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}