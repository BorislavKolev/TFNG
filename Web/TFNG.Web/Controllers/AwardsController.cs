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
            var viewModel = new AwardsListViewModel();

            var count = this.awardsService.GetAwardsCount();

            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            var awards = this.awardsService.GetAll<AwardsListItemViewModel>(ItemsPerPage, (page - 1) * ItemsPerPage);

            viewModel.Awards = awards;

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            var viewModel = new AwardsCreateInputModel();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateAsync(AwardsCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var imageUrl = await CloudinaryExtension.UploadSingleAsync(this.cloudinary, input.Picture);

            string latinName = Transliteration.CyrillicToLatin(input.Name, Language.Bulgarian);
            latinName = latinName.Replace(' ', '-');

            _ = await this.awardsService.CreateAsync(input.Name, latinName, input.Date, input.Location, input.Place, imageUrl, user.Id);

            return this.RedirectToAction("All");
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id)
        {
            var awardToEdit = await this.awardsService.GetViewModelByIdAsync<AwardsEditViewModel>(id);

            return this.View(awardToEdit);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(AwardsEditViewModel awardToEdit)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var imageUrl = string.Empty;

            if (awardToEdit.Picture != null)
            {
                imageUrl = await CloudinaryExtension.UploadSingleAsync(this.cloudinary, awardToEdit.Picture);
            }

            string latinName = Transliteration.CyrillicToLatin(awardToEdit.Name, Language.Bulgarian);

            latinName = latinName.Replace(' ', '-');

            await this.awardsService.EditAsync(awardToEdit.Name, latinName, awardToEdit.Date, awardToEdit.Location, awardToEdit.Place, imageUrl, user.Id, awardToEdit.Id);

            return this.RedirectToAction("All");
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Remove(int id)
        {
            var awardToDelete = await this.awardsService.GetViewModelByIdAsync<AwardsDeleteViewModel>(id);

            return this.View(awardToDelete);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Remove(AwardsDeleteViewModel awardToDelete)
        {
            await this.awardsService.DeleteByIdAsync(awardToDelete.Id);

            return this.RedirectToAction("All");
        }
    }
}
