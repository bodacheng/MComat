using UnityEngine.UI;
using UnityEngine;
using mainMenu;

public class MailModel : MonoBehaviour
{
    public string mailId;
    public Image mailIcon;
    public Text title;
    public Text message;
    public Text presentlifeRemain;
    public Image rayCastTarget;
    public Button ReadMe;

    void Awake()
    {
        ReadMe.onClick.RemoveAllListeners();
        ReadMe.onClick.AddListener(ReadMail);
    }
    
    void ReadMail()
    {
        MailDetailProcess.targetMailID = mailId;
        PreScene.target.trySwitchToStep(MainSceneStep.MailDetail, true);
    }
}
