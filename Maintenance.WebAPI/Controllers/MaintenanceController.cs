using Microsoft.AspNetCore.Mvc;
using Maintenance.WebAPI.Services;
using Maintenance.WebAPI.Models;

namespace Maintenance.WebAPI.Controllers;

[ApiController]
[Route("api/maintenance")]
public class MaintenanceController : ControllerBase
{
    private readonly IRepairHistoryService _service;

    public MaintenanceController(IRepairHistoryService service)
    {
        _service = service;
    }

    // GET: api/maintenance/vehicles/{vehicleId}/repairs
    [HttpGet("vehicles/{vehicleId}/repairs")]
    public IActionResult GetRepairHistory(int vehicleId)
    {
        var history = _service.GetByVehicleId(vehicleId);
        return Ok(history);
    }

    // POST: api/maintenance/vehicles/{vehicleId}/repairs
    [HttpPost("vehicles/{vehicleId}/repairs")]
    public IActionResult AddRepair(int vehicleId, [FromBody] RepairHistoryDto repair)
    {
        if (repair == null)
        {
            return BadRequest("Repair data is required.");
        }

        var added = _service.AddRepair(vehicleId, repair);
        return Ok(added);
    }
}
