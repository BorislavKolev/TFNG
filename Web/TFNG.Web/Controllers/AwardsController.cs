namespace TFNG.Web.Controllers
{
    using System;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TFNG.Data.Models;
    using TFNG.Services.Data.Contracts;
    using TFNG.Web.ViewModels.Awards;

    public class AwardsController : Controller
    {
        private const int ItemsPerPage = 6;

        private readonly IAwardsService awardsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly Cloudinary cloudinary;

        public AwardsController(IAwardsService awardsService, UserManager<ApplicationUser> userManager, Cloudinary cloudinary)
        {
            this.awardsService = awardsService;
            this.userManager = userManager;
            this.cloudinary = cloudinary;
        }

        public IActionResult All(string searchString, string filter, int page = 1)
        {
            //this.ViewData["CurrentFilter"] = filter;
            //this.ViewData["CurrentSearchString"] = searchString;

            var viewModel = new AwardsListViewModel();

            var count = this.awardsService.GetAwardsCount();

            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            var awards = this.awardsService.GetAll<AwardsListItemViewModel>(ItemsPerPage, (page - 1) * ItemsPerPage);

            //if (!string.IsNullOrEmpty(filter))
            //{
            //    awards = awards.Where(l => l.Type == filter);
            //}

            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    locations = locations.Where(l => l.Name.ToLower().Contains(searchString.ToLower()) || l.Description.ToLower().Contains(searchString.ToLower()));
            //}

            viewModel.Awards = awards;

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }
    }
}
