using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using InventoryService.MVC.Models;

namespace InventoryService.MVC.Controllers
{
    public class MaintenanceController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MaintenanceController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: /Maintenance/History
        [HttpGet]
        public IActionResult History()
        {
            return View(new List<RepairHistoryViewModel>());
        }

        // POST: /Maintenance/History (search by vehicleId)
        [HttpPost]
        public async Task<IActionResult> History(int vehicleId)
        {
            if (vehicleId <= 0)
            {
                ViewBag.Error = "Please enter a valid Vehicle ID (must be greater than 0).";
                return View(new List<RepairHistoryViewModel>());
            }

            var client = _httpClientFactory.CreateClient("MaintenanceApi");

            try
            {
                // Call the secured Web API 
                var response = await client.GetAsync($"api/maintenance/vehicles/{vehicleId}/repairs");

                if (!response.IsSuccessStatusCode)
                {
                    // Common helpful messages for demo
                    if ((int)response.StatusCode == 401)
                        ViewBag.Error = "Unauthorized (401). Check that X-Api-Key is configured in MVC appsettings and attached in Program.cs.";
                    else
                        ViewBag.Error = $"API call failed: {(int)response.StatusCode} {response.ReasonPhrase}";

                    return View(new List<RepairHistoryViewModel>());
                }

                var repairs = await response.Content.ReadFromJsonAsync<List<RepairHistoryViewModel>>();

                // Pass vehicleId back to the view
                ViewBag.VehicleId = vehicleId;

                return View(repairs ?? new List<RepairHistoryViewModel>());
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(new List<RepairHistoryViewModel>());
            }
        }
    }
}
