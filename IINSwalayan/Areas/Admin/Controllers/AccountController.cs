using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IINSwalayan.Models;

namespace IINSwalayan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var userEmail = User.Identity?.Name;

                // PENTING: SignOut dulu sebelum redirect
                await _signInManager.SignOutAsync();
                _logger.LogInformation($"Admin user {userEmail} logged out");

                // Return success status untuk JavaScript
                return Ok(new { success = true, redirectUrl = "/" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during admin logout");
                return BadRequest(new { success = false });
            }
        }
    }
}