using UnityEngine;

[CreateAssetMenu(fileName = "PlayfabSetting", menuName = "ScriptableObjects/PlayfabSetting", order = 2)]

public class PlayfabSetting : ScriptableObject
{
    public string UnitCatalog;
    public string StoneCatalog;
    public string MailCatalog;
    public int VersionMaxStoneLevel;
    public string GoldCode;
    public string DiamondCode;
    public string ArenaTicketCode;
    public string AdTicketCode;
    public string GDGotchaCode;
    public string DMGotchaCode;
    public int adTicketRewardDM;
    public int adFightRewardDM;
    
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
    }

    public static int ArenaPointToRank(int point)
    {
        var rank = point / 100;
        rank = Mathf.Clamp(rank, 0, 5);
        return rank;
    }
}
