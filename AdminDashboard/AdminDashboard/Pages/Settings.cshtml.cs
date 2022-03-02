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
using Microsoft.AspNetCore.Mvc.Rendering;
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
        [BindProperty]
        public UserSecurityModel UserSecurityDetails { get; set; }
        [BindProperty]
        public PaymentDetailsModel PaymentDetails { get; set; }
        [BindProperty]
        public NotificationAlertsModel NotificationAlerts { get; set; }
        [BindProperty]
        public NotificationEmailsModel NotificationEmails { get; set; }
        [BindProperty]
        public List<SelectListItem> CountryOptions { get; set; }
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
            if (UserSecurityDetails == null)
            {
                UserSecurityDetails = new UserSecurityModel()
                {
                    CurrentPassword = "",
                    NewPassword = "",
                    Enable2FA = loggedUser.TwoFactorEnabled,
                    RequestChange = true
                };
            }
            if (CountryOptions == null)
            {
                CountryOptions = new List<SelectListItem>() {
                                  new SelectListItem() {
                                      Text = "USA",
                                      Value = "1"
                                  },
                                  new SelectListItem() {
                                      Text = "United Kingdom",
                                      Value = "2"
                                  },
                                  new SelectListItem() {
                                      Text = "Bulgaria",
                                      Value = "3"
                                  }
                              };
            }
            if (PaymentDetails == null)
            {
                PaymentDetails = new PaymentDetailsModel() 
                {
                    CardHolder = loggedUser.FullName,
                    CardHolderCountry = "1",
                    ZipCode = "12345"
                          
                };
            }
            if (NotificationAlerts == null)
            {
                NotificationAlerts = new NotificationAlertsModel()
                {
                    Communication = true,
                    Security = false,
                    Meetups = true
                };
            }
            if (NotificationEmails == null)
            {
                NotificationEmails = new NotificationEmailsModel()
                {
                    Photos = false,
                    Mentions = false,
                    ReplyToGroup = true
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

        public class UserSecurityModel
        {
            public string CurrentPassword { get; set; }
            public string NewPassword { get; set; }
            public bool Enable2FA { get; set; }
            public bool RequestChange { get; set; }
        }

        public class PaymentDetailsModel
        {
            public string CardHolder { get; set; }
            public string CardNumber { get; set; }
            public DateTime? ExpirationDate { get; set; }
            public string CVCNumber { get; set; }
            public string CardHolderCountry { get; set; }
            public string ZipCode { get; set; }
        }
        public class NotificationAlertsModel
        {
            public bool Communication { get; set; }
            public bool Security { get; set; }
            public bool Meetups { get; set; }
        }
        public class NotificationEmailsModel
        {
            public bool Photos { get; set; }
            public bool Mentions { get; set; }
            public bool ReplyToGroup { get; set; }
        }
    }
}
