using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Api.Dto.Model;
using DG.Tweening;
using Skill;

public partial class NineForShow : MonoBehaviour
{
    #region 演出用相关特效 这些东西可能不希望和其他特效一起带入战斗场景
    List<Decompositioner> screenStarModels = new List<Decompositioner>();
    List<Decompositioner> screenStarExplosionModels = new List<Decompositioner>();
    #endregion
    
    public void ClearGochaEffects()
    {
        for (int i = 0; i < screenStarModels.Count; i++)
        {
            screenStarModels[i].EnergyRessolve();
        }
        for (int i = 0; i < screenStarExplosionModels.Count; i++)
        {
            screenStarExplosionModels[i].EnergyRessolve();
        }
        screenStarModels.Clear();
        screenStarExplosionModels.Clear();
    }
    
    public IEnumerator OneStoneGochaAnim(StoneOfPlayerInfo stoneinfo, Vector3 waitposition, Vector3 endPosition)
    {
        SkillConfig skillConfig = SkillConfigTable.GetSkillConfigByID(stoneinfo.skillId);
        string screenstarname = "";
        string explosionname = "";
        switch(skillConfig.SP_LEVEL) // 这里应该是rarelevel
        {
            case 0:
                screenstarname = "normal_test_screenstar0";
                explosionname = "screenStarExplostionTest0";
            break;
            case 1:
                screenstarname = "normal_test_screenstar1";
                explosionname = "screenStarExplostionTest1";
            break;
            case 2:
                screenstarname = "normal_test_screenstar2";
                explosionname = "screenStarExplostionTest2";
            break;
            case 3:
                screenstarname = "normal_test_screenstar3";
                explosionname = "screenStarExplostionTest3";
            break;
        }
    
        Decompositioner screenStar = EffectsManager.GenerateEffect(screenstarname, FightGlobalSetting.EffectPathDefine(Zokusei.Null), waitposition, Quaternion.identity, null);
        screenStarModels.Add(screenStar);
        screenStar.transform.DOMove(endPosition, 2f);
        yield return new WaitForSeconds(2f);
        Decompositioner boo = EffectsManager.GenerateEffect(explosionname, FightGlobalSetting.EffectPathDefine(Zokusei.Null), endPosition, Quaternion.identity, null);
        screenStarExplosionModels.Add(boo);
    }

    public void GochaResultShow(List<StoneOfPlayerInfo> results)
    {
        string A1skillid = null, A2skillid= null, A3skillid= null, B1skillid= null, B2skillid= null, B3skillid= null, C1skillid= null, C2skillid= null, C3skillid= null;
        for (int i = 0; i < results.Count; i++)
        {
            switch(i)
            {
                case 0:
                A1skillid = results[i].skillId;
                break;
                case 1:
                A2skillid = results[i].skillId;
                break;
                case 2:
                A3skillid = results[i].skillId;
                break;
                case 3:
                B1skillid = results[i].skillId;
                break;
                case 4:
                B2skillid = results[i].skillId;
                break;
                case 5:
                B3skillid = results[i].skillId;
                break;
                case 6:
                C1skillid = results[i].skillId;
                break;
                case 7:
                C2skillid = results[i].skillId;
                break;
                case 8:
                C3skillid = results[i].skillId;
                break;
            }
        }
        ShowStones(A1skillid, A2skillid, A3skillid, B1skillid, B2skillid, B3skillid, C1skillid, C2skillid, C3skillid);
    }    
}