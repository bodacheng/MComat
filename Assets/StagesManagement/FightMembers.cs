using System;

[Serializable]
public class FightMembers
{
    [NonSerialized]
    public MultiDict<int, int, CharDataInfo> HeroSets = new MultiDict<int, int, CharDataInfo>();
    public MultiDict<int, int, CharDataInfo> EnemySets = new MultiDict<int, int, CharDataInfo>();
    
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