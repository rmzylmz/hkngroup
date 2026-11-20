
namespace HKNGroup.Application.Features.ProjectCategories
{
    public class ProjectCategoryListDto : DtoGetBase
    {
        public IList<ProjectCategory>? ProjectCategories { get; set; }
    }
}

