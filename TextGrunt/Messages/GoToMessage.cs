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