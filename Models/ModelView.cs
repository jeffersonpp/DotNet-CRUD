using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Client.Models
{
    public class ModelView
    {
        public ICollection<mWork> Works { get; set; }
        public ICollection<mClient> Clients { get; set; }

        public mWork Work { get; set; }
        public mClient Client { get; set; }

        public SelectList listClient { get; set; }
        public SelectList listWork { get; set; }
    }
}