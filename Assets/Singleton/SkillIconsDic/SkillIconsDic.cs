using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SkillIconsDic {

    static SkillIconsDic instance;
    public static SkillIconsDic Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SkillIconsDic();
            }
            return instance;
        }
    }
    
    IDictionary<string, GameObject> SkillIconDic = new Dictionary<string, GameObject>();

    public GameObject Get(string skillID)
    {
        GameObject readingSprite;
        SkillIconDic.TryGetValue(skillID, out readingSprite);
        if (readingSprite != null)
        {
            return readingSprite;
        }
        return null;
    }
}
