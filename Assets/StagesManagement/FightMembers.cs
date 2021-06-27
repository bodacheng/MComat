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
        foreach (var charData in EnemySets.GetValues())
        {
            charData._NineAndTwo.SetSkillLevel(level);
        }
    }
}