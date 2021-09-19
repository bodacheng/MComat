using UnityEngine.UI;
using UnityEngine;
using mainMenu;

/// <summary>
/// 邮件ListView的开发主要有如下问题
/// 1. 邮件icon。这个取决于这个邮件送的礼物是什么
/// 2. 是否已经提取了邮件中的礼物的flag。这个简单做的话就是覆盖在icon上面的一个"Got"标志
/// 3. 邮件的基本文本显示
/// 4. 获取倒计时
///
/// 一个有点麻烦的地方在于，如果这个邮件箱的空间有限，那么邮件箱内部邮件有累积并超过了邮件箱的尺寸的话...
/// 我们想到一个问题是login bonus和邮件礼物真是两码事
/// </summary>

public class MailListView : MonoBehaviour
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
        PreScene.target.trySwitchToStep(MainSceneStep.MailDetail, mailId, true);
    }

    // 根据报酬不同显示不同的图片
    // 已经获取的话直接就显示个read标签

    public void LoadPic(string itemId)
    {
    }
}