using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsApplicationProj.Server.Models
{
    public class FileUrl
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public CourseApplication CourseApplication { get; set; }
        public int CourseApplicationId { get; set; }
        public string Url { get; set; }
    }
}
