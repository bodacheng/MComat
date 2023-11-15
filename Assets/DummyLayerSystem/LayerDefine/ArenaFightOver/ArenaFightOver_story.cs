using UnityEngine.UI;
using UnityEngine;

public partial class ArenaFightOver : UILayer
{
    [SerializeField] private Text shortStory;

    public bool LoadStory()
    {
        var code = FightLoad.Fight.ID;
        shortStory.text = ShortStory.Get(code);
        return !string.IsNullOrEmpty(shortStory.text);
    }
}
