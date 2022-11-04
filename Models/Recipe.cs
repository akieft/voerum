using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Voerum.Models
{
    public class Recipe
    { 
        public Recipe()
        {
            SetAverage(Id);
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(64, MinimumLength = 4)]
        public string Name { get; set; }     
        public int AverageRating { get; set; }

        [ForeignKey("RecipeId")]
        public virtual ICollection<Rating> Ratings { get; set; }
        [ForeignKey("RecipeId")]
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        [ForeignKey("RecipeId")]
        public virtual ICollection<Step> Steps { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; internal set; }

        public void SetAverage(int id)
        {
            int result = 0;
            List<Rating> ratings = new List<Rating>();
            using (VoerumDbContext db = new VoerumDbContext())
            {
                ratings = db.Ratings.Where(r => r.RecipeId == id).ToList();
            }

            foreach (var rating in ratings)
            {
                result = +rating.Value;
            }

            int devider = 1;

            if (devider < ratings.Count)
            {
                devider = ratings.Count;
            }

            this.AverageRating = result * 100 / devider;
        }
    }
}
