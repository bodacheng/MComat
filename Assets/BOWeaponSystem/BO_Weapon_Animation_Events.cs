using UnityEngine;
using System.Collections.Generic;
using HittingDetection;
using UniRx;

public class BO_Weapon_Animation_Events : MonoBehaviour {

    private TeamConfig _TeamConfig;
    private List<Transform> _Used_Targets = new List<Transform>();
    private IDictionary<Transform,Decompositioner> bodyPartsWeaponRegisterDic;
    private List<Transform> bodyweaponParts;
    private Transform right_hand, left_hand, right_foot, left_foot, head, tail;    
    private Transform geometryCenter;
    private FightAttriCalReference myownheath;
    private DamageType damageType;
    private Decompositioner target_hitbox;
    static DecompositionerPool default_hitboxPool;

    void Awake()
    {
        if (default_hitboxPool == null)
        {
            GameObject hurtObject = Resources.Load("HurtObjects/defaultmagic/d_hitbox") as GameObject;
            default_hitboxPool = new DecompositionerPool(hurtObject);
            default_hitboxPool.PreloadAsync(20, 1).Subscribe(_ => Debug.Log("已经为对象池:d_hitbox预留物件"));
        }
    }
    
    public void assignWeaponsFromDataCenter(FightAttriCalReference Ownheath,Transform geometryCenter, Transform right_hand,Transform left_hand,Transform right_foot,Transform left_foot,Transform head,Transform tail)
    {
        this.myownheath = Ownheath;
        this.geometryCenter = geometryCenter;
        this.right_hand = right_hand;
        this.left_hand = left_hand;
        this.right_foot = right_foot;
        this.left_foot = left_foot;
        this.head = head;
        this.tail = tail;
        
        bodyPartsWeaponRegisterDic = new Dictionary<Transform,Decompositioner>();
        bodyweaponParts = new List<Transform>();

        if (this.right_hand != null)
            bodyweaponParts.Add(this.right_hand);
        if (this.left_hand != null)
            bodyweaponParts.Add(this.left_hand);
        if (this.left_foot != null)
            bodyweaponParts.Add(this.left_foot);
        if (this.right_foot != null)
            bodyweaponParts.Add(this.right_foot);
        if (this.head != null)
            bodyweaponParts.Add(this.head);
        if (this.tail != null)
            bodyweaponParts.Add(this.tail);
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
                keyValuePair.Value._HitBox.ClearTargets();
        }
    }
    
    public void clearMarkerManagers()
    {
        foreach (Transform T in bodyweaponParts) 
        {
            RemoveBodyPartWeapon(T);
        }
    }
    
	public void EnableMarkers()
	{
        foreach (KeyValuePair<Transform,Decompositioner> keyValuePair in bodyPartsWeaponRegisterDic) 
        {
            if (keyValuePair.Value != null)
                keyValuePair.Value._HitBox.EnableMarkers();
        }
	}

    public void SetDamageType(AnimationEvent e)
    {
        switch (e.intParameter)
        {
			case 0:
                damageType = DamageType.slight_damage;
				break;
            case 1 :
                damageType = DamageType.light_damage;
                break;
            case 2:
                damageType = DamageType.heavy_damage;
                break;
			case 3:
                damageType = DamageType.supper_damage;
				break;
            default:
                damageType = DamageType.light_damage;
                break;
        }
        foreach (KeyValuePair<Transform,Decompositioner> keyValuePair in bodyPartsWeaponRegisterDic) 
        {
            if (keyValuePair.Value != null)
                keyValuePair.Value._HitBox.SetDamageType(damageType);
        }
    }
    
    public void assignTeamFlag(TeamConfig teamConfig)
    {
        this._TeamConfig = teamConfig;
    }
    
    public void SetAllBodyMarkerManagersIn()
    {
        foreach (Transform T in bodyweaponParts)
        {
            RegisterBodyPartWeapon(T,1);
        }
    }

    //注意看0被空出来是和添加 删除有效武器列表中的0参数有关
    private void SetThisWeaponDamageTypeByNum(int heavynum, BO_Marker_Manager theweapon)
    {
        switch (heavynum)
        {
            case -1:
                damageType = DamageType.slight_damage;
                break;
            case 1:
                damageType = DamageType.light_damage;
                break;
            case 2:
                damageType = DamageType.heavy_damage;
                break;
            case 3:
                damageType = DamageType.supper_damage;
                break;
            default:
                damageType = DamageType.light_damage;
                break;
        }
        theweapon.SetDamageType(damageType);
    }

    private void RegisterBodyPartWeapon(Transform t)
    {
        if (t != null && !bodyPartsWeaponRegisterDic.ContainsKey(t))
            bodyPartsWeaponRegisterDic.Add(t, null);
            
        if (bodyPartsWeaponRegisterDic[t] == null)
        {
            target_hitbox = default_hitboxPool.Rent();
            bodyPartsWeaponRegisterDic[t] = target_hitbox;
        }
        bodyPartsWeaponRegisterDic[t].transform.SetParent(t);
        bodyPartsWeaponRegisterDic[t].transform.localPosition = Vector3.zero;        
        bodyPartsWeaponRegisterDic[t]._HitBox.SetTeamConfig(_TeamConfig);
        bodyPartsWeaponRegisterDic[t]._HitBox.SetHolderCenter(this.geometryCenter);
        bodyPartsWeaponRegisterDic[t]._HitBox.SetOwnerFightAttriCalReference(myownheath);
        bodyPartsWeaponRegisterDic[t]._HitBox.SetDectionTargetsUnion(this._Used_Targets);
        bodyPartsWeaponRegisterDic[t]._HitBox.EnableMarkers();
    }
    
    private void RemoveBodyPartWeapon(Transform t)
    {
        if (!bodyPartsWeaponRegisterDic.ContainsKey(t))
            return;
        if (bodyPartsWeaponRegisterDic[t] != null)
        {
            bodyPartsWeaponRegisterDic[t]._HitBox.SetOwnerFightAttriCalReference(null);
            bodyPartsWeaponRegisterDic[t]._HitBox.SetDectionTargetsUnion(null);
            default_hitboxPool.Return(bodyPartsWeaponRegisterDic[t]); //diablemarkers在对象池物件的onbeforereturn里。原因是方便特效攻击在作用周期结束时自主disablemarker
            bodyPartsWeaponRegisterDic[t] = null;
        }
    }
    
    private void RegisterBodyPartWeapon(Transform t,int hit_type) //hit_type == 0: clear ;hit_type != 0 : in
    {
        if (hit_type != 0)
        {
            RegisterBodyPartWeapon(t);
            SetThisWeaponDamageTypeByNum(hit_type,bodyPartsWeaponRegisterDic[t]._HitBox);
        }else{
            RemoveBodyPartWeapon(t);
        }
    }

	public void SetRightHandMarkerManager (int in_or_out = 1)
	{
        RegisterBodyPartWeapon(right_hand,in_or_out);
    }

    public void SetLeftHandMarkerManager(int in_or_out = 1)
    {
        RegisterBodyPartWeapon(left_hand,in_or_out);
    }

    public void SetLeftFootMarkerManager(int in_or_out = 1)
    {
        RegisterBodyPartWeapon(left_foot,in_or_out);
    }

    public void SetRightFootMarkerManager(int in_or_out = 1)
    {
        RegisterBodyPartWeapon(right_foot,in_or_out);
    }

    public void SetHeadMarkerManager(int in_or_out = 1)
    {
        RegisterBodyPartWeapon(head,in_or_out);
    }

    public void SetTailMarkerManager(int in_or_out = 1)
    {
        RegisterBodyPartWeapon(tail,in_or_out);
    }
}
