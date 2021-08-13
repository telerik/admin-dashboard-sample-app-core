using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AdminDashboard.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AdminDashboard.Pages
{
    [Authorize]
    public class SettingsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<SettingsModel> _logger;
        public SettingsModel(ILogger<SettingsModel> logger,
           UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
        }
        [BindProperty]
        public UserDetailsModel UserDetails { get; set; }
        [BindProperty]
        public UserInformationModel UserInformation { get; set; }
        public async Task OnGetAsync()
        {
            ClaimsPrincipal currentUser = User;
            
            var loggedUser = await _userManager.GetUserAsync(User);
            if (UserDetails == null)
            {
                UserDetails = new UserDetailsModel()
                {
                    Email = loggedUser.Email,
                    Username = loggedUser.UserName,
                    Phone = loggedUser.PhoneNumber,
                    Nickname = loggedUser.FullName
                };
            }
            if (UserInformation == null)
            {
                UserInformation = new UserInformationModel() 
                {
                    Website = "https://www.telerik.com/",
                    WorkPhone = loggedUser.PhoneNumber,
                    Country = "1",
                };
            }
        }
        public class UserDetailsModel
        {
            [Required]
            public string Username { get; set; }
            [Required]
            public string Nickname { get; set; }
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            public string Phone { get; set; }
        }
        public class UserInformationModel
        {
            public DateTime BirthDate { get; set; }
            public string Country { get; set; }
            public string Website { get; set; }
            public string WorkPhone { get; set; }
        }
    }
}
