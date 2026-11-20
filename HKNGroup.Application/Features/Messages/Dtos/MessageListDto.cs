using System;
using System.Collections.Generic;
using System.Text;

namespace HKNGroup.Application.Features.Messages
{
    public class MessageListDto : DtoGetBase
    {
        public IList<Message>? Messages { get; set; }
    }
}

