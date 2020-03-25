using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using HittingDetection;
using UniRx;
using UnityEngine.Animations;

public class BO_Weapon_Animation_Events : MonoBehaviour
{
    public HiddenMethods hiddenMethods;

    TeamConfig _TeamConfig = TeamConfig.defaultSet;
    readonly List<Transform> _Used_Targets = new List<Transform>();
    IDictionary<Transform, Decompositioner> bodyPartsWeaponRegisterDic;
    Transform right_hand, left_hand, right_foot, left_foot, head, tail;
    Transform geometryCenter;
    FightAttriCalReference myownheath;
    static DecompositionerPool default_hitboxPool;

    void Awake()
    {
        hiddenMethods = new HiddenMethods(this);
        if (default_hitboxPool == null)
        {
            GameObject hurtObject = Resources.Load("HurtObjects/defaultmagic/d_hitbox") as GameObject;
            default_hitboxPool = new DecompositionerPool(hurtObject);
            default_hitboxPool.PreloadAsync(20, 1).Subscribe(_ => Debug.Log("已经为对象池:d_hitbox预留物件"));
        }
    }

    public class HiddenMethods
    {
        readonly BO_Weapon_Animation_Events BEs;
        public HiddenMethods(BO_Weapon_Animation_Events bO_Weapon_Animation_Events)
        {
            BEs = bO_Weapon_Animation_Events;
        }
        
        public void AssignTeamFlag(TeamConfig teamConfig)
        {
            BEs._TeamConfig = teamConfig;
        }
        public void AssignWeaponsFromDataCenter(FightAttriCalReference Ownheath,Transform geometryCenter, Transform right_hand,Transform left_hand,Transform right_foot,Transform left_foot,Transform head,Transform tail)
        {
            BEs.myownheath = Ownheath;
            BEs.geometryCenter = geometryCenter;
            BEs.right_hand = right_hand;
            BEs.left_hand = left_hand;
            BEs.right_foot = right_foot;
            BEs.left_foot = left_foot;
            BEs.head = head;
            BEs.tail = tail;
            BEs.bodyPartsWeaponRegisterDic = new Dictionary<Transform,Decompositioner>();
            if (BEs.right_hand != null)
                BEs.bodyPartsWeaponRegisterDic.Add(BEs.right_hand,null);
            if (BEs.left_hand != null)
                BEs.bodyPartsWeaponRegisterDic.Add(BEs.left_hand,null);
            if (BEs.left_foot != null)
                BEs.bodyPartsWeaponRegisterDic.Add(BEs.left_foot,null);
            if (BEs.right_foot != null)
                BEs.bodyPartsWeaponRegisterDic.Add(BEs.right_foot,null);
            if (BEs.head != null)
                BEs.bodyPartsWeaponRegisterDic.Add(BEs.head,null);
            if (BEs.tail != null)
                BEs.bodyPartsWeaponRegisterDic.Add(BEs.tail,null);
        }
        
        //注意看0被空出来是和添加 删除有效武器列表中的0参数有关
        DamageType damageType;
        Decompositioner target_hitbox;
        void SetThisWeaponDamageTypeByNum(int heavynum, BO_Marker_Manager theweapon)
        {
            switch (heavynum)
            {
                case -1:
                    damageType = DamageType.slight_damage_forward;
                    break;
                case 1:
                    damageType = DamageType.light_damage_forward;
                    break;
                case 2:
                    damageType = DamageType.heavy_damage_forward;
                    break;
                case 3:
                    damageType = DamageType.supper_damage_forward;
                    break;
                default:
                    damageType = DamageType.light_damage_forward;
                    break;
            }
            theweapon.SetDamageType(damageType);
        }

        readonly Decompositioner processingEffectObj;
        ConstraintSource myConstraintSource;
        void RegisterBodyPartWeapon(Transform t)
        {
            if (t != null && !BEs.bodyPartsWeaponRegisterDic.ContainsKey(t))
            {
                BEs.bodyPartsWeaponRegisterDic.Add(t, null);
            }
            if (BEs.bodyPartsWeaponRegisterDic[t] == null)
            {
                target_hitbox = default_hitboxPool.Rent();
                BEs.bodyPartsWeaponRegisterDic[t] = target_hitbox;
                BEs.bodyPartsWeaponRegisterDic[t]._HitBox.SetOwnerFightAttriCalReference(BEs.myownheath);
                
                
            }
            myConstraintSource.sourceTransform = t;
            myConstraintSource.weight = 1;
            BEs.bodyPartsWeaponRegisterDic[t].transform.position = t.position;
            BEs.bodyPartsWeaponRegisterDic[t].GetPositionConstraint().SetSources(new List<ConstraintSource> { myConstraintSource });
            BEs.bodyPartsWeaponRegisterDic[t].GetPositionConstraint().constraintActive = true;
            BEs.bodyPartsWeaponRegisterDic[t].GetPositionConstraint().locked = true;
            BEs.bodyPartsWeaponRegisterDic[t]._HitBox.SetTeamConfig(BEs._TeamConfig);
            BEs.bodyPartsWeaponRegisterDic[t]._HitBox.SetReferenceTransformInfo(BEs.geometryCenter);//第二个参数是因为BE本身就在wholeT上
            BEs.bodyPartsWeaponRegisterDic[t]._HitBox.SetDectionTargetsUnion(BEs._Used_Targets);
            BEs.bodyPartsWeaponRegisterDic[t]._HitBox.MarkersEnablingStarts();
            
            if (FightGlobalSetting.HitBoxLogger)
            {
                BEs.bodyPartsWeaponRegisterDic[t]._HitBox.GeneratedByStateKey = BEs.myownheath._Center._MyBehaviorRunner.GetNowState().StateKey;
                BEs.bodyPartsWeaponRegisterDic[t]._HitBox.HitBoxLifeEnding = HitBoxLifeEnding.untouched;
            }
        }
        
        public void RemoveBodyPartWeapon(Transform t)
        {
            if (!BEs.bodyPartsWeaponRegisterDic.ContainsKey(t))
            {
                return;
            }
            if (BEs.bodyPartsWeaponRegisterDic[t] != null)
            {
                target_hitbox = BEs.bodyPartsWeaponRegisterDic[t];
                BEs.bodyPartsWeaponRegisterDic[t] = null;
                target_hitbox.GetPositionConstraint().constraintActive = false;
                default_hitboxPool.Return(target_hitbox); //diablemarkers在对象池物件的onbeforereturn里。原因是方便特效攻击在作用周期结束时自主disablemarker
                target_hitbox._HitBox.SetDectionTargetsUnion(null);//为什么要先把hitbox返回对象池内才执行SetDectionTargetsUnion(null)呢，因为disablemarker在返回对象池的操作里，如果不先disablemarker的话，会导致检测过程中找不到used_targets
            }
        }
        public void RegisterBodyPartWeapon(Transform t, int hit_type) //hit_type == 0: clear ;hit_type != 0 : in
        {
            if (hit_type != 0)
            {
                RegisterBodyPartWeapon(t);
                SetThisWeaponDamageTypeByNum(hit_type, BEs.bodyPartsWeaponRegisterDic[t]._HitBox);
            }
            else
            {
                RemoveBodyPartWeapon(t);
            }
        }
    }
    
    // 当前这个版本因为把“身体固化武器”的检测对象给联合化了。。其实关于_Used_Targets清理，
    // 在以下的ClearTargets()DisableMarkers ()EnableMarkers()
    // 三个函数中都存在重复执行，都把本模块内的_Used_Targets给多次执行clear操作了。
    // 因为武器在作为飞行道具，伤害性特效的情况下，是自己保有一个单独的_Used_Targets列表，也是靠这三个函数来连带着对其进行清空
    // 所以找不太到一种能更好统合固化武器，特效武器这方面的写法，所以干脆就保持着这个让_Used_Targets重复被clear的状态。
    public void ClearTargets()
	{
        foreach (KeyValuePair<Transform,Decompositioner> keyValuePair in bodyPartsWeaponRegisterDic) 
        {
            if (keyValuePair.Value != null)
            {
                keyValuePair.Value._HitBox.ClearTargets();
            }
        }
    }

    List<Transform> bodyparts;
    public void ClearMarkerManagers()
    {
        bodyparts = bodyPartsWeaponRegisterDic.Keys.ToList();
        for (int i = 0; i < bodyparts.Count;i++)
        {
            if (bodyparts[i] !=null)
            {
                hiddenMethods.RemoveBodyPartWeapon(bodyparts[i]);
            }
        }
    }
    
	public void EnableMarkers()
	{
        foreach (KeyValuePair<Transform,Decompositioner> keyValuePair in bodyPartsWeaponRegisterDic) 
        {
            if (keyValuePair.Value != null)
            {
                keyValuePair.Value._HitBox.EnableMarkers();
            }
        }
	}
    
    public void DisableMarkers()
    {
        foreach (KeyValuePair<Transform,Decompositioner> keyValuePair in bodyPartsWeaponRegisterDic) 
        {
            if (keyValuePair.Value != null)
            {
                keyValuePair.Value._HitBox.DisableMarkers();
            }
        }
    }
    
    DamageType damageType;
    public void SetDamageType(AnimationEvent e)
    {
        switch (e.intParameter)
        {
			case 0:
                damageType = DamageType.slight_damage_forward;
				break;
            case 1 :
                damageType = DamageType.light_damage_forward;
                break;
            case 2:
                damageType = DamageType.heavy_damage_forward;
                break;
			case 3:
                damageType = DamageType.supper_damage_forward;
				break;
            default:
                damageType = DamageType.light_damage_forward;
                break;
        }
        foreach (KeyValuePair<Transform,Decompositioner> keyValuePair in bodyPartsWeaponRegisterDic) 
        {
            if (keyValuePair.Value != null)
            {
                keyValuePair.Value._HitBox.SetDamageType(damageType);
            }
        }
    }
        
    public void SetAllBodyMarkerManagersIn()
    {
        bodyparts = bodyPartsWeaponRegisterDic.Keys.ToList();
        for (int i = 0; i < bodyparts.Count;i++)
        {
            if (bodyparts[i] !=null)
            {
                hiddenMethods.RegisterBodyPartWeapon(bodyparts[i],1);
            }
        }
    }

    public void SetRightHandMarkerManager (int in_or_out = 1)
	{
        hiddenMethods.RegisterBodyPartWeapon(right_hand,in_or_out);
    }

    public void SetLeftHandMarkerManager(int in_or_out = 1)
    {
        hiddenMethods.RegisterBodyPartWeapon(left_hand,in_or_out);
    }

    public void SetLeftFootMarkerManager(int in_or_out = 1)
    {
        hiddenMethods.RegisterBodyPartWeapon(left_foot,in_or_out);
    }

    public void SetRightFootMarkerManager(int in_or_out = 1)
    {
        hiddenMethods.RegisterBodyPartWeapon(right_foot,in_or_out);
    }

    public void SetHeadMarkerManager(int in_or_out = 1)
    {
        hiddenMethods.RegisterBodyPartWeapon(head,in_or_out);
    }

    public void SetTailMarkerManager(int in_or_out = 1)
    {
        hiddenMethods.RegisterBodyPartWeapon(tail,in_or_out);
    }
}
