using System;
using System.Collections.Generic;
using System.Text;

namespace HKNGroup.Application.Features.References
{
    public class ReferanceListDto : DtoGetBase
    {
        public IList<Referance>? Referances { get; set; }
    }
}

