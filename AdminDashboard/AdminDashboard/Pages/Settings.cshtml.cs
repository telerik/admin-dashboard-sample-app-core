using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminDashboard.Pages
{
    public class SettingsModel : PageModel
    {
        [BindProperty]
        public UserDetailsModel UserDetails { get; set; }
        [BindProperty]
        public UserInformationModel UserInformation { get; set; }
        public void OnGet()
        {
            if (UserDetails == null)
            {
                UserDetails = new UserDetailsModel();
            }
            if (UserInformation == null)
            {
                UserInformation = new UserInformationModel();
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
