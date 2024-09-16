using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SwapnapurtiWebsite;
using SwapnapurtiWebsite.Models;
using System.Diagnostics;
using System.Net.Mime;
using System.Text;
using WebAppForAPITest.Models;

namespace WebAppForAPITest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly HttpClient _client;
		private string apiUrl = "https://qr7h4xb4hf.execute-api.us-east-1.amazonaws.com";
		public HomeController(ILogger<HomeController> logger, HttpClient client)
        {
            _logger = logger;
			_client = client ?? throw new ArgumentNullException(nameof(client));
		}

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
		public IActionResult Login(LoginModel loginModel)
		{
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public async Task<IActionResult> PersonalInfoForm()
		{
			try
			{
				var request = new HttpRequestMessage
				{
					Method = HttpMethod.Post,
					RequestUri = new Uri(apiUrl + "/get-new-uid"),
					Content = new StringContent(
						CommonVariables.empCode + ";" + CommonVariables.UserId,
						Encoding.UTF8,
						MediaTypeNames.Application.Json), // or "application/json" in older versions
				};

				var response = await _client.SendAsync(request)
					.ConfigureAwait(false);
				response.EnsureSuccessStatusCode();

				var responseBody = await response.Content.ReadAsStringAsync()
					.ConfigureAwait(false);
				ViewData["UID"] = responseBody;
			}
			catch (Exception e1)
			{
				ViewData["UID"] = "ERROR";
			}
			return View();
		}

		[HttpPost]
		public IActionResult PersonalInfoForm(PersonalInfoFormModel model)
		{
			return RedirectToAction("Index", "Home");
		}


		public async Task<IActionResult> SavePersonalInfoForm(string formdata)//WebMethod to Save the data
		{

			var serializeData = JsonConvert.DeserializeObject<PersonalInfoFormModel>(formdata);
			serializeData.DEOName = CommonVariables.UserId;
			serializeData.DEODate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(serializeData);
			try
			{
				var request = new HttpRequestMessage
				{
					Method = HttpMethod.Post,					
					RequestUri = new Uri(apiUrl + "/add-personal-form"),
					Content = new StringContent(
						jsonString,
						Encoding.UTF8,
						MediaTypeNames.Application.Json), // or "application/json" in older versions
				};

				var response = await _client.SendAsync(request)
					.ConfigureAwait(false);
				response.EnsureSuccessStatusCode();

				var responseBody = await response.Content.ReadAsStringAsync()
					.ConfigureAwait(false);

				if(responseBody == "1")
					return Json(new Object[] { "SUCCESS;Personal data saved successfully - UID - " + serializeData.UniqueID }) ;
				else
					return Json(new Object[] { "2;Data did not saved! Error while saving data." });
			}
			catch (Exception e1)
			{
				return Json(new Object[] { "2;Data did not saved! Error while saving data." });
			}

		}

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

		public IActionResult OralExamForm()
		{
			return View();
		}

		public async Task<IActionResult> SaveOralExamForm(string formdata)//WebMethod to Save the data
		{

			var serializeData = JsonConvert.DeserializeObject<OralExamFormModel>(formdata);
			serializeData.DEOName = CommonVariables.UserId;
			serializeData.DEODate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(serializeData);
			try
			{
				var request = new HttpRequestMessage
				{
					Method = HttpMethod.Post,
					RequestUri = new Uri(apiUrl + "/add-oralexam-form"),
					Content = new StringContent(
						jsonString,
						Encoding.UTF8,
						MediaTypeNames.Application.Json), // or "application/json" in older versions
				};

				var response = await _client.SendAsync(request)
					.ConfigureAwait(false);
				response.EnsureSuccessStatusCode();

				var responseBody = await response.Content.ReadAsStringAsync()
					.ConfigureAwait(false);

				if (responseBody == "1")
					return Json(new Object[] { "SUCCESS;Oral exam form data saved successfully - UID - " + serializeData.UniqueID });
				else
					return Json(new Object[] { "2;Data did not saved! Error while saving data." });
			}
			catch (Exception e1)
			{
				return Json(new Object[] { "2;Data did not saved! Error while saving data." });
			}

		}

		public IActionResult OralSurgeonForm()
		{
			return View();
		}

		public async Task<IActionResult> SaveOralSurgeonForm(string formdata)//WebMethod to Save the data
		{

			var serializeData = JsonConvert.DeserializeObject<OralSurgeonFormModel>(formdata);
			serializeData.DEOName = CommonVariables.UserId;
			serializeData.DEODate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(serializeData);
			try
			{
				var request = new HttpRequestMessage
				{
					Method = HttpMethod.Post,
					RequestUri = new Uri(apiUrl + "/add-oralsurgeon-form"),
					Content = new StringContent(
						jsonString,
						Encoding.UTF8,
						MediaTypeNames.Application.Json), // or "application/json" in older versions
				};

				var response = await _client.SendAsync(request)
					.ConfigureAwait(false);
				response.EnsureSuccessStatusCode();

				var responseBody = await response.Content.ReadAsStringAsync()
					.ConfigureAwait(false);

				if (responseBody == "1")
					return Json(new Object[] { "SUCCESS;Oral surgeon form data saved successfully - UID - " + serializeData.UniqueID });
				else
					return Json(new Object[] { "2;Data did not saved! Error while saving data." });
			}
			catch (Exception e1)
			{
				return Json(new Object[] { "2;Data did not saved! Error while saving data." });
			}

		}

		public async Task<IActionResult> SearchUIDData(string formdata)//WebMethod to Save the data
		{
			try
			{
				var request = new HttpRequestMessage
				{
					Method = HttpMethod.Post,
					RequestUri = new Uri(apiUrl + "/get-uid-details"),
					Content = new StringContent(
						formdata,
						Encoding.UTF8,
						MediaTypeNames.Application.Json), // or "application/json" in older versions
				};

				var response = await _client.SendAsync(request)
					.ConfigureAwait(false);
				response.EnsureSuccessStatusCode();

				var responseBody = await response.Content.ReadAsStringAsync()
					.ConfigureAwait(false);

				if (responseBody.Split(";")[0] == "SUCCESS")
					return Json(new Object[] { responseBody });
				else
					return Json(new Object[] { "2;Data did not saved! Error while saving data." });
			}
			catch (Exception e1)
			{
				return Json(new Object[] { "2;Data did not saved! Error while saving data." });
			}

		}

		public IActionResult ViewFormData()
		{
			return View();
		}

		public async Task<IActionResult> GetFormDataAgainstDate(string formdata)//WebMethod to Save the data
		{
			try
			{
				var request = new HttpRequestMessage
				{
					Method = HttpMethod.Post,
					RequestUri = new Uri(apiUrl + "/get-form-details"),
					Content = new StringContent(
						formdata,
						Encoding.UTF8,
						MediaTypeNames.Application.Json), // or "application/json" in older versions
				};

				var response = await _client.SendAsync(request)
					.ConfigureAwait(false);
				response.EnsureSuccessStatusCode();

				var responseBody = await response.Content.ReadAsStringAsync()
					.ConfigureAwait(false);

				if (responseBody != "")
					return Json(new Object[] { responseBody });
				else
					return Json(new Object[] { "" });
			}
			catch (Exception e1)
			{
				return Json(new Object[] { "" });
			}

		}
	}
}
