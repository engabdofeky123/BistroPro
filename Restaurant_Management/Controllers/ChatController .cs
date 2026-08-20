using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace MVC.Controllers
{
    public class ChatController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public ChatController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string message, string sessionId)
        {
            var client = _httpClientFactory.CreateClient();

            var body = new
            {
                message = message,
                session_id = sessionId
            };

            var json = JsonSerializer.Serialize(body);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(
                "https://abdo-feky.app.n8n.cloud/webhook-test/customer-support",
                content);

            if (!response.IsSuccessStatusCode)
            {
                return Json(new
                {
                    success = false,
                    reply = "Sorry, something went wrong."
                });
            }

            var result = await response.Content.ReadAsStringAsync();
            using JsonDocument document = JsonDocument.Parse(result);

            var reply = document.RootElement
                                .GetProperty("reply")
                                .GetString();

            return Json(new
            {
                success = true,
                reply
            });
        }
    }
}