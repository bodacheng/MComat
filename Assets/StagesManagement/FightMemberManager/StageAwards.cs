#if UNITY_EDITOR
using System.Collections.Generic;
using Json;
using Newtonsoft.Json;

public partial class FightMemberManager
{
    public class StageAward
    {
        public string stageKey;
        public Award award;
    }

    public class Award
    {
        public int g;
        public int d;
    }
    
    public static void ExportStageAward()
    {
        List<StageAward> stageAwards = new List<StageAward>();
        for (int i = 1; i < 100 ; i++)
        {
            StageAward award = new StageAward
            {
                stageKey = i.ToString(),
                award = new Award
                {
                    g = 100,
                    d = 10,
                }
            };
            stageAwards.Add(award);
        }
        
        string json = JsonConvert.SerializeObject(stageAwards.ToArray());
        LocalJson.SaveInfoToJsonFile_dataPath(null, "stage_awards.json", json);
    }
}
#endif