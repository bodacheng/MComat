using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SkillIconsDic {

    static SkillIconsDic _instance;
    public static SkillIconsDic Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SkillIconsDic();
            }
            return _instance;
        }
    }
    
    readonly IDictionary<string, GameObject> _skillIconDic = new Dictionary<string, GameObject>();
}
