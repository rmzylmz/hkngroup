using System;
using System.Collections.Generic;
using System.Text;

namespace HKNGroup.Application.Features.Sliders
{
    public class SliderListDto : DtoGetBase
    {
        public IList<Slider>? Sliders { get; set; }
    }
}

