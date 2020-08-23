using System.Collections.Generic;

public struct LevelCal
{
    static LevelCal instance;
    static Dictionary<int, int> LevelExp;
    public static LevelCal Instance
    {
        get
        {
            if (LevelExp == null)
            {
                LevelExp = new Dictionary<int, int>();
                for (int i = 1; i < 100; i++)
                {
                    LevelExp.Add(i, 100);
                }
            }
            return instance;
        }
    }
        
    public int GetLevelExp(int level)
    {
        if (LevelExp.ContainsKey(level))
        {
            return LevelExp[level];
        }
        return -1;
    }
    
    public Current GetCurrentInfo(int exp)
    {
        int currentLevel = 1;
        while (exp >= 0)
        {
            int remain = exp - LevelExp[currentLevel];
            if (remain >= 0)
            {
                currentLevel++;
                exp -= LevelExp[currentLevel];
            }
            else
            {
                Current current = new Current
                {
                    currentLevel = currentLevel,
                    expRemain = exp,
                    expToNextLevel = LevelExp[currentLevel] - exp
                };
                return current;
            }
        }
        Current level1 = new Current
        {
            currentLevel = currentLevel,
            expRemain = exp
        };
        return level1;
    }

    public struct Current
    {
        public int currentLevel;
        public int expRemain;
        public int expToNextLevel;
    }
}