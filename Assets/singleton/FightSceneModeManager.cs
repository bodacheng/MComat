using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSceneModeManager
{
    // 这个单例模式的目的在于在从战斗准备页面把“开的是什么模式”这个信息给传递到战斗场景中去。
    // 因为我们现在是为了管理上的方便把各种战斗模式统合在一个场景中。但这样的话就为了那么个信息建设这样一个单例可能不太简洁，考虑把这个信息和teamset合并。
    private static FightSceneModeManager instance;
    SceneMode mode;

    private FightSceneModeManager()
    {
        mode = SceneMode.localDebug;
    }

    public void setSceneMode(SceneMode mode)
    {
        this.mode = mode;
    }

    public SceneMode getSceneMode()
    {
        return this.mode;
    }

    public static FightSceneModeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FightSceneModeManager();
            }
            return instance;
        }
    }
}
