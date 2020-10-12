using System.Collections.Generic;
using UnityEngine;
using Skill;
using System.IO;
using System.Text;
using HittingDetection;

public partial class SKillAnalyzer : MonoBehaviour
{
    readonly List<string> NormalAttackMethodNames = new List<string>() {
        "SetRightHandMarkerManager","SetLeftHandMarkerManager",
        "SetRightFootMarkerManager","SetLeftFootMarkerManager",
        "SetRightHandWeaponMarkerManager","SetLeftHandWeaponMarkerManager",
        "SetHeadMarkerManager","SetTailMarkerManager",
        "SetAllBodyMarkerManagersIn"
    };
    
    readonly List<string> MagicMethods22 = new List<string>()
    {
        "MagicForward", "Bullet_shoot_from_body_part", "BlastAttack"
    };
    
    public void Export(string type)
    {
        SkillConfigTable.LoadAllSkillConfigFromLocalConfigFile();
        List<SkillConfig> SkillConfigs = SkillConfigTable.GetSkillConfigsOfType(type);
        IDictionary<string, AnimationClip> AnimDic = AllSkillAnims(type);
        GenerateFile(Application.persistentDataPath + "/技能总伤害预估.csv", SkillConfigs, AnimDic);
    }
    
    void GenerateFile(string filepath, List<SkillConfig> SkillConfigs, IDictionary<string, AnimationClip> AnimDic)
    {
        string[][] grid = new string[SkillConfigs.Count + 1][];
        for (int i = 0; i < grid.Length; i++)
        {
            grid[i] = new string[3];
            if (i == 0)
            {
                grid[i][0] = "RECORD_ID";
                grid[i][1] = "REAL_NAME";
                grid[i][2] = "预计最大伤害";
            }
            else
            {
                grid[i][0] = SkillConfigs[i - 1].RECORD_ID;
                grid[i][1] = SkillConfigs[i - 1].REAL_NAME;
                AnimDic.TryGetValue(SkillConfigs[i -1].REAL_NAME, out AnimationClip clip);
                grid[i][2] = ATCal(clip, SkillConfigs[i - 1].ATTACK_WEIGHT).ToString();
            }
        }
        string delimiter = ",";
        StringBuilder sb = new StringBuilder();
        for (int index = 0; index < grid.Length; index++)
            sb.AppendLine(string.Join(delimiter, grid[index]));
        Debug.Log("尝试最终保存文件" + filepath);
        StreamWriter outStream = File.CreateText(filepath);
        outStream.WriteLine(sb);
        outStream.Close();
    }
    
    public float ATCal(AnimationClip _clip, float skillATRef)
    {
        float amount = 0;
        for (int i = 0; i < _clip.events.Length; i++)
        {
            if (NormalAttackMethodNames.Contains(_clip.events[i].functionName) ||
                _clip.events[i].functionName == "ClearTargets")
            {
                amount += skillATRef;
            }
            
            if (_clip.events[i].functionName == "MagicForward")
            {
                string magicobjectname = _clip.events[i].stringParameter;
                GameObject hurtObject = Resources.Load("HurtObjects/defaultmagic/" + magicobjectname) as GameObject;
                BO_Marker_Manager bO_Marker_Manager = hurtObject.GetComponent<BO_Marker_Manager>();
                if (bO_Marker_Manager == null)
                {
                    Debug.Log("请检查这个技能动画:" + _clip.name + ",与此伤害物体：" + magicobjectname);
                }
                amount += bO_Marker_Manager.AT_weight * skillATRef;
                
                Decompositioner decompositioner = hurtObject.GetComponent<Decompositioner>();
                if ( decompositioner.Attachments.Length > 0)
                {
                    Debug.Log("技能动画："+_clip.name + " 不好机械评估");
                }
                
                // attachment /////
                //for (int z = 0; z < decompositioner.Attachments.Length; z++)
                //{
                //    GameObject attachment = Resources.Load("HurtObjects/defaultmagic/" + decompositioner.Attachments[z]) as GameObject;
                //    BO_Marker_Manager attachmentbo = attachment.GetComponent<BO_Marker_Manager>();
                //    if (attachmentbo == null)
                //    {
                //        Debug.Log("请检查这个技能动画:" + _clip.name + ",与此伤害物体：" + magicobjectname);
                //    }
                //    amount += attachmentbo.AT_weight * skillATRef;
                //}
                ////////////////////
            }
            if (_clip.events[i].functionName == "Bullet_shoot_from_body_part" || _clip.events[i].functionName == "BlastAttack")
            {
                amount += skillATRef;
            }
            
            if (_clip.events[i].functionName == "PrepareOneMagic")
            {
                float oneDamege;
                string magicobjectname = _clip.events[i].stringParameter;
                GameObject hurtObject = Resources.Load("HurtObjects/defaultmagic/" + magicobjectname) as GameObject;
                BO_Marker_Manager bO_Marker_Manager = hurtObject.GetComponent<BO_Marker_Manager>();
                oneDamege = bO_Marker_Manager.AT_weight * skillATRef;
                
                Decompositioner decompositioner = hurtObject.GetComponent<Decompositioner>();
                if ( decompositioner.Attachments.Length > 0)
                {
                    Debug.Log("技能动画："+_clip.name + " 不好机械评估");
                }
                
                // attachment /////
                //for (int z = 0; z < decompositioner.Attachments.Length; z++)
                //{
                //    GameObject attachment = Resources.Load("HurtObjects/defaultmagic/" + decompositioner.Attachments[z]) as GameObject;
                //    BO_Marker_Manager attachmentbo = attachment.GetComponent<BO_Marker_Manager>();
                //    if (attachmentbo == null)
                //    {
                //        Debug.Log("请检查这个技能动画:" + _clip.name + ",与此伤害物体：" + magicobjectname);
                //    }
                //    oneDamege += attachmentbo.AT_weight * skillATRef;
                //}
                ////////////////////
                
                for (int y = i + 1; y < _clip.events.Length; y++)
                {
                    if (_clip.events[y].functionName == "ReleasePreparedMagic" || _clip.events[y].functionName == "ReleasePreparedMagicToAir")
                    {
                        amount += oneDamege;
                    }
                    if (_clip.events[i].functionName == "PrepareOneMagic")
                    {
                        break;
                    }
                }
            }
        }
        return amount;
    }
}