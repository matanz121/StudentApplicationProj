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
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserAccount.FirstName));

            CreateMap<SystemUser, UserAccountModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserAccount.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.UserAccount.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.UserAccount.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserAccount.Email))
                .ForMember(dest => dest.AccountStatus, opt => opt.MapFrom(src => src.UserAccount.AccountStatus))
                .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => src.UserAccount.UserRole));

            CreateMap<Department, DepartmentModel>();

            CreateMap<FileUrl, FileUrlModel>();

            CreateMap<StudentCourse, CourseApplicationViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CourseApplication.Id))
                .ForMember(dest => dest.ApplicationName, opt => opt.MapFrom(src => src.CourseApplication.ApplicationName))
                .ForMember(dest => dest.ApplicationBody, opt => opt.MapFrom(src => src.CourseApplication.ApplicationBody))
                .ForMember(dest => dest.ApplicationDateTime, opt => opt.MapFrom(src => src.CourseApplication.ApplicationDateTime))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.CourseApplication.Status))
                .ForMember(dest => dest.FileUrls, opt => opt.MapFrom(src => src.CourseApplication.FileUrls));

            CreateMap<FileUrlModel, FileUrl>();

            CreateMap<CourseApplication, CourseApplicationViewModel>();

            CreateMap<CourseModel, Course>()
                .ForMember(dest => dest.Department, opt => opt.Ignore())
                .ForMember(dest => dest.CourseInstructorId, opt => opt.MapFrom(src => src.CourseInstructor.Id))
                .ForMember(dest => dest.CourseInstructor, opt => opt.Ignore());

            CreateMap<ApplicationRequestFormModel, CourseApplication>();
        }
    }
}
