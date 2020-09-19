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
        
        public static DamageType FormalIntToDamageType(int num)
        {
            DamageType damageType;
            switch (num)
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
                case 5:
                    damageType = DamageType.draw;
                    break;
                case 6:
                    damageType = DamageType.explosion;
                    break;
                case 7:
                    damageType = DamageType.push_to_mid;
                    break;
                case 8:
                    damageType = DamageType.high;
                    break;
                case 9:
                    damageType = DamageType.push_to_mid_slight;
                    break;
                case 10:
                    damageType = DamageType.same_height_to_mid;
                    break;
                default:
                    damageType = DamageType.light_damage_forward;
                    break;
            }
            return damageType; 
        }
        
        public static int WeaponHeavyCal(DamageType me)
        {
            switch (me)
            {
                case DamageType.none:
                case DamageType.slight_damage_forward:
                    return 0;
                case DamageType.light_damage_forward:
                    return 1;
                case DamageType.heavy_damage_forward:
                case DamageType.same_height_to_mid:
                case DamageType.draw:
                case DamageType.high:
                case DamageType.push_to_mid:
                case DamageType.push_to_mid_slight:
                    return 2;
                case DamageType.explosion:
                case DamageType.supper_damage_forward:
                    return 3;
                default:
                    return 1;
            }
        }
        
        public static float WpHpCost(int meLevel, int counterdLevel)
        {
            if (meLevel > counterdLevel)
            {
                switch (meLevel - counterdLevel)
                {
                    case 1:
                        return 0.5f;
                    case 2:
                        return 0.25f;
                    case 3:
                        return 0.2f;
                }
            }
            return 1;
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
        push_to_mid_slight = 9,
        same_height_to_mid = 10,
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