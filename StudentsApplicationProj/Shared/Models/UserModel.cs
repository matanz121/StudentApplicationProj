using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApplicationProj.Shared.Models
{
    public class UserModel
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
