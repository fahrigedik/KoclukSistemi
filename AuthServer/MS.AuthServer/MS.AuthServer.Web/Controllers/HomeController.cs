using System.Data;
using Microsoft.AspNetCore.Mvc;
using MS.AuthServer.Web.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MS.AuthServer.Data;
using MS.AuthServer.Web.ViewModels;
using Npgsql;

namespace MS.AuthServer.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _appDbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var unverifiedCountParam = new NpgsqlParameter
            {
                ParameterName = "p_unverified_count",
                NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer,
                Direction = ParameterDirection.Output
            };

            await _appDbContext.Database.ExecuteSqlRawAsync(
                "CALL sp_GetUnverifiedUsers(NULL);",
                unverifiedCountParam);


            var lockedCountParam = new NpgsqlParameter
            {
                ParameterName = "p_locked_count",
                NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer,
                Direction = ParameterDirection.Output
            };

            await _appDbContext.Database.ExecuteSqlRawAsync(
                "CALL sp_GetLockedUsersCount(NULL);",
                lockedCountParam);

            ViewBag.LockedUsersCount = Convert.ToInt32(lockedCountParam.Value);
            ViewBag.UnverifiedUsersCount = Convert.ToInt32(unverifiedCountParam.Value);

            return View();
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



        public async Task<IActionResult> AllRoles()
        {
            var roles = await _appDbContext.Database
                .SqlQuery<AllRolesViewModel>($"SELECT * FROM vw_AllRoles")
                .ToListAsync();

            return View(roles);
        }

        public async Task<IActionResult> AllUsers()
        {
            var users = await _appDbContext.Database
                .SqlQuery<AllUsersViewModel>($"SELECT * FROM vw_AllUsers")
                .ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> EmailVerificationStatus()
        {
            var emailVerificationStatus = await _appDbContext.Database
                .SqlQuery<EmailVerificationStatusViewModel>($"SELECT * FROM vw_EmailVerificationStatus")
                .ToListAsync();
            return View(emailVerificationStatus);
        }

        public async Task<IActionResult> ExpiredRefreshToken()
        {
            var expiredRefreshTokens = await _appDbContext.Database
                .SqlQuery<ExpiredRefreshTokenViewModel>($"SELECT * FROM vw_ExpiredRefreshTokens")
                .ToListAsync();
            return View(expiredRefreshTokens);
        }

        public async Task<IActionResult> UserRoles()
        {
            var userRoles = await _appDbContext.Database
                .SqlQuery<UserRolesViewModel>($"SELECT * FROM vw_UserRoles")
                .ToListAsync();
            return View(userRoles);
        }
    }
}


