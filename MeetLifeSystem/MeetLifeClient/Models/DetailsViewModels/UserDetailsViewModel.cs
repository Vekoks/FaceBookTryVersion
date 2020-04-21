using MeetLife.Model;
using MeetLifeClient.Models.HomeViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MeetLifeClient.Models.DetailsViewModels
{
    public class UserDetailsViewModel
    {
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "{0} must be a natural number")]
        [Display(Name = "Age")]
        public int Age { get; set; }

        public string Adress { get; set; }

        public string ImageBrand { get; set; }

        public IEnumerable<FriendViewModel> Friends { get; set; }

        public IEnumerable<HomePostModel> Post { get; set; }

    }
}