using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextGrunt.Models;

namespace TextGrunt.Messages
{
    public class GotoMessage
    {
        public Sheet TargetSheet { get; set; }
        /// <summary>
        /// Can be null
        /// </summary>
        public Row TargetRow { get; set; }
    }
}
