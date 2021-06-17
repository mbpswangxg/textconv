
namespace Text.Web
{
    public class WebParam
    {
        public string key;
        public string mark;
        public string value;
        public override string ToString()
        {
            return string.Format("key:{0}, mark:{1}, value:{2}", key, mark, value);
        }
    }

}
