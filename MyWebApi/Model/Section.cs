using System;
using System.Collections.Generic;

namespace MyWebApi
{
    public partial class Section
    {
        public Section()
        {
            Line = new HashSet<Line>();
        }

        public int SectionId { get; set; }
        public int TopicId { get; set; }
        public string Timeintervall { get; set; }
        public string Section1 { get; set; }

        public virtual Topic Topic { get; set; }
        public virtual ICollection<Line> Line { get; set; }
    }
}
