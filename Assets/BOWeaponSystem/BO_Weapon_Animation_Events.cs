using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HittingDetection;

public class BO_Weapon_Animation_Events : MonoBehaviour {
    public List<Transform> _Used_Targets = new List<Transform>();
    private List<BO_Marker_Manager> using_MarkerManager_List = new List<BO_Marker_Manager>();
    private List<BO_Marker_Manager> AllMarkerManager_List = new List<BO_Marker_Manager>();
    private BO_Marker_Manager right_hand, left_hand, right_foot, left_foot, right_hand_weapon, left_hand_weapon, head, tail;
    private damageType damageType;

    // 当前这个版本因为把“身体固化武器”的检测对象给联合化了。。其实关于_Used_Targets清理，
    // 在以下的ClearTargets()DisableMarkers ()EnableMarkers()
    // 三个函数中都存在重复执行，都把本模块内的_Used_Targets给多次执行clear操作了。
    // 因为武器在作为飞行道具，伤害性特效的情况下，是自己保有一个单独的_Used_Targets列表，也是靠这三个函数来连带着对其进行清空
    // 所以找不太到一种能更好统合固化武器，特效武器这方面的写法，所以干脆就保持着这个让_Used_Targets重复被clear的状态。
    public void ClearTargets()
	{
        foreach (BO_Marker_Manager MarkerManager in using_MarkerManager_List) 
        {
            MarkerManager.ClearTargets();
        }
    }
	public void DisableMarkers()
	{
        foreach (BO_Marker_Manager MarkerManager in AllMarkerManager_List)
        {
            if (MarkerManager.enabled == true)
                MarkerManager.DisableMarkers();
        }
	}
	public void EnableMarkers()
	{
        foreach (BO_Marker_Manager MarkerManager in using_MarkerManager_List)
        {
            if (MarkerManager.enabled == true)
                MarkerManager.EnableMarkers();
        }
	}

    public void SetDamageType(AnimationEvent e)
    {
        if (using_MarkerManager_List.Count == 0)
            return;

        switch (e.intParameter)
        {
			case 0:
                damageType = damageType.slight_damage;
				break;
            case 1 :
                damageType = damageType.light_damage;
                break;
            case 2:
                damageType = damageType.heavy_damage;
                break;
			case 3:
                damageType = damageType.supper_damage;
				break;
            default:
                damageType = damageType.light_damage;
                break;
        }

        foreach (BO_Marker_Manager MarkerManager in using_MarkerManager_List)
            MarkerManager.SetDamageType(damageType);
    }

    public void assignWeaponsFromDataCenter(
        BO_Marker_Manager right_hand,
        BO_Marker_Manager left_hand,
        BO_Marker_Manager right_foot,
        BO_Marker_Manager left_foot,
        BO_Marker_Manager right_hand_weapon, 
        BO_Marker_Manager left_hand_weapon,
        BO_Marker_Manager head,
        BO_Marker_Manager tail)
    {
        this.right_hand = right_hand;
        this.left_hand = left_hand;
        this.right_foot = right_foot;
        this.left_foot = left_foot;
        this.right_hand_weapon = right_hand_weapon;
        this.left_hand_weapon = left_hand_weapon;
        this.head = head;
        this.tail = tail;

        if (right_hand != null)
            AllMarkerManager_List.Add(right_hand);
        if (left_hand != null)
            AllMarkerManager_List.Add(left_hand);
        if (right_foot != null)
            AllMarkerManager_List.Add(right_foot);
        if (left_foot != null)
            AllMarkerManager_List.Add(left_foot);
        if (right_hand_weapon != null)
            AllMarkerManager_List.Add(right_hand_weapon);
        if (left_hand_weapon != null)
            AllMarkerManager_List.Add(left_hand_weapon);
        if (head != null)
            AllMarkerManager_List.Add(head);
        if (tail != null)
            AllMarkerManager_List.Add(tail);

        foreach (BO_Marker_Manager _bo in AllMarkerManager_List)
        {
            _bo.setDectionTargetsUnion(this._Used_Targets);
        }
    }
    
    public void addMarkerManagerToUsingList(BO_Marker_Manager bO_Marker_Manager)
    {
        this.using_MarkerManager_List.Add(bO_Marker_Manager);
    }

    public void clearMarkerManagers()
    {
        foreach(BO_Marker_Manager bom in this.using_MarkerManager_List)
        {
            bom.DisableMarkers();
            //if (bom._WeaponMode == WeaponMode.EnergyFromBodyWeapon)
            //{
            //    if (bom.gameObject.activeSelf)
            //        bom.StartCoroutine(bom.disableAfterTime(0.6f));
            //}
        }
        this.using_MarkerManager_List.Clear();
        turn_off_Left_energy_blade();
        turn_off_Right_energy_blade();
    }

    public void SetAllBodyMarkerManagersIn()
    {
        foreach (BO_Marker_Manager _bm in AllMarkerManager_List)
        {
            if (!using_MarkerManager_List.Contains(_bm))
                using_MarkerManager_List.Add(_bm);
        }

        foreach (BO_Marker_Manager manager in using_MarkerManager_List)
            manager.SetDamageType(damageType.light_damage);

        EnableMarkers();
    }

    //注意看0被空出来是和添加 删除有效武器列表中的0参数有关
    private void SetThisWeaponDamageTypeByNum(int heavynum,BO_Marker_Manager theweapon)
    {
        switch (heavynum)
        {
            case -1:
                damageType = damageType.slight_damage;
                break;
            case 1:
                damageType = damageType.light_damage;
                break;
            case 2:
                damageType = damageType.heavy_damage;
                break;
            case 3:
                damageType = damageType.supper_damage;
                break;
            default:
                damageType = damageType.light_damage;
                break;
        }
        theweapon.SetDamageType(damageType);
    }

	public void  SetRightHandMarkerManager (int in_or_out = 1)
	{
        if (right_hand != null)
        {
            if (using_MarkerManager_List.Contains(right_hand))
            {
                if (in_or_out >= 1 || in_or_out == -1)
                {
                    SetThisWeaponDamageTypeByNum(in_or_out, right_hand);
                    EnableMarkers();
                }
                else if (in_or_out == 0)
                {
                    using_MarkerManager_List.Remove(right_hand);
                }
            }
            else{
                if (in_or_out >= 1 || in_or_out == -1)
                {
                    SetThisWeaponDamageTypeByNum(in_or_out, right_hand);
                    using_MarkerManager_List.Add(right_hand);
                    EnableMarkers();
                }
                else if (in_or_out == 0)
                {
                }
            }
        }
    }

    public void SetLeftHandMarkerManager(int in_or_out = 1)
    {
        if (left_hand != null)
        {
            if (using_MarkerManager_List.Contains(left_hand))
            {
                if (in_or_out >= 1 || in_or_out == -1)
                {
                    SetThisWeaponDamageTypeByNum(in_or_out, left_hand);
                    EnableMarkers();
                }
                else if (in_or_out == 0)
                {
                    using_MarkerManager_List.Remove(left_hand);
                }
            }
            else
            {
                if (in_or_out >= 1 || in_or_out == -1)
                {
                    SetThisWeaponDamageTypeByNum(in_or_out, left_hand);
                    using_MarkerManager_List.Add(left_hand);
                    EnableMarkers();
                }
                else if (in_or_out == 0)
                {
                }
            }
        }
    }

    public void SetLeftFootMarkerManager(int in_or_out = 1)
    {
        if (left_foot != null)
        {
            if (using_MarkerManager_List.Contains(left_foot))
            {
                if (in_or_out >= 1 || in_or_out == -1)
                {
                    SetThisWeaponDamageTypeByNum(in_or_out, left_foot);
                    EnableMarkers();
                }
                else if (in_or_out == 0)
                {
                    using_MarkerManager_List.Remove(left_foot);
                }
            }
            else
            {
                if (in_or_out >= 1 || in_or_out == -1)
                {
                    SetThisWeaponDamageTypeByNum(in_or_out, left_foot);
                    using_MarkerManager_List.Add(left_foot);
                    EnableMarkers();
                }
                else if (in_or_out == 0)
                {
                }
            }
        }
    }

    public void SetRightFootMarkerManager(int in_or_out = 1)
    {
        if (right_foot != null)
        {
            if (using_MarkerManager_List.Contains(right_foot))
            {
                if (in_or_out >= 1 || in_or_out == -1)
                {
                    SetThisWeaponDamageTypeByNum(in_or_out, right_foot);
                    EnableMarkers();
                }
                else if (in_or_out == 0)
                {
                    using_MarkerManager_List.Remove(right_foot);
                }
            }
            else
            {
                if (in_or_out >= 1 || in_or_out == -1)
                {
                    SetThisWeaponDamageTypeByNum(in_or_out, right_foot);
                    using_MarkerManager_List.Add(right_foot);
                    EnableMarkers();
                }
                else if (in_or_out == 0)
                {
                }
            }
        }
    }

    public void SetRightHandWeaponMarkerManager(int in_or_out = 1)
    {
        if (right_hand_weapon != null)
        {
            if (using_MarkerManager_List.Contains(right_hand_weapon))
            {
                if (in_or_out >= 1 || in_or_out == -1)
                {
                    right_hand_weapon.gameObject.SetActive(true);
                    SetThisWeaponDamageTypeByNum(in_or_out, right_hand_weapon);
                    EnableMarkers();
                }
                else if (in_or_out == 0)
                {
                    right_hand_weapon.gameObject.SetActive(false);
                    using_MarkerManager_List.Remove(right_hand_weapon);
                }
            }
            else
            {
                if (in_or_out >= 1 || in_or_out == -1)
                {
                    right_hand_weapon.gameObject.SetActive(true);
                    SetThisWeaponDamageTypeByNum(in_or_out, right_hand_weapon);
                    using_MarkerManager_List.Add(right_hand_weapon);
                    EnableMarkers();
                }
                else if (in_or_out == 0)
                {
                    right_hand_weapon.gameObject.SetActive(false);
                }
            }
        }
    }

    public void SetLeftHandWeaponMarkerManager(int in_or_out = 1)
    {
        if (left_hand_weapon != null)
        {
            if (using_MarkerManager_List.Contains(left_hand_weapon))
            {
                if (in_or_out >= 1 || in_or_out == -1)
                {
                    SetThisWeaponDamageTypeByNum(in_or_out, left_hand_weapon);
                    left_hand_weapon.gameObject.SetActive(true);
                    EnableMarkers();
                }
                else if (in_or_out == 0)
                {
                    left_hand_weapon.gameObject.SetActive(false);
                    using_MarkerManager_List.Remove(left_hand_weapon);
                }
            }
            else
            {
                if (in_or_out >= 1 || in_or_out == -1)
                {
                    left_hand_weapon.gameObject.SetActive(true);
                    SetThisWeaponDamageTypeByNum(in_or_out, left_hand_weapon);
                    using_MarkerManager_List.Add(left_hand_weapon);
                    EnableMarkers();
                }
                else if (in_or_out == 0)
                {
                    left_hand_weapon.gameObject.SetActive(false);
                }
            }
        }
    }

    public void SetHeadMarkerManager(int in_or_out = 1)
    {
        if (head != null)
        {
            if (using_MarkerManager_List.Contains(head))
            {
                if (in_or_out >= 1 || in_or_out == -1)
                {
                    SetThisWeaponDamageTypeByNum(in_or_out, head);
                    EnableMarkers();
                }
                else if (in_or_out == 0)
                {
                    using_MarkerManager_List.Remove(head);
                }
            }
            else
            {
                if (in_or_out >= 1 || in_or_out == -1)
                {
                    SetThisWeaponDamageTypeByNum(in_or_out, head);
                    using_MarkerManager_List.Add(head);
                    EnableMarkers();
                }
                else if (in_or_out == 0)
                {
                }
            }
        }
    }

    public void SetTailMarkerManager(int in_or_out = 1)
    {
        if (tail != null)
        {
            if (using_MarkerManager_List.Contains(tail))
            {
                if (in_or_out >= 1 || in_or_out == -1)
                {
                    SetThisWeaponDamageTypeByNum(in_or_out, tail);
                    EnableMarkers();
                }
                else if (in_or_out == 0)
                {
                    using_MarkerManager_List.Remove(tail);
                }
            }
            else
            {
                if (in_or_out >= 1 || in_or_out == -1)
                {
                    SetThisWeaponDamageTypeByNum(in_or_out, tail);
                    using_MarkerManager_List.Add(tail);
                    EnableMarkers();
                }
                else if (in_or_out == 0)
                {
                }
            }
        }
    }

    public void turn_on_Right_energy_blade()
    {
        if (right_hand_weapon)
            turnRightEnergyBlade(true);
    }
    public void turn_off_Right_energy_blade()
    {
        if (right_hand_weapon)
            turnRightEnergyBlade(false);
    }
    public void turnRightEnergyBlade(bool _on)
    {
        if (right_hand_weapon)
            right_hand_weapon.gameObject.SetActive(_on);
    }

    public void turn_on_Left_energy_blade()
    {
        if (left_hand_weapon)
            turnLeftEnergyBlade(true);
    }
    public void turn_off_Left_energy_blade()
    {
        if (left_hand_weapon)
            turnLeftEnergyBlade(false);
    }
    public void turnLeftEnergyBlade(bool _on)
    {
        if (left_hand_weapon)
            left_hand_weapon.gameObject.SetActive(_on);
    }
}
