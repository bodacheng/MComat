using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HittingDetection
{
    // Define what kind of thing will happen to characters depend on what kind of attack and shield crossed
    public class Attack_And_Shield_Specification
    {

        private static Attack_And_Shield_Specification instance;

        public static Attack_And_Shield_Specification Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Attack_And_Shield_Specification();
                }
                return instance;
            }
        }

        // 攻撃方と防衛方にはそれぞれどいうダーメージを与えるのか
        public attack_on_shield_result Attack_On_Shield_Cal(damageType weapon_damage_type, damageType shield_type)
        {
            if ((weapon_damage_type == damageType.light_damage || weapon_damage_type == damageType.slight_damage) &&
                (shield_type == damageType.normal_shield || shield_type == damageType.hard_shield))
            {
                return new attack_on_shield_result(damageType.stagger, damageType.light_block);
            }
            if (weapon_damage_type == damageType.heavy_damage && shield_type == damageType.normal_shield)
            {
                return new attack_on_shield_result(damageType.stagger, damageType.heavy_block);
            }
            if (weapon_damage_type == damageType.supper_damage && shield_type == damageType.hard_shield)
            {
                return new attack_on_shield_result(damageType.stagger, damageType.heavy_block);
            }
            if (weapon_damage_type == damageType.supper_damage && shield_type == damageType.normal_shield)
            {
                return new attack_on_shield_result(damageType.stagger, damageType.heavy_block);
            }
            return new attack_on_shield_result(damageType.stagger, damageType.none);
        }

        //public skillRangeReference SkillRange(behaviorEnterRange r)
        //{
        //    switch(r)
        //    {
        //        case behaviorEnterRange.faraway:
        //            return new skillRangeReference(18f, 9999f);
        //        case behaviorEnterRange.far:
        //            return new skillRangeReference(9f, 18f);
        //        case behaviorEnterRange.middle:
        //            return new skillRangeReference(5f, 9f);
        //        case behaviorEnterRange.near:
        //            return new skillRangeReference(3f, 5f);
        //        case behaviorEnterRange.close:
        //            return new skillRangeReference(2f, 3f);
        //        case behaviorEnterRange.too_close:
        //            return new skillRangeReference(0f, 2f);
        //        default:
        //            return new skillRangeReference(-1f, 9999f);
        //    }
        //}
    }

    //public class skillRangeReference
    //{
    //    public float near, far;
    //	public float near2j, far2j;
    //    public skillRangeReference(float near, float far)
    //    {
    //        this.near = near;
    //        this.far = far;
    //		near2j = Mathf.Pow (this.near, 2);
    //		far2j = Mathf.Pow (this.far,2);
    //    }
    //}

    public class attack_on_shield_result
    {
        public damageType on_shield_holder, on_weapon_holder;
        public attack_on_shield_result(damageType on_weapon_holder, damageType on_shield_holder)
        {
            this.on_shield_holder = on_shield_holder;
            this.on_weapon_holder = on_weapon_holder;
        }
    }

    public class v_Damage
    {
        public damageType damage_type;
        public Vector3 force_direction;
        public Vector3 damageHappenPoint;
        public BO_Health toWho;
        public BO_Marker_Manager fromWeapon;
        public float AT;

        public specialApply specialApply = specialApply.none;
        //public Vector3 testHurtGetFixPos;//这个应该没什么用了。可能在将来删掉

        public v_Damage() { }
        public v_Damage(damageType damage_type, Vector3 force_direction, Vector3 damageHappenPoint, BO_Health toWho, BO_Marker_Manager fromWeapon)
        {
            this.damage_type = damage_type;
            this.force_direction = force_direction;
            this.damageHappenPoint = damageHappenPoint;
            this.toWho = toWho;
            this.fromWeapon = fromWeapon;
            if (this.fromWeapon != null)
                this.AT = this.fromWeapon.GetWeaponOwnerHealth().AT;
            else
                this.AT = 0;
            specialApply = specialApply.none;   
        }
        public v_Damage(damageType damage_type, Vector3 force_direction, Vector3 damageHappenPoint, BO_Health toWho, BO_Marker_Manager fromWeapon,specialApply specialApply)
        {
            this.damage_type = damage_type;
            this.force_direction = force_direction;
            this.damageHappenPoint = damageHappenPoint;
            this.toWho = toWho;
            this.fromWeapon = fromWeapon;
            if (this.fromWeapon != null)
                this.AT = this.fromWeapon.GetWeaponOwnerHealth().AT;
            else
                this.AT = 0;
            this.specialApply = specialApply;   
        }
    }

    public class e_Damage
    {
        private BO_Health Attacker_Health;
        private BO_Health Damaged_Health;
        public Position_set Position_set;

        public e_Damage() { }
        public e_Damage(BO_Health Attacker_Health, Position_set Position_set)
        {
            this.Attacker_Health = Attacker_Health;
            this.Position_set = Position_set;
        }

        public BO_Health getAttackerHealthBody()
        {
            return this.Attacker_Health;
        }

        public void setAttackerHealthBody(BO_Health BO_Health)
        {
            this.Attacker_Health = BO_Health;
        }

        public void setDamagedHealthBody(BO_Health b)
        {
            this.Damaged_Health = b;
        }

        public BO_Health getDamagedHealthBody()
        {
            return this.Damaged_Health;
        }
    }

    public enum damageType : int
    {
        none = 0,
        slight_damage = 8,//不会对敌人动作产生任何影响
        light_damage = 1,//dizzy时间很短的攻击
        heavy_damage = 2,//dizzy时间较长的攻击
        supper_damage = 6,//能够打飞敌人的攻击 
        knockOff_damage = 7,
        stagger = 4,//攻击方打中盾牌造成的震动
        light_block = 12,//防御方在攻击下所受的轻微震动
        heavy_block = 5,//防御者受到较强攻击时所受到的较大震动
        normal_shield = 10,//普通防御罩
        hard_shield = 11,//强防御罩
        deathknockoff = 13
    }
    public enum specialApply : int
    {
        none = 0,
        gravitylost = 1,
    }
}