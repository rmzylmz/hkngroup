using ProjectImages = HKNGroup.Domain.Entities.ProjectImages;

namespace HKNGroup.Application.Features.ProjectImages
{
    public class ProjectImagesAddDto : DtoGetBase
    {
        public int ProjectId { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string? URL { get; set; }
         public string? siteBaseUrl { get; set; }
    }
}


