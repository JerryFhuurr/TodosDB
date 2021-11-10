using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TodosDB2
{
    public class Todo
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Enter a number larger than 1")]
        [JsonPropertyName("userid")]
        public int UserId { get; set; }

        [JsonPropertyName("TodoId"), Key]
        public int TodoId { get; set; }

        [Required, MaxLength(128)]
        [JsonPropertyName("Tittle")]
        public string Title { get; set; }

        [JsonPropertyName("completed"), Required]
        public bool IsCompleted { get; set; }

        /*
         * Step2: Add packaages
         *  Add Microsoft.EntityFrameworkCore to your project
            Add Microsoft.EntityFrameworkCore.Design to your project
            Add Microsoft.EntityFrameworkCore.Sqlite to your project
         */
    }
}
