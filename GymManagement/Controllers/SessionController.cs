using GymManagement.BLL.Services.Classes.Interfaces;
using GymManagement.BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagement.PL.Controllers
{
    public class SessionController : Controller
    {
        public ISessionService SessionService { get; }

        public SessionController(ISessionService sessionService)
        {
            SessionService = sessionService;
        }

        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var result = await SessionService.GetallSessionsAsync(ct);

            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.Message;
                return View(new List<SessionViewModel>());
            }

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken ct)
        {
            ViewBag.Trainers = new SelectList(
                  await SessionService.GetTrainerDropdownlist(ct),
                            "Id",
                                "Name"
            );

            ViewBag.Categories = new SelectList(
          await SessionService.GetCategoryDropdownlist(ct),
                  "Id",
            "Name"
 );
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSessionViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await SessionService.CreateSessionAsync(model, ct);

            if (result)
            {
                TempData["Success"] = "Session Created";
                return RedirectToAction(nameof(Index));
            }

            TempData["Fail"] = "Session wasn't Created";
            return View(model);
        }
        public async Task<IActionResult> Details(int id, CancellationToken ct = default)
        {
            var result = await SessionService.GetSessionByid(id, ct);
            if (result.Success)
            {

                return View(result.Data);

            }
            else
            {
                TempData["Fail"] = "Session wasn't Created";
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id,CancellationToken ct = default)
        {
            //use method
            var result =  SessionService.GetSessionUpdate(id, ct);
            //validate
            if (result.IsCompletedSuccessfully)
            {
                ViewBag.Trainers = new SelectList(await SessionService.GetTrainerDropdownlist(), "Id", "Name");
                ViewBag.Categories = new SelectList(await SessionService.GetCategoryDropdownlist(), "Id", "Name"); // لو عندك كاتيغوري
                return View(result.Result);

            }
            else
            {
                TempData["Error"] = result.Exception;
                return RedirectToAction(nameof(Index));

            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, UpdateSesssionViewModel model, CancellationToken ct = default)
        {
            //use method
            var result = SessionService.UpdateSession(id, model, ct);


            //validate
            if (result.IsCompletedSuccessfully)
            {
                ViewBag.Trainers = new SelectList(await SessionService.GetTrainerDropdownlist(), "Id", "Name");
                ViewBag.Categories = new SelectList(await SessionService.GetCategoryDropdownlist(), "Id", "Name"); // لو عندك كاتيغوري

                return View(nameof(Index));


            }
            else
            {
                TempData["Error"] = result.Exception;
                return RedirectToAction(nameof(Index));

            }

        }
            [HttpGet]
            //ده هيظهرلك مسدج الديليت كونفريمشن
            public async Task<IActionResult> Delete(int id, CancellationToken ct = default)
        {
            var result = await SessionService.GetSessionByid(id, ct);
            if (result.Success)
            {
                return View(result.Data);

            }
            else
            {
                TempData["Error"] = result.Data;
                return RedirectToAction(nameof(Index));
            }


        }
        //ده هينفذ ويبعتك للاندكس
            [HttpPost]

            public async Task<IActionResult> DeleteConfirmation(int id, CancellationToken ct = default)
        {
            var result = await SessionService.DeleteSession(id, ct);
            if (result.Success)
            {
                TempData["Success"] = "Data Deleted ";
                return RedirectToAction(nameof(Index));

            }
            else
            {
                TempData["Error"] = result.Data;
                return RedirectToAction(nameof(Index));
            }


        }
        }
    }
