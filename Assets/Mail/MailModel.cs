using UnityEngine.UI;
using UnityEngine;
using mainMenu;

public class MailModel : MonoBehaviour
{
    public string mailId;
    public Image mailIcon;
    public Text title;
    public Text presentlifeRemain;
    public GameObject ReadFlag;
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

    // 根据报酬不同显示不同的图片
    // 已经获取的话直接就显示个read标签

    public void LoadPic(string itemId)
    {
    }
}