using Coffee.Entities;
using Coffee.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Coffee.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICrudlDrinkService _crudlDrinkService;
        private readonly ICrudlIngredientOfDrinkService _crudlIngredientOfDrinkService;
        private readonly ICrudlWalletService _crudlWalletService;
        private readonly ICrudlBillsService _crudlBillsService;
        private readonly ICrudlBillDrinkService _crudlBillDrinkService;
        private readonly ICrudlUserService _crudlUserService;
        private readonly ICrudlRoleService _crudlRoleService;




        public HomeController(ICrudlDrinkService crudlDrinkService,
            ICrudlIngredientOfDrinkService crudlIngredientOfDrinkService,
            ICrudlWalletService crudlWalletService,
            ICrudlBillsService crudlBillsService,
            ICrudlBillDrinkService crudlBillDrinkService,
            ICrudlUserService crudlUserService,
            ICrudlRoleService crudlRoleService)

        {
            _crudlDrinkService = crudlDrinkService;
            _crudlIngredientOfDrinkService = crudlIngredientOfDrinkService;
            _crudlWalletService = crudlWalletService;
            _crudlBillsService = crudlBillsService;
            _crudlBillDrinkService = crudlBillDrinkService;
            _crudlUserService = crudlUserService;
            _crudlRoleService = crudlRoleService;
        }

        [HttpGet]
        public async Task<IActionResult> UserFirstPage()
        {
            var wallets = await _crudlWalletService.GetOneWallet(int.Parse(Request.Cookies["userId"]));
            if (wallets == null)
            {
                await _crudlWalletService.CreateWallet(int.Parse(Request.Cookies["userId"]));
            }
            ViewData["UserId"] = Request.Cookies["userId"];
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AdminFirstPage()
        {
          return View();
        }

        [HttpGet]
        [ActionName("UserProfile")]
        public async Task<IActionResult> UserProfile()
        {
            var userId = Request.Cookies["userId"];
            if (userId != null)
            {
                var users = await _crudlUserService.GetUser(Convert.ToInt32(userId));
                var wallets = await _crudlWalletService.GetWalletByUserId(users.Id);
                var roles = await _crudlRoleService.GetRole(users.RoleId);
                ViewData["UserId"] = users.Id;
                ViewData["UserName"] = users.Name;
                ViewData["UserRole"] = roles.Name;
                if (wallets != null)
                {
                    ViewData["UserMoney"] = wallets.Money;
                }
                else
                    ViewData["UserMoney"] = "U have no money because you are an admin";
                return View();
            }
            return NotFound();
        }

        public async Task<IActionResult> MenuFirstPage()
        {
            var g = Guid.NewGuid();
            Response.Cookies.Append("BillFromMenuId", $"{g}");
            await _crudlBillsService.CreateFirstBill(g, Convert.ToInt32(Request.Cookies["userId"]));
            return View();
        }

        public async Task<IActionResult> Menu()
        {
            return View(await _crudlDrinkService.GetAllDrink());
        }

        public async Task<IActionResult> MenuPost(int id)
        {
            var wallet = await _crudlWalletService.GetWalletByUserId(Convert.ToInt32(Request.Cookies["userId"]));

            var drink = await _crudlDrinkService.GetDrink(id);

            await _crudlIngredientOfDrinkService.DecreaseIngredientAmount(id);

            await _crudlWalletService.WithdrawMoney(wallet, drink);

            await _crudlBillDrinkService.CreateBillDrink(Guid.Parse(Request.Cookies["BillFromMenuId"]), id, drink.Coast);

            return RedirectToAction("Menu");
        }

        [HttpGet]
        public async Task<IActionResult> BuyDrink()
        {
            return View(await _crudlBillDrinkService.GetAllBillDrink());
        }

        public async Task<IActionResult> BuyDrinkPost(Bill bill)
        {
            await _crudlBillDrinkService.GetSum(Guid.Parse(Request.Cookies["BillFromMenuId"]));
            return RedirectToAction("WaitingPage");
        }

        public async Task<IActionResult> DisplayBills()
        {
            return View(await _crudlBillsService.GetAllBill());
        }
        public async Task<IActionResult> WaitingPage()
        {
            return View();
        }

    }
}