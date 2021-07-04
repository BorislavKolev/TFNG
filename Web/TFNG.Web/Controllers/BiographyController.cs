namespace TFNG.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using TFNG.Web.ViewModels;

    public class BiographyController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
