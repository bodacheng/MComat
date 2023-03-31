using UnityEngine.UI;
using UnityEngine;

public class FightModeSwitch : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private Text modeText;
    [SerializeField] private Animator animator;
    
    private TeamMode _teamMode;
    public TeamMode TeamMode => _teamMode;
    
    void OnClick()
    {
        if (_teamMode == TeamMode.Rotation)
        {
            PlayerPrefs.GetInt("preferAdventureMode", 1);
            SetMode(TeamMode.MultiRaid);
        }
        else if (_teamMode == TeamMode.MultiRaid)
        {
            PlayerPrefs.GetInt("preferAdventureMode", 2);
            SetMode(TeamMode.Rotation);
        }
    }

    public void Setup(int arcadeFightMode, int defaultMode)
    {
        switch (arcadeFightMode)
        {
            case 1:
                btn.interactable = false;
                animator.enabled = false;
                SetMode(TeamMode.MultiRaid);
            break;
            case 2:
                btn.interactable = false;
                animator.enabled = false;
                SetMode(TeamMode.Rotation);
            break;
            default:
                btn.onClick.AddListener(OnClick);
                btn.interactable = true;
                animator.enabled = true;
                SetMode((TeamMode)defaultMode);
            break;
        }
    }

    void SetMode(TeamMode mode)
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
