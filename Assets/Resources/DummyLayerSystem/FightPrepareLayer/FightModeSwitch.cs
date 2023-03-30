using UnityEngine.UI;
using UnityEngine;

public class FightModeSwitch : MonoBehaviour
{
    [SerializeField] private Text modeText;
    
    private TeamMode _teamMode;
    public TeamMode TeamMode => _teamMode;
    
    public void OnClick()
    {
        if (_teamMode == TeamMode.Rotation)
        {
            Set(TeamMode.MultiRaid);
        }
        else if (_teamMode == TeamMode.MultiRaid)
        {
            Set(TeamMode.Rotation);
        }
    }

    public void Set(TeamMode mode)
    {
        _teamMode = mode;
        if (_teamMode == TeamMode.Rotation)
        {
            modeText.text = Translate.Get("TeamModeR");
        }
        if (_teamMode == TeamMode.MultiRaid)
        {
            modeText.text = Translate.Get("TeamModeM");
        }
    }
}
