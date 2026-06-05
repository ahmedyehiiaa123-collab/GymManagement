using GymManagement.DAL.Data.Models;
using GymManagement.DAL.Resporitory.Classes;
using GymManagement.DAL.Resporitory.Inrerface;
using GymManagement.Dbcontext;
using GymManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;

namespace GymManagement.DAL.Data.Controllers
{
    public class PlansController : Controller
    {

        private readonly IGenericRepository<Plan> planInterface;

        public PlansController(IGenericRepository<Plan> planInterface)
        {
            this.planInterface = planInterface;
        }

        public async Task<IActionResult> Index(CancellationToken ct)
        {

            var  plan = await planInterface.GetAllAsync(ct : ct);

            return  View(plan);  

        }
      public async Task<IActionResult> Details( int id, CancellationToken ct)
        {
            var plan = await planInterface.GetByIdAsync(id,CancellationToken ct);
            if (plan == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
                return View(plan);

        } 
        }


    }

