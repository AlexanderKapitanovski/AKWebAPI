using System;
using System.Collections.Generic;

namespace MyWebApi
{
    public partial class Topic
    {
        public Topic()
        {
            Section = new HashSet<Section>();
        }

        public int TopicId { get; set; }
        public string Topic1 { get; set; }

        public virtual ICollection<Section> Section { get; set; }
    }
}
