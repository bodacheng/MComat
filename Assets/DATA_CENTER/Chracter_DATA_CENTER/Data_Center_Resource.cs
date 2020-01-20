using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Data_Center : MonoBehaviour
{
    public IEnumerator step2InitializeByResourceFolder(string type, TextAsset Script, Zokusei _zokusei, string personalMagic)
    {
        if (!phase2Initialized)
        {
            AIStateRunner.characterType = type;
            phase2Initialized = true;
        }

        if (AIStateRunner.usingScript != Script)
        {
            AIStateRunner.LoadStatesTransition(type, Script);//这个环节之后我应该有一份列表来展示到底我一个角色一场战斗都能用上什么招
                                                                       // 上面这个环节结束后，有这样几个重要情况1. state_Transition_Dictionary的内容就正确了 2.AIStateRunner内的States_Dictionary实例内将有一份正确的skill类key的列表
            AIStateRunner.IniStates(this);
            List<string> toLoadSkillAnimsNames = AIStateRunner.PassSkillTypeKeys();
            yield return (
                Animation_Manger.preloadPersonalAnimsResourceMode(type, toLoadSkillAnimsNames, personalMagic, _zokusei));
            //本函数里隐藏着一个相当大头的工作，那就是提前根据动画片段生成所有可能的对象池，牵扯到各种路径的确定，BO_E必须提前做好准备
        }

        yield return null;
    }
}
