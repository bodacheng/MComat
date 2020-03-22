using System.Collections.Generic;

public class StageIconObjectBank : BaseListBank
{    
    public void Initialize(List<StageScriptableObject> stages)
    {
        List<string> titles = new List<string>();
        for (int i = 0; i < stages.Count; i++)
        {
            if (!contents.ContainsKey(stages[i].LocalFightID))
            {
                contents.Add(stages[i].LocalFightID,stages[i].battleNameCH);
            }
        }
    }
    
    public void Clear()
    {
        contents.Clear();
    }

    readonly IDictionary<int, string> contents = new Dictionary<int, string>();

    public override string GetListContent(int index)
    {
        if (contents.ContainsKey(index))
            return contents[index];
        return null;
    }

    public override int GetListLength()
    {
        return contents.Count;
    }
}