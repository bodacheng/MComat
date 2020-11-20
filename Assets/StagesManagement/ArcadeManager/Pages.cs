using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using dataAccess;

namespace mainMenu
{
    public partial class ArcadeManager : MonoBehaviour
    {
        // 分页排版
        // mode 1: 无限模式 2. 5个关卡算一个章节模式
        public void INIPagingSystem(int mode)
        {
            // 假设有100关，然后按钮应该是越往下拖关卡数越大，才能和JumpToNewest()堆起来
            for (int i = StageCount; i > -1; i--)
            {
                if (!ArcadeStages.ContainsKey(i))
                {
                    continue;
                }
                ArcadeStages[i].stageButton.gameObject.SetActive(true);
                ArcadeStages[i].stageButton.gameObject.transform.SetParent(ButtonsContainer);
                ArcadeStages[i].stageButton.gameObject.transform.localScale = Vector3.one;
            }
            VerticalLayoutGroup verticalLayoutGroup = ButtonsContainer.GetComponent<VerticalLayoutGroup>();
            ButtonsContainer.sizeDelta = new Vector2(ButtonsContainer.sizeDelta.x, (pretab.button.GetComponent<RectTransform>().rect.height + verticalLayoutGroup.spacing) * ArcadeStages.Count);
            
            if (mode == 1)
            {
                MugenPageFeature();
            }
            if (mode == 2)
            {
                OneChapterFiveLevel();
            }
            RefreshRender();
        }
        
        public void JumpTo(float target)
        {
            DOTween.To(() => _Scrollbar.value, x => _Scrollbar.value= x, target, 0.5f);
        }
        
        void RefreshRender()
        {
            foreach (KeyValuePair<int, StageInfo> keyValuePair in ArcadeStages)
            {
                Image buttonImage = keyValuePair.Value.stageButton.GetComponent<Image>();
                Animator buttonAnimator = keyValuePair.Value.stageButton.GetComponent<Animator>();
                if (buttonAnimator != null)
                    buttonAnimator.enabled = AccountSet._AccInfo.ArcadeProcess == keyValuePair.Key;
                if (AccountSet._AccInfo.ArcadeProcess >= keyValuePair.Key)
                {
                    keyValuePair.Value.ChangeColorOfIcons(true);
                }else{
                    keyValuePair.Value.ChangeColorOfIcons(false);
                }
            }
        }
    }
}