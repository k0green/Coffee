using Coffee.Entities;
using Coffee.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Coffee.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly ICrudlIngredientService _crudlIngredientService;

        public IngredientsController(ICrudlIngredientService crudlIngredientService)
        {
            _crudlIngredientService = crudlIngredientService;
        }
        public async Task<IActionResult> DisplayIngredients()
        {
            return View(await _crudlIngredientService.GetAllIngredient());
        }

        public IActionResult CreateIngredient()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateIngredient(Ingredient ingredient)
        {
            await _crudlIngredientService.CreateIngredient(ingredient);
            return RedirectToAction("DisplayIngredients");
        }
        public async Task<IActionResult> EditIngredient(int? id)
        {
            if (id != null)
            {
                return View(await _crudlIngredientService.UpdateIngredient(id));
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditIngredient(Ingredient ingredient)
        {
            await _crudlIngredientService.UpdateIngredient(ingredient);
            return RedirectToAction("DisplayIngredients");
        }

        [HttpGet]
        [ActionName("DeleteIngredient")]
        public async Task<IActionResult> ConfirmDeleteIngredient(int? id)
        {
            ViewData["IngredientId"] = $"id={id}";
            if (id != null)
            {
                return View(await _crudlIngredientService.ConfirmDeleteIngredient(id));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteIngredient(int? id)
        {
            if (id != null)
            {
                await _crudlIngredientService.DeleteIngredient(id);
                return RedirectToAction("DisplayIngredients");
            }
            return NotFound();
        }
    }
}
