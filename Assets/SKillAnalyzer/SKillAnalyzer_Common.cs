using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class SKillAnalyzer : MonoBehaviour
{
    class EventNameAndAtFrame 
    {
        public string name;
        public float startframe;
    }
    
    public static List<string> AttackFrameStartMethodNames = new List<string>() {
        "SetRightHandMarkerManager","SetLeftHandMarkerManager",
        "SetRightFootMarkerManager","SetLeftFootMarkerManager",
        "SetRightHandWeaponMarkerManager","SetLeftHandWeaponMarkerManager",
        "SetHeadMarkerManager","SetTailMarkerManager"
    };
    
    readonly static List<string> AttackClearMethodNames = new List<string>() {
        "SetRightHandMarkerManager","SetLeftHandMarkerManager",
        "SetRightFootMarkerManager","SetLeftFootMarkerManager",
        "SetRightHandWeaponMarkerManager","SetLeftHandWeaponMarkerManager",
        "SetHeadMarkerManager","SetTailMarkerManager"
    };
    readonly static List<string> EffectsAttackFrameStartMethodNames = new List<string>()
    {
        "MagicForward","Bullet_shoot_from_body_part","BlastAttack","ReleasePreparedMagic","ReleasePreparedMagicToAir"
    };
    
    public static IDictionary<string, AnimationClip> AllSkillAnims(string type)
    {
        List<Object> G_Attack_States = Resources.LoadAll("Animations/" + type + "/G_Attack_State", typeof(AnimationClip)).ToList();
        List<Object> G_Attack_State_Stays = Resources.LoadAll("Animations/" + type + "/G_Attack_State_Stay", typeof(AnimationClip)).ToList();
        List<Object> GMStatess = Resources.LoadAll("Animations/" + type + "/GMStates", typeof(AnimationClip)).ToList();

        IDictionary<string, AnimationClip> AnimationClips = new Dictionary<string, AnimationClip>();
        
        foreach (Object _object in G_Attack_States)
        {
            AnimationClips.Add(_object.name, _object as AnimationClip);
        }
        foreach (Object _object in G_Attack_State_Stays)
        {
            AnimationClips.Add(_object.name, _object as AnimationClip);
        }
        foreach (Object _object in GMStatess)
        {
            AnimationClips.Add(_object.name, _object as AnimationClip);
        }
        return AnimationClips;
    }
}
