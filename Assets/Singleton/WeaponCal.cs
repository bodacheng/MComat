using HittingDetection;

public static class WeaponCal
{
    public static int WeaponHeavyCal(DamageType me)
    {
        switch(me)
        {
            case DamageType.slight_damage_forward:
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
            }
        }
        return 1;
    }
}
