using UnityEngine;

[CreateAssetMenu(fileName = "PlayFabSetting", menuName = "ScriptableObjects/PlayFabSetting", order = 2)]

public class PlayFabSetting : ScriptableObject
{
    [SerializeField] string UnitCatalog;
    [SerializeField] string StoneCatalog;
    [SerializeField] string MailCatalog;
    [SerializeField] int VersionMaxStoneLevel;
    [SerializeField] string GoldCode;
    [SerializeField] string DiamondCode;
    [SerializeField] string ArenaTicketCode;
    [SerializeField] string AdTicketCode;
    [SerializeField] string GDGotchaCode;
    [SerializeField] string DMGotchaCode;
    [SerializeField] int adTicketRewardDM;
    [SerializeField] int adFightRewardDM;//指的是关卡战斗结束后观看广告获取的报酬，关卡号码是5的倍数的情况下是它2倍。
    [SerializeField] string arenaPointCode = "arenapoint";
    
    public static string _UnitCatalog;
    public static string _StoneCatalog;
    public static string _MailCatalog;
    public static int _VersionMaxStoneLevel;
    public static string _GoldCode;
    public static string _DiamondCode;
    public static string _ArenaTicketCode;
    public static string _AdTicketCode;
    public static string _GDGotchaCode;
    public static string _DMGotchaCode;
    public static int _adTicketRewardDM;
    public static int _adFightRewardDM;
    public static string _arenaPointCode;

    public void Initialise()
    {
        _UnitCatalog = UnitCatalog;
        _StoneCatalog = StoneCatalog;
        _MailCatalog = MailCatalog;
        _VersionMaxStoneLevel = VersionMaxStoneLevel;
        _GoldCode = GoldCode;
        _DiamondCode = DiamondCode;
        _ArenaTicketCode = ArenaTicketCode;
        _AdTicketCode = AdTicketCode;
        _GDGotchaCode = GDGotchaCode;
        _DMGotchaCode = DMGotchaCode;
        _adTicketRewardDM = adTicketRewardDM;
        _adFightRewardDM = adFightRewardDM;
        _arenaPointCode = arenaPointCode;
    }

    public static int ArenaPointToRank(int point)
    {
        var rank = point / 100;
        rank = Mathf.Clamp(rank, 0, 5);
        return rank;
    }
}
