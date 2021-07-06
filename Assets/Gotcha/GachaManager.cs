using UnityEngine;
using System.Collections.Generic;
using Api.Dto.Model;
using mainMenu;
using dataAccess;
using Skill;
using System.Collections;

public class GachaManager : MonoBehaviour
{
    public Canvas GotchaCanvas;
    public RectTransform GotchaFrontT;
    public RectTransform GotchaResultT;
    public RectTransform MemberDetailT;
    public RectTransform SKillEditStoneBoxT;
    public NineForShow NineForShow;
    public SkillStoneDetail _skillStoneDetail;
    
    List<StoneOfPlayerInfo> Result;
    
    public static GachaManager target;
    
    void Awake()
    {
        target = this;
    }
       
    public List<StoneOfPlayerInfo> GetResult()
    {
        return Result;
    }
    
    public void OneTime()
    {
    }
    
    public void NineTimes()
    {
    }

    public void GetAllSK()
    {
        CloudScript.GrantStonesTest();
    }

    public void GetAllM()
    {
        CloudScript.GrantMonsterTest();
    }

    public void Remove25Stones()
    {
        CloudScript.Remove25Stones();
    }

    public void GachaTest()
    {
        //Server.RandomRemove25Items();
        CloudScript.GachaTest(temp);

        void temp(List<StoneOfPlayerInfo> stones)
        {
            Result = stones;
            PreScene.target.trySwitchToStep(MainSceneStep.GotchaAnim, true);
        } 
    }
}