using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Client.Models
{
    public class mClient
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        [Display(Name = "Created At")]
        [DataType(DataType.Date)]
        public DateTime Created_At { get; set; }

        public ICollection<mWork> Works { get; set; }
    }
}