using UnityEngine.UI;
using UnityEngine;

public class FightModeSwitch : MonoBehaviour
{
    [SerializeField] private BOButton btn;
    [SerializeField] private Text modeText;
    [SerializeField] private Animator animator;
    
    private FightMode _fightMode;
    public FightMode FightMode => _fightMode;
    
    void OnClick()
    {
        if (_fightMode == FightMode.Rotate)
        {
            PlayerPrefs.SetInt("preferAdventureMode", 2);
            SetMode(FightMode.Multi);
        }
        else if (_fightMode == FightMode.Multi)
        {
            PlayerPrefs.SetInt("preferAdventureMode", 1);
            SetMode(FightMode.Rotate);
        }
    }

    public void Setup(FightMode fightMode)
    {
        switch (fightMode)
        {
            case FightMode.Multi:
                btn.interactable = false;
                animator.enabled = false;
                SetMode(FightMode.Multi);
            break;
            case FightMode.Rotate:
                btn.interactable = false;
                animator.enabled = false;
                SetMode(FightMode.Rotate);
            break;
            default:
                btn.onClick.AddListener(OnClick);
                btn.interactable = true;
                animator.enabled = true;
                SetMode((FightMode)PlayerPrefs.GetInt("preferAdventureMode", 1));
            break;
        }
    }

    void SetMode(FightMode mode)
    {
        _fightMode = mode;
        if (_fightMode == FightMode.Rotate)
        {
            modeText.text = Translate.Get("TeamModeR");
        }
        if (_fightMode == FightMode.Multi)
        {
            modeText.text = Translate.Get("TeamModeM");
        }
    }
}
