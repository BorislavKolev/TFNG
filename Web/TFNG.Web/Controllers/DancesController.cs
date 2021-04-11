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
            var viewModel = new DancesListViewModel();

            var count = this.dancesService.GetDancesCount();

            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            var dances = this.dancesService.GetAll<DancesListItemViewModel>(ItemsPerPage, (page - 1) * ItemsPerPage);
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

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            var viewModel = new DancesCreateInputModel();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateAsync(DancesCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var imageUrl = await CloudinaryExtension.UploadSingleAsync(this.cloudinary, input.Picture);

            string latinName = Transliteration.CyrillicToLatin(input.Name, Language.Bulgarian);
            latinName = latinName.Replace(' ', '-');

            _ = await this.dancesService.CreateAsync(input.Name, input.Description, user.Id, imageUrl, latinName, input.FolkloreArea, input.VideoUrl);

            return this.RedirectToAction("ByName", new { name = latinName });
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id)
        {
            var danceToEdit = await this.dancesService.GetViewModelByIdAsync<DanceEditViewModel>(id);

            return this.View(danceToEdit);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(DanceEditViewModel danceToEdit)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var imageUrl = string.Empty;

            if (danceToEdit.Picture != null)
            {
                imageUrl = await CloudinaryExtension.UploadSingleAsync(this.cloudinary, danceToEdit.Picture);
            }

            string latinName = Transliteration.CyrillicToLatin(danceToEdit.Name, Language.Bulgarian);

            latinName = latinName.Replace(' ', '-');

            await this.dancesService.EditAsync(danceToEdit.Name, danceToEdit.Description, user.Id, imageUrl, latinName, danceToEdit.FolkloreArea, danceToEdit.VideoUrl, danceToEdit.Id);

            return this.RedirectToAction("ByName", new { name = latinName });
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Remove(int id)
        {
            var danceToDelete = await this.dancesService.GetViewModelByIdAsync<DanceDeleteViewModel>(id);

            return this.View(danceToDelete);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Remove(DanceDeleteViewModel danceToDelete)
        {
            await this.dancesService.DeleteByIdAsync(danceToDelete.Id);

            return this.RedirectToAction("All");
        }
    }
}
