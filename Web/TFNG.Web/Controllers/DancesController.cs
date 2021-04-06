namespace TFNG.Web.Controllers
{
    using System;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TFNG.Data.Models;
    using TFNG.Services.Data.Contracts;
    using TFNG.Web.ViewModels.Dances;

    public class DancesController : Controller
    {
        private const int ItemsPerPage = 6;

        private readonly IDancesService dancesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly Cloudinary cloudinary;

        public DancesController(IDancesService dancesService, UserManager<ApplicationUser> userManager, Cloudinary cloudinary)
        {
            this.dancesService = dancesService;
            this.userManager = userManager;
            this.cloudinary = cloudinary;
        }

        public IActionResult All(string searchString, string filter, int page = 1)
        {
            //this.ViewData["CurrentFilter"] = filter;
            //this.ViewData["CurrentSearchString"] = searchString;

            var viewModel = new DancesListViewModel();

            var count = this.dancesService.GetDancesCount();

            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            var dances = this.dancesService.GetAll<DancesListItemViewModel>(ItemsPerPage, (page - 1) * ItemsPerPage);

            //if (!string.IsNullOrEmpty(filter))
            //{
            //    awards = awards.Where(l => l.Type == filter);
            //}

            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    locations = locations.Where(l => l.Name.ToLower().Contains(searchString.ToLower()) || l.Description.ToLower().Contains(searchString.ToLower()));
            //}

            viewModel.Dances = dances;

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }

        public IActionResult ByName(string name)
        {
            var danceViewModel = this.dancesService.GetByName<DanceDetailsViewModel>(name);

            if (danceViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(danceViewModel);
        }
    }
}
