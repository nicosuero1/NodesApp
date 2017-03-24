using System.Text.RegularExpressions;
using System.Linq;
using System.Text;

namespace NodesApp.Helpers
{
    /// <summary>
    /// Helper to manage strings in the application
    /// </summary>
    public static class StringHelper
    {
        // Add before 'text' the string 'str' 'count' times 
        public static string AddIdentation(string text, int count, char str)
        {
            string [] lines = Regex.Split(text, "\r\n|\r|\n");
            if (lines.Length == 0) return "";
            StringBuilder sb = new StringBuilder();
            lines.ToList()
                  .ForEach(line => {
                        string newLine = new string(str, count) + @line;
                        sb.AppendLine(newLine);
                  });
            if (sb.Length > 0) {
                sb.Remove(sb.Length - 2, 2);
            }
            return sb.ToString();
        }
        public static string AddIdentation4(string text)
        {
            return AddIdentation(text, 4, ' ');
        }
    }
}
