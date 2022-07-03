using System.Collections.Generic;

namespace Cocone.ProjectP3
{
    public class ReleaseThemeList
    {
        public List<string> list { get; private set; }

        public bool IsReleasedItem(string itemId)
        {
            var theme = itemId.Substring(1, 5);
            return list.Contains(theme);
        }
        
        public static ReleaseThemeList Deserialize(string path)
        {
            return YamlDeserializer.Deserialize<ReleaseThemeList>(path);
        }
    }
}