using System.Text.RegularExpressions;

namespace TextConv
{
    public class LineMatch 
    {
        public int lineNo;
        public Match Match;

        public LineMatch(int lineNo, Match match)
        {
            this.lineNo = lineNo;
            this.Match = match;
        }
    }
}