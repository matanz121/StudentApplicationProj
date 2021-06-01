using AutoMapper;
using StudentsApplicationProj.Server.Models;
using StudentsApplicationProj.Shared.Models;


namespace StudentsApplicationProj.Server.AutoMapper
{
    public class AutoMapperMap: Profile
    {
        public AutoMapperMap()
        {
            CreateMap<Course, CourseModel>();

            CreateMap<SystemUser, UserModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName));

            CreateMap<SystemUser, UserAccountModel>();

            CreateMap<Department, DepartmentModel>();

            CreateMap<FileUrl, FileUrlModel>();

            CreateMap<StudentCourse, CourseApplicationViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CourseApplication.Id))
                .ForMember(dest => dest.ApplicationName, opt => opt.MapFrom(src => src.CourseApplication.ApplicationName))
                .ForMember(dest => dest.ApplicationBody, opt => opt.MapFrom(src => src.CourseApplication.ApplicationBody))
                .ForMember(dest => dest.ApplicationDateTime, opt => opt.MapFrom(src => src.CourseApplication.ApplicationDateTime))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.CourseApplication.Status))
                .ForMember(dest => dest.NoteMessage, opt => opt.MapFrom(src => src.CourseApplication.NoteMessage))
                .ForMember(dest => dest.NoteFrom, opt => opt.MapFrom(src => src.CourseApplication.NoteFrom))
                .ForMember(dest => dest.FileUrls, opt => opt.MapFrom(src => src.CourseApplication.FileUrls));

            CreateMap<FileUrlModel, FileUrl>();

            CreateMap<CourseApplication, CourseApplicationViewModel>();

            CreateMap<CourseModel, Course>()
                .ForMember(dest => dest.Department, opt => opt.Ignore())
                .ForMember(dest => dest.CourseInstructorId, opt => opt.MapFrom(src => src.CourseInstructor.Id))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Department.Id))
                .ForMember(dest => dest.Department, opt => opt.Ignore())
                .ForMember(dest => dest.CourseInstructor, opt => opt.Ignore());

            CreateMap<ApplicationRequestFormModel, CourseApplication>();

            CreateMap<CourseApplication, ApplicationRequestFormModel>()
                .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.StudentCourse.CourseId));
        }
    }
}
