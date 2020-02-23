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
        public WeaponMode _weaponMode;
        public SpecialApply specialApply = SpecialApply.none;
        public Vector3 damageHappenPoint;
        public Vector3 AttackerT_foward;
        public Vector3 AttackerT_pos;
        public Vector3 WeaponMarkerPos;
        public FightAttriCalReference attacker;
        public FightAttriCalReference victim;
        public Quaternion CutRotation;
        
        public bool effectSpreadOnBody;
        public string effectPath;
        
        public V_Damage() { }

        public V_Damage(BO_Marker_Manager weapon, Marker weapon_marker)
        {

        }

        public V_Damage(float _AT,
                        FightAttriCalReference _victim, FightAttriCalReference _attacker,
                        DamageType damage_type, WeaponPosAdjustMode _WeaponPosAdjustMode, WeaponMode weaponMode,SpecialApply specialApply,
                        Vector3 damageHappenPoint, Quaternion _CutRotation,
                        Vector3 AttackerT_foward, Vector3 AttackerT_pos, Vector3 WeaponMarker,
                        string effectPath,bool _effectSpreadOnBody)
        {
            victim = _victim;
            attacker = _attacker;
            this.damage_type = damage_type;
            this._WeaponPosAdjustMode = _WeaponPosAdjustMode;
            this._weaponMode = weaponMode;
            this.damageHappenPoint = damageHappenPoint;
            CutRotation = _CutRotation;
            this.AttackerT_foward = AttackerT_foward;
            this.AttackerT_pos = AttackerT_pos;
            this.WeaponMarkerPos = WeaponMarker;
            this.effectPath = effectPath;
            AT = _AT;
            this.specialApply = specialApply;
            this.effectSpreadOnBody = _effectSpreadOnBody;
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
        DeathKnockoff = 13
    }

    public enum WeaponPosAdjustMode
    {
        pushToMidForward = 1,
        draw = 2,
        explosion = 3
    }

    public enum WeaponMode
    {
        FlyerWeapon = 2,
        EnergyFromBodyWeapon = 3
    }

    public enum SpecificTarget
    {
        both = 0,
        flesh = 1,
        energy = 2
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