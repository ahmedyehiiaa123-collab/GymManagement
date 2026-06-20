using GymManagement.BLL.Services.Classes;
using GymManagement.DAL.Data.Models;
using GymManagement.DAL.Resporitory.Inrerface;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.PL.Configuration
{
    public class MembersController : Controller
    {
        private readonly IMemberServices memberServices;

        public MembersController(IMemberServices memberServices)
        {
            this.memberServices = memberServices;
        }


        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var member = await memberServices.GetAllAsync(ct);
            return View(member);
        }

        [HttpGet]

        public async Task<IActionResult> Create() => View();



        public async Task<IActionResult> GetMemberDetailsByIdAsync(int id, CancellationToken ct)
        {
            var member = await memberServices.GetMemberDetailsByIdAsync(id, ct);
            if (member == null)
            {
                TempData["ErrorMessage"] = "Member NOt Found";
                return RedirectToAction(nameof(Index));

            }


            return View(member);



            //Delete //baseurl/members/delete/id{get}
            //delete post //baseurl/member/deleteconfirmed {id}
        }

        //showform
        //method checkmember by id

        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var member = await memberServices.GetMemberDetailsByIdAsync(id, ct);
            if(member == null)
            {
                TempData["ErrorMessage"] = "MembernotFound";
                return RedirectToAction(nameof(Index));
            }
            return View();

        }
    }
}
