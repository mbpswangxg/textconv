using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextConv
{
    public class CommentConfigItem
    {
        public List<string> fileExtension = new List<string>();
        public string commentHeader;
        public string commentMarker;
        public string commentFooter;
    }
    public class CommentConfig
    {
        public List<CommentConfigItem> rules = new List<CommentConfigItem>();
    }
}
