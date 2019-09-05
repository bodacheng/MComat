using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

// 在这个管理一切的单例模式里我们需要一个关卡加载失败flag。因为现在关卡的初始化流程需要运行多个模块的协程，
//而协程没有返回值一说，那你没法靠单线程上的一些东西去判断这些初始化过程到底有没有问题
public partial class FightLoadError {

    private static FightLoadError instance;

    public List<string> FightLoadErrors = new List<string>();
    public static FightLoadError Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FightLoadError();
            }
            return instance;
        }
    }
}
