using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBlog.HelpClass
{
    public class Utils
    {
        public static string MakeCutText(string text,int maxLenght)
        {
            if (text==null)
            {
                return null;
            }
            else if (text.Length>maxLenght)
            {
                return text.Substring(0, maxLenght)+ ".....";
            }
            else
            {
                return text;
            }
          
        }
    }
}