using Maintenance.WebAPI.Models;

namespace Maintenance.WebAPI.Services;

public class FakeRepairHistoryService : IRepairHistoryService
{
    // In-memory state (persists while the app is running)
    private readonly Dictionary<int, List<RepairHistoryDto>> _repairsByVehicle = new();
    private int _nextId = 3;

    public FakeRepairHistoryService()
    {
        // Seed initial in-memory data once
        _repairsByVehicle[1] = new List<RepairHistoryDto>
        {
            new RepairHistoryDto
            {
                Id = 1,
                VehicleId = 1,
                RepairDate = DateTime.Now.AddDays(-10),
                Description = "Oil change",
                Cost = 89.99m,
                PerformedBy = "Quick Lube"
            },
            new RepairHistoryDto
            {
                Id = 2,
                VehicleId = 1,
                RepairDate = DateTime.Now.AddDays(-40),
                Description = "Brake pad replacement",
                Cost = 350.00m,
                PerformedBy = "Auto Repair Pro"
            }
        };
    }

    public List<RepairHistoryDto> GetByVehicleId(int vehicleId)
    {
        if (!_repairsByVehicle.ContainsKey(vehicleId))
            _repairsByVehicle[vehicleId] = new List<RepairHistoryDto>();

        return _repairsByVehicle[vehicleId];
    }

    public RepairHistoryDto AddRepair(int vehicleId, RepairHistoryDto repair)
    {
        var list = GetByVehicleId(vehicleId);

        repair.Id = _nextId++;
        repair.VehicleId = vehicleId;

        // If client didn’t send a date, set one
        if (repair.RepairDate == default)
            repair.RepairDate = DateTime.Now;

        list.Add(repair);
        return repair;
    }
}
