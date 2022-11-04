using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Voerum.Models
{
    public class Ingredient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Text { get; set; }
        /* For future improvements
        public string Name {get; set;}
        public string Measurement { get; set; }
        public string Amount { get; set; }
        */


        [JsonIgnore] [XmlIgnore]
        public Recipe Recipe { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }
    }
}
