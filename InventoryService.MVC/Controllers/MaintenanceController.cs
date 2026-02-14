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

        [HttpGet]
        public IActionResult History()
        {
            return View(new List<RepairHistoryViewModel>());
        }

        [HttpPost]
        public async Task<IActionResult> History(int vehicleId)
        {
            var client = _httpClientFactory.CreateClient("MaintenanceApi");

            try
            {
                var response = await client.GetAsync($"api/maintenance/vehicles/{vehicleId}/repairs");

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = $"API call failed: {(int)response.StatusCode} {response.ReasonPhrase}";
                    return View(new List<RepairHistoryViewModel>());
                }

                var repairs = await response.Content.ReadFromJsonAsync<List<RepairHistoryViewModel>>();

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
