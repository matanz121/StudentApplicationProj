﻿using StudentsApplicationProj.Shared.Enum;
using System;
using System.Collections.Generic;

namespace StudentsApplicationProj.Shared.Models
{
    public class CourseApplicationViewModel
    {
        public int Id { get; set; }
        public UserModel Student { get; set; }
        public CourseModel Course { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationBody { get; set; }
        public ApplicationStatus Status { get; set; }
        public DateTime? ApplicationDateTime { get; set; }
        public IList<FileUrlModel> FileUrls { get; set; }
    }
}