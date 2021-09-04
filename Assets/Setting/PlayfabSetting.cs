using UnityEngine;

[CreateAssetMenu(fileName = "PlayfabSetting", menuName = "ScriptableObjects/PlayfabSetting", order = 2)]

public class PlayfabSetting : ScriptableObject
{
    public string UnitCatalog;
    public string StoneCatalog;
    public string MailCatalog;
    public string GoldCode;
    public string DiamondCode;

    public static string _UnitCatalog;
    public static string _StoneCatalog;
    public static string _MailCatalog;
    public static string _GoldCode;
    public static string _DiamondCode;

    public void Initialise()
    {
        _UnitCatalog = UnitCatalog;
        _StoneCatalog = StoneCatalog;
        _MailCatalog = MailCatalog;
        _GoldCode = GoldCode;
        _DiamondCode = DiamondCode;
    }
}
