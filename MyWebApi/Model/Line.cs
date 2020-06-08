using System;
using System.Collections.Generic;

namespace MyWebApi
{
    public partial class Line
    {
        public int SectionId { get; set; }
        public int LineId { get; set; }
        public string Linetitel { get; set; }
        public string Linetext { get; set; }

        public virtual Section Section { get; set; }
    }
}
