using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Client.Models
{
    public class mWork
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public mClient Client { get; set; }

        [Display(Name = "Started At")]
        [DataType(DataType.Date)]
        public DateTime Started_At { get; set; }


    }
}