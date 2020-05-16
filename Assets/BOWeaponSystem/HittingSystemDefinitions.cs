using UnityEngine;

namespace HittingDetection
{
    public class V_Damage
    {
        public FightAttriCalReference attacker;
        public FightAttriCalReference victim;
        public BO_Marker_Manager from_weapon;
        public Marker from_weapon_marker;
        public Vector3 damageHappenPoint;
        public Quaternion CutRotation;

        public V_Damage() { }
        public V_Damage(BO_Marker_Manager weapon, Marker weapon_marker, FightAttriCalReference _victim, FightAttriCalReference _attacker, Vector3 _damageHappenPoint, Quaternion _CutRotation)
        {
            from_weapon = weapon;
            from_weapon_marker = weapon_marker;
            attacker = _attacker;
            victim = _victim;
            damageHappenPoint = _damageHappenPoint;
            CutRotation = _CutRotation;
        }

        public V_Damage Clone()
        {
            return (V_Damage)MemberwiseClone();
        }
    }

    // 枚举所对应的数字与SetThisWeaponDamageTypeByNum参数值没必要存在对应关系
    public enum DamageType
    {
        none = 0,
        slight_damage_forward = -1,//不会对敌人动作产生任何影响
        light_damage_forward = 1,//dizzy时间很短的攻击
        heavy_damage_forward = 2,//dizzy时间较长的攻击
        supper_damage_forward = 3,//能够打飞敌人的攻击 
        draw = 5,
        explosion = 6,
        push_to_mid = 7,
        high = 8,
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