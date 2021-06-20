using System;

[Serializable]
public class FightMembers
{
    [NonSerialized]
    public MultiDictionary<int, int, CharDataInfo> HeroSets = new MultiDictionary<int, int, CharDataInfo>();
    public MultiDictionary<int, int, CharDataInfo> EnemySets = new MultiDictionary<int, int, CharDataInfo>();
    
    public FightMembers()
    {
    }
    
    public void SetEnemyLevel(int level)
    {
        for (int i = 0; i < EnemySets.values.Count; i++)
        {
            EnemySets.values[i]._NineAndTwo.SetSkillLevel(level);
        }
    }
}