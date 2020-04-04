using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;
using dataAccess;

public class gotchaManager : MonoBehaviour
{
    public PreScene preparingScene;
    public RectTransform gotchaCanvas;
    
    public void gotchaButtonFeature()
    {
        preparingScene.mainProcessRunner.Run(MySkillStonesReader.Instance.StoneGotcha());        
    }
}
