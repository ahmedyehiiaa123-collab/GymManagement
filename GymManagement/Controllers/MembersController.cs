using GymManagement.BLL.Services.Classes;
using GymManagement.DAL.Data.Models;
using GymManagement.DAL.Resporitory.Inrerface;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.PL.Configuration
{
    public class MembersController : Controller
    {
        public IMemberServices MemberServices { get; }
        public MembersController(IMemberServices memberServices)
        {
            MemberServices = memberServices;
        }


        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var member = await MemberServices.GetAllAsync(ct);
            return View(member);
        }

        [HttpGet]

        public async Task<IActionResult> Create()=>  View();
    }
}
