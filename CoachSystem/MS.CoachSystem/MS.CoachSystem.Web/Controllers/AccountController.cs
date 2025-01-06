using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.CoachSystem.Core.DTOs;
using MS.CoachSystem.Service.Services;
using MS.CoachSystem.Web.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Mail;
using MS.CoachSystem.Service.Client;

namespace MS.CoachSystem.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _authService;
        public AccountController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }
            var tokenResponse = await _authService.LoginAsync(new LoginDto() { Email = loginDto.Email, Password = loginDto.Password });

            if (tokenResponse.IsSuccessfull)
            {
                HttpContext.Response.Cookies.Append("AccessToken", tokenResponse.Data.AccessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true
                });
                return RedirectToAction("Index", "Home");
            }

            tokenResponse.Error.Errors.ToList().ForEach(x => ModelState.AddModelError(string.Empty, x));
            return View(loginDto);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateCoachViewModel createCoachViewModel)
        {
            string SoapEndpoint = "https://localhost:7114/RegisterCoachService.asmx"; // SOAP servisi endpoint

            // Model doğrulama
            if (!ModelState.IsValid)
            {
                return View(createCoachViewModel);  // Hata varsa formu tekrar göster
            }

            // SOAP XML isteği oluşturma
            string soapXml = $@"
    <soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:web='http://tempuri.org/'>
       <soapenv:Header/>
       <soapenv:Body>
          <web:RegisterCoachUser>
             <userName>{createCoachViewModel.UserName}</userName>
             <email>{createCoachViewModel.Email}</email>
             <name>{createCoachViewModel.Name}</name>
             <surname>{createCoachViewModel.Surname}</surname>
             <password>{createCoachViewModel.Password}</password>
             <city>{createCoachViewModel.City}</city>
             <birthDate>{createCoachViewModel.BirthDate:yyyy-MM-ddTHH:mm:ss}</birthDate>
             <tckn>{createCoachViewModel.TCKN}</tckn>
             <gender>{createCoachViewModel.Gender}</gender>
          </web:RegisterCoachUser>
       </soapenv:Body>
    </soapenv:Envelope>";

            try
            {
                // SOAP servisine istek gönderme
                string result = await SOAPCoachRegisterClient.CallSoapWebService(SoapEndpoint, soapXml);

                // Yanıtı döndürme (HTML sayfasında gösterme)
                return Content(result, "text/xml");  // Yanıtın doğru XML formatında olduğunu doğrulamak için
            }
            catch (Exception ex)
            {
                // Hata durumunda kullanıcıyı bilgilendir
                return Content($"SOAP request failed: {ex.Message}", "text/plain");
            }

        }
    }
}
