using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;
using dataAccess;

public class gotchaManager : MonoBehaviour
{
    public preparingScene2 preparingScene;
    public RectTransform gotchaCanvas;
    
    public void gotchaButtonFeature()
    {
        preparingScene.mainProcessRunner.TriggerMainProcess(MySkillStonesReader.Instance.StoneGotcha());        
    }
}
