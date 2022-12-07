using UnityEngine.UI;
using UnityEngine;
using mainMenu;
using System;
using UniRx;

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
    [SerializeField] Image bg;
    [SerializeField] Image mailIcon;
    [SerializeField] Text title;
    [SerializeField] Text presentlifeRemain;
    [SerializeField] GameObject ReadFlag;
    [SerializeField] Button ClaimBtn;
    [SerializeField] Button ReadMe;

    string _itemInstanceId;
    string _itemId;

    public bool claimed = false;

    public void PassMailInfo(MailItemInstance mailData, Action sort)
    {
        _itemInstanceId = mailData.ItemInstanceId;
        _itemId = mailData.ItemId;
        title.text = mailData.DisplayName;

        if (mailData.DisplayName.Contains("DM"))
        {
            mailIcon.sprite = DefaultIconSetting._diamondIcon;
        }
        else if (mailData.DisplayName.Contains("GD"))
        {
            mailIcon.sprite = DefaultIconSetting._coinIcon;
        }

        if (mailData.Expiration.HasValue)
        {
            presentlifeRemain.text = mailData.Expiration.Value.ToString("yyyy-MM-dd");
        }
        else
        {
            presentlifeRemain.gameObject.SetActive(false);
        }
        mailData.ReadObservable.Subscribe(AsRead).AddTo(this.gameObject);
        
        ClaimBtn.onClick.RemoveAllListeners();
        ClaimBtn.onClick.AddListener(
            () => PlayFabReadClient.ClaimPresent(
                mailData.ItemId,
                x =>
                {
                    PlayFabReadClient.SaveReadMailAsJson(x);
                    sort.Invoke();
                }
            )
        );
        
        //  暂不需要详细读取邮件功能
        ReadMe.onClick.RemoveAllListeners();
        ReadMe.onClick.AddListener(ReadMail);
        
        mailData.Set();
    }
    
    private Color unreadc = new Color(0.4f,0.4f,1, 1);
    private Color readc = new Color(0.4f,0.4f,1, 0.6f); 
    void AsRead(bool read)
    {
        claimed = read;
        presentlifeRemain.gameObject.SetActive(!read);
        ClaimBtn.gameObject.SetActive(!read);
        ReadFlag.SetActive(read);
        bg.color = read ? readc : unreadc;
    }
    
    void ReadMail()
    {
        PreScene.target.trySwitchToStep(MainSceneStep.MailDetail, _itemInstanceId, true);
    }
    
    // 根据报酬不同显示不同的图片
    // 已经获取的话直接就显示个read标签

    public void LoadPic(string itemId)
    {
    }
}