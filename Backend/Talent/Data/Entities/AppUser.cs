using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Talent.Data.Entities
{
    public class AppUser : IdentityUser
    {
        public List<DataSource> DataSources { get; set; }
        public List<Pipeline> Pipelines { get; set; }
    }
}
