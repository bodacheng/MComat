#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class SKillAnalyzer : MonoBehaviour
{
    public void SkillsAnalyzeByFrames(string type, string targetEventName, float start_min, float start_max, float end_min, float end_max)
    {
        List<UnityEngine.Object> G_Attack_States = Resources.LoadAll("Animations/" + type + "/G_Attack_State", typeof(AnimationClip)).ToList();
        List<UnityEngine.Object> G_Attack_State_Stays = Resources.LoadAll("Animations/" + type + "/G_Attack_State_Stay", typeof(AnimationClip)).ToList();
        List<UnityEngine.Object> GMStatess = Resources.LoadAll("Animations/" + type + "/GMStates", typeof(AnimationClip)).ToList();

        List<AnimationClip> AnimationClips = new List<AnimationClip>();
        foreach (UnityEngine.Object _object in G_Attack_States)
        {
            AnimationClips.Add(_object as AnimationClip);
        }
        foreach (UnityEngine.Object _object in G_Attack_State_Stays)
        {
            AnimationClips.Add(_object as AnimationClip);
        }
        foreach (UnityEngine.Object _object in GMStatess)
        {
            AnimationClips.Add(_object as AnimationClip);
        }
        foreach (AnimationClip _clip in AnimationClips)
        {
            if (SkillFrameAnalyze(_clip, targetEventName, start_min, start_max, end_min, end_max))
            {
                Debug.Log("符合：" + _clip.name);
            }
        }
    }

    readonly List<string> AttackFrameStartMethodNames = new List<string>() {
        "SetRightHandMarkerManager","SetLeftHandMarkerManager",
        "SetRightFootMarkerManager","SetLeftFootMarkerManager",
        "SetRightHandWeaponMarkerManager","SetLeftHandWeaponMarkerManager",
        "SetHeadMarkerManager","SetTailMarkerManager",
        "SetAllBodyMarkerManagersIn",
    };
    readonly List<string> EffectsAttackFrameStartMethodNames = new List<string>()
    {
        "MagicForward","Bullet_shoot_from_body_part","BlastAttack","ReleasePreparedMagic","ReleasePreparedMagicToAir"
    };

    public void ReplaceAnimEventName(string type, string old_name, string new_name)
    {
        if (!(!string.IsNullOrEmpty(old_name)&& new_name != null && new_name != ""))
        {
            return;
        }

        List<UnityEngine.Object> BasicPack = Resources.LoadAll("Animations/" + type + "/" + "BasicPack", typeof(AnimationClip)).ToList();
        List<UnityEngine.Object> G_Attack_States = Resources.LoadAll("Animations/" + type + "/" + "G_Attack_State", typeof(AnimationClip)).ToList();
        List<UnityEngine.Object> G_Attack_State_Stays = Resources.LoadAll("Animations/" + type + "/" + "G_Attack_State_Stay", typeof(AnimationClip)).ToList();
        List<UnityEngine.Object> GMStatess = Resources.LoadAll("Animations/" + type + "/" + "GMStates", typeof(AnimationClip)).ToList();

        List<UnityEngine.Object> AnimationClips = new List<UnityEngine.Object>();
        foreach (UnityEngine.Object _object in BasicPack)
        {
            AnimationClips.Add(_object as AnimationClip);
        }
        foreach (UnityEngine.Object _object in G_Attack_States)
        {
            AnimationClips.Add(_object as AnimationClip);
        }
        foreach (UnityEngine.Object _object in G_Attack_State_Stays)
        {
            AnimationClips.Add(_object as AnimationClip);
        }
        foreach (UnityEngine.Object _object in GMStatess)
        {
            AnimationClips.Add(_object as AnimationClip);
        }
        foreach (UnityEngine.Object _clip in AnimationClips)
        {
            AnimationClip animationClip = _clip as AnimationClip;
            bool changed = false;
            List<AnimationEvent> evnets = new List<AnimationEvent>();
            foreach (AnimationEvent e in animationClip.events)
            {
                AnimationEvent toSave = e;
                if (e.functionName == old_name)
                {
                    toSave.functionName = new_name;
                    changed = true;
                    Debug.Log("讲动画片段：" + animationClip.name + "的函数" + old_name + "换了新名字" + new_name);
                }
                evnets.Add(toSave);
            }
            if (changed)
            {
                AnimationClip toSave = (AnimationClip)UnityEngine.Object.Instantiate(animationClip);
                AnimationUtility.SetAnimationEvents(toSave, evnets.ToArray());
                AssetDatabase.CreateAsset(toSave, AssetDatabase.GetAssetPath(_clip));
                AssetDatabase.SaveAssets();
            }
        }
    }

    public bool SkillFrameAnalyze(AnimationClip _clip, string targetEventName, float start_min, float start_max, float end_min, float end_max)
    {
        float earlieststartframe = 0, latestendframe;
        float cancelflagFrame = 0;
        List<float> allAttackStartFrames = new List<float>();

        bool hasTargetEventName = false;

        foreach (AnimationEvent e in _clip.events)
        {
            if (e.functionName == "SetAllBodyMarkerManagersIn")
            {
                allAttackStartFrames.Add(e.time);
            }
            if (AttackFrameStartMethodNames.Contains(e.functionName))
            {
                if (e.intParameter != 0)
                    allAttackStartFrames.Add(e.time);
            }
            if (EffectsAttackFrameStartMethodNames.Contains(e.functionName))
            {
                allAttackStartFrames.Add(e.time);
            }
            if (e.functionName == "turn_on_flag")
            {
                cancelflagFrame = e.time;
            }
            hasTargetEventName |= ((!string.IsNullOrEmpty(targetEventName)&& e.functionName == targetEventName) || string.IsNullOrEmpty(targetEventName));
        }

        if (!hasTargetEventName)
        {
            return false;
        }

        if (allAttackStartFrames.Count == 0)
        {
            Debug.Log(_clip.name + "貌似缺乏有效攻击帧控制类函数，需检查");
            return false;
        }

        if (Mathf.Approximately(cancelflagFrame, 0))
        {
            Debug.Log(_clip.name + "貌似没有取消flag,应做单独分析");
            return false;
        }
        earlieststartframe = allAttackStartFrames.Min();
        latestendframe = allAttackStartFrames.Max();

        float attackendtocancelstart = cancelflagFrame - latestendframe;
        bool startfilteroutcome = (earlieststartframe > start_min) && (earlieststartframe <= start_max);
        bool endfiltercoutcome = (attackendtocancelstart > end_min) && (attackendtocancelstart <= end_max);
        return startfilteroutcome && endfiltercoutcome;
    }
}
#endif