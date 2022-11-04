using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Voerum.Models
{
    public class Step
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Text { get; set; }

        [DefaultValue(1)]
        public int Order { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public Recipe Recipe { get; set; }
        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }
    }
}
