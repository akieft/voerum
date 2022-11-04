using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Voerum.Models;

namespace Voerum.Controllers
{
    public class RecipeController : Controller
    {
        private VoerumDbContext db = new VoerumDbContext();

        //GET: Recipe/List?search={search}
        public ActionResult List(string search)
        {
            var recipes = db.Recipes.Where(r => r.Name.Contains(search) || search == null).ToList();
            var ingredients = db.Ingredients.Where(i => i.Text.Contains(search) || search == null).Include(i => i.Recipe).ToList();
            foreach (Ingredient i in ingredients)
            {
                if (!recipes.Contains(i.Recipe))
                {
                    recipes.Add(i.Recipe);
                }
            }
            ViewBag.SearchCount = recipes.Count();
            ViewBag.SearchString = search;
            return View(recipes);
        }

        //GET: Recipe/List?searchi={searchi}
        public ActionResult ListIngredients(string searchi)
        {
            var ingredients = db.Ingredients.Where(i => i.Text.Contains(searchi) || searchi == null);
            List<Recipe> recipes = new List<Recipe>();
            foreach (Ingredient i in ingredients)
            {
                if (!recipes.Contains(i.Recipe))
                {
                    recipes.Add(i.Recipe);
                }
            }
            ViewBag.SearchCount = recipes.Count();
            ViewBag.SearchString = searchi;
            return View(recipes);
        }

        //GET: Recipe/{id}
        public ActionResult Show(int id)
        {
            string userId = User.Identity.GetUserId();
            ViewBag.Check = db.Ratings.Where(r => r.UserId == userId && r.RecipeId == id).Count();
            var recipe = db.Recipes.
                Where(r => r.Id == id).
                Include(r => r.Ingredients).
                Include(r => r.Steps).First();

            return View(recipe);
        }

        public void CreateRating(short rating, int recipeId)
        {
            string userId = User.Identity.GetUserId();
            db.Ratings.Add(new Rating()
            {
                Value = rating,
                RecipeId = recipeId,
                UserId = userId
            });
            db.Recipes.Find(recipeId).SetAverage(recipeId);
            db.SaveChanges();
            Redirect("/");
        }
    }
}
