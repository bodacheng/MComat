using UnityEngine;

namespace HittingDetection
{
    // Define what kind of thing will happen to characters depend on what kind of attack and shield crossed
    public class Attack_And_Shield_Specification
    {
        static Attack_And_Shield_Specification instance;
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
        public Attack_on_shield_result Attack_On_Shield_Cal(DamageType weapon_damage_type, DamageType shield_type)
        {
            if ((weapon_damage_type == DamageType.light_damage || weapon_damage_type == DamageType.slight_damage) &&
                (shield_type == DamageType.normal_shield || shield_type == DamageType.hard_shield))
            {
                return new Attack_on_shield_result(DamageType.stagger, DamageType.light_block);
            }
            if (weapon_damage_type == DamageType.heavy_damage && shield_type == DamageType.normal_shield)
            {
                return new Attack_on_shield_result(DamageType.stagger, DamageType.heavy_block);
            }
            if (weapon_damage_type == DamageType.supper_damage && shield_type == DamageType.hard_shield)
            {
                return new Attack_on_shield_result(DamageType.stagger, DamageType.heavy_block);
            }
            if (weapon_damage_type == DamageType.supper_damage && shield_type == DamageType.normal_shield)
            {
                return new Attack_on_shield_result(DamageType.stagger, DamageType.heavy_block);
            }
            return new Attack_on_shield_result(DamageType.stagger, DamageType.none);
        }
    }

    public class Attack_on_shield_result
    {
        public DamageType on_shield_holder, on_weapon_holder;
        public Attack_on_shield_result(DamageType on_weapon_holder, DamageType on_shield_holder)
        {
            this.on_shield_holder = on_shield_holder;
            this.on_weapon_holder = on_weapon_holder;
        }
    }

    public class V_Damage
    {
        public float AT;
        public DamageType damage_type;
        public WeaponPosAdjustMode _WeaponPosAdjustMode;
        public Vector3 damageHappenPoint;
        public Vector3 AttackerT_foward;
        public Vector3 AttackerT_pos;
        public BO_Marker_Manager fromWeapon;
        public SpecialApply specialApply = SpecialApply.none;
        
        public V_Damage() { }

        public string effectPath;
        public FightAttriCalReference victim;
        public Vector3 weaponsCutDirection;
        public V_Damage(FightAttriCalReference _Raw_Target_Instance, Vector3 _StartPoint, Vector3 _Direction)
        {
            victim = _Raw_Target_Instance;
            damageHappenPoint = _StartPoint;
            weaponsCutDirection = _Direction;
        }
        
        public V_Damage(DamageType damage_type, WeaponPosAdjustMode _WeaponPosAdjustMode, Vector3 damageHappenPoint, Vector3 AttackerT_foward, Vector3 AttackerT_pos, BO_Marker_Manager fromWeapon)
        {
            this.damage_type = damage_type;
            this._WeaponPosAdjustMode = _WeaponPosAdjustMode;
            this.damageHappenPoint = damageHappenPoint;
            this.fromWeapon = fromWeapon;
            this.AttackerT_foward = AttackerT_foward;
            this.AttackerT_pos = AttackerT_pos;
            AT = this.fromWeapon != null ? this.fromWeapon.GetFixedAT(): 0;
            specialApply = SpecialApply.none;
        }
        public V_Damage(DamageType damage_type, WeaponPosAdjustMode _WeaponPosAdjustMode, Vector3 damageHappenPoint, Vector3 AttackerT_foward, Vector3 AttackerT_pos, BO_Marker_Manager fromWeapon,string effectPath,SpecialApply specialApply)
        {
            this.damage_type = damage_type;
            this._WeaponPosAdjustMode = _WeaponPosAdjustMode;
            this.damageHappenPoint = damageHappenPoint;
            this.fromWeapon = fromWeapon;
            this.AttackerT_foward = AttackerT_foward;
            this.AttackerT_pos = AttackerT_pos;
            this.effectPath = effectPath;
            AT = this.fromWeapon != null ? this.fromWeapon.GetFixedAT() : 0;
            this.specialApply = specialApply;   
        }
        
        public V_Damage Clone()
        {
            return (V_Damage)MemberwiseClone();
        }
    }

    public enum DamageType
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
    
    public enum SpecialApply
    {
        none = 0,
        gravitylost = 1,
    }
    
    public class E_Damage
    {
        FightAttriCalReference Attacker_Health;
        FightAttriCalReference Damaged_Health;
        public Position_set Position_set;

        public E_Damage() { }
        public E_Damage(FightAttriCalReference Attacker_Health, Position_set Position_set)
        {
            this.Attacker_Health = Attacker_Health;
            this.Position_set = Position_set;
        }

        public FightAttriCalReference GetAttackerHealthBody()
        {
            return Attacker_Health;
        }

        public void SetAttackerHealthBody(FightAttriCalReference BO_Health)
        {
            Attacker_Health = BO_Health;
        }

        public void SetDamagedHealthBody(FightAttriCalReference b)
        {
            Damaged_Health = b;
        }

        public FightAttriCalReference GetDamagedHealthBody()
        {
            return Damaged_Health;
        }
    }
}