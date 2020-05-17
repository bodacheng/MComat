using System.Collections.Generic;

namespace mainMenu
{
    public class TutorialLog
    {
        public TutorialStep step;
        public string description;
    }

    public static class TutorialLogger
    {
        public static List<TutorialLog> Logs = new List<TutorialLog>();
    }
}