using UnityEngine;

namespace HittingDetection
{
    public class V_Damage
    {
        public readonly FightParamsReference attacker;
        public readonly FightParamsReference victim;
        public readonly HitBoxManager from_weapon;
        public readonly Marker from_weapon_marker;
        public Vector3 DamageEffectPoint;
        public Vector3 impactComingPoint;
        public Quaternion CutRotation;
        
        public V_Damage() { }
        public V_Damage(HitBoxManager weapon, Marker weapon_marker, FightParamsReference _victim, FightParamsReference _attacker, Vector3 damageEffectPoint, Vector3 impactComingPoint,Quaternion _CutRotation)
        {
            from_weapon = weapon;
            from_weapon_marker = weapon_marker;
            attacker = _attacker;
            victim = _victim;
            DamageEffectPoint = damageEffectPoint;
            this.impactComingPoint = impactComingPoint;
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
                case 4:
                    damageType = DamageType.stable_damage;
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
                case 11:
                    damageType = DamageType.time_pause;
                    break;
                case 12:
                    damageType = DamageType.sekka;
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
                case DamageType.stable_damage:
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
    
        public static float WpHpCost(int meLevel, int counterLevel)
        {
            if (meLevel > counterLevel)
            {
                switch (meLevel - counterLevel)
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
}