using System.Collections.Generic;
using System.Security.Cryptography;
using Voerum.Models;

namespace Voerum.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Voerum.Models.VoerumDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Voerum.Models.VoerumDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            Random random = new Random();
            List<Recipe> allRecipes = context.Recipes.ToList();
            foreach (var recipe in allRecipes)
            {
                for (int i = 1; i < (random.Next(2, 5) + allRecipes.Count) / i; i++)
                {
                    context.Ratings.AddOrUpdate(
                        r => r.RecipeId,
                        new Rating { Value = 5, RecipeId = recipe.Id },
                        new Rating { Value = (short)random.Next(1, 5), RecipeId = recipe.Id },
                        new Rating { Value = (short)random.Next(1, 5), RecipeId = recipe.Id },
                        new Rating { Value = (short)random.Next(1, 5), RecipeId = recipe.Id }, 
                        new Rating { Value = (short)random.Next(1, 5), RecipeId = recipe.Id },
                        new Rating { Value = (short)random.Next(1, 5), RecipeId = recipe.Id },
                        new Rating { Value = (short)random.Next(1, 5), RecipeId = recipe.Id }, 
                        new Rating { Value = (short)random.Next(1, 5), RecipeId = recipe.Id },
                        new Rating { Value = (short)random.Next(1, 5), RecipeId = recipe.Id },
                        new Rating { Value = (short)random.Next(1, 5), RecipeId = recipe.Id }, 
                        new Rating { Value = (short)random.Next(1, 5), RecipeId = recipe.Id },
                        new Rating { Value = (short)random.Next(1, 5), RecipeId = recipe.Id },
                        new Rating { Value = (short)random.Next(1, 5), RecipeId = recipe.Id }, 
                        new Rating { Value = (short)random.Next(1, 5), RecipeId = recipe.Id },
                        new Rating { Value = (short)random.Next(1, 5), RecipeId = recipe.Id },
                        new Rating { Value = (short)random.Next(1, 5), RecipeId = recipe.Id },
                        new Rating { Value = (short)random.Next(1, 5), RecipeId = recipe.Id }
                    );
                }
            }
        }
    }
}
