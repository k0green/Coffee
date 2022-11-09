using Coffee.Entities;
using Coffee.Interfaces;
using Coffee.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Coffee.Controllers
{
    public class IngredientOfDrinkController : Controller
    {
        private readonly ICrudlIngredientOfDrinkService _crudlIngredientOfDrinkService;

        public IngredientOfDrinkController(ICrudlIngredientOfDrinkService crudlIngredientOfDrinkService)
        {
            _crudlIngredientOfDrinkService = crudlIngredientOfDrinkService;
        }

        public async Task<IActionResult> DisplayIngredientOfDrink()
        {
            return View(await _crudlIngredientOfDrinkService.GetAllDrinkingredient());
        }

        public IActionResult CreateIngredientOfDrink()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateIngredientOfDrink(DrinksingredientItemModel drinkIngredientItemModel)
        {
            await _crudlIngredientOfDrinkService.CreateDrinksingredient(drinkIngredientItemModel);
            return RedirectToAction("DisplayIngredientOfDrink");
        }
        public async Task<IActionResult> EditIngredientOfDrink(int? idd, int? idi)
        {
            if (idd != null && idi != null)
            {
                return View(await _crudlIngredientOfDrinkService.UpdateDrinkingredient(idd, idi));
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditIngredientOfDrink(DrinksingredientItemModel drinksIngredientItemModel)
        {
            await _crudlIngredientOfDrinkService.UpdateDrinksingredient(drinksIngredientItemModel);
            return RedirectToAction("DisplayIngredientOfDrink");
        }

        [HttpGet]
        [ActionName("DeleteIngredientOfDrink")]
        public async Task<IActionResult> ConfirmDeleteIngredientOfDrink(int? idd, int? idi)
        {
            if (idd != null && idi != null)
            {
                ViewData["IngredientAndDrinkId"] = $"idd={idd}&idi={idi}";
                return View(await _crudlIngredientOfDrinkService.ConfirmDeleteDrinkingredient(idd, idi));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteIngredientOfDrink(int? idd, int? idi)
        {
            if (idd != null && idi != null)
            {
                await _crudlIngredientOfDrinkService.DeleteDrinkingredient(idd, idi);
                return RedirectToAction("DisplayIngredientOfDrink");
            }
            return NotFound();
        }
    }
}
