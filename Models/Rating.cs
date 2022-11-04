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
    public class Rating
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Range(1,5)]
        public short Value { get; set; }
        
        //[ForeignKey("RecipeId")]
        [JsonIgnore]
        [XmlIgnore]
        public virtual Recipe Recipe { get; set; }
        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }

        //[ForeignKey("UserId")]
        [JsonIgnore]
        [XmlIgnore]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }


    }
}
