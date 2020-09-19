namespace HittingDetection
{
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
}