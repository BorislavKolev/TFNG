namespace TFNG.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using NickBuhro.Translit;
    using TFNG.Data.CloudinaryHelper;
    using TFNG.Data.Models;
    using TFNG.Services.Data.Contracts;
    using TFNG.Web.ViewModels.Dances;
    using TFNG.Web.ViewModels.Gallery;

    public class GalleryController : Controller
    {
        private const int ItemsPerPage = 12;

        private readonly IGalleryService galleryService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly Cloudinary cloudinary;

        public GalleryController(IGalleryService galleryService, UserManager<ApplicationUser> userManager, Cloudinary cloudinary)
        {
            this.galleryService = galleryService;
            this.userManager = userManager;
            this.cloudinary = cloudinary;
        }

        public IActionResult AllCompetitions(string searchString, string filter, int page = 1)
        {
            var viewModel = new GalleryListViewModel();

            var count = this.galleryService.GetCompetitionPicturesCount();

            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            var pictures = this.galleryService.GetAllCompetition<GalleryListItemViewModel>(ItemsPerPage, (page - 1) * ItemsPerPage);

            viewModel.Pictures = pictures;

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }

        public IActionResult AllVacations(string searchString, string filter, int page = 1)
        {
            var viewModel = new GalleryListViewModel();

            var count = this.galleryService.GetVacationPicturesCount();

            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            var pictures = this.galleryService.GetAllVacation<GalleryListItemViewModel>(ItemsPerPage, (page - 1) * ItemsPerPage);

            viewModel.Pictures = pictures;

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }
    }
}
