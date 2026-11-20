namespace HKNGroup.Application.Features.ProjectImages
{
    public class ProjectImagesListDto : DtoGetBase
    {
        public IList<HKNGroup.Domain.Entities.ProjectImages>? ProjectImages { get; set; }
    }
}
