using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using dataAccess;
using System;

namespace mainMenu
{
    public partial class ArcadeManager : MonoBehaviour
    {
        private Action EndDragExtra;
        
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
            VerticalLayoutGroup vGroup = ButtonsContainer.GetComponent<VerticalLayoutGroup>();
            ButtonsContainer.sizeDelta = new Vector2(ButtonsContainer.sizeDelta.x, (pretab.button.GetComponent<RectTransform>().rect.height + vGroup.spacing) * ArcadeStages.Count);
            
            JumpToNewStage.onClick.RemoveAllListeners();
            Action JumpToButtonFeature = () => {};
            if (mode == 1)
            {
                JumpToButtonFeature = () =>
                {
                    JumpTo(CurrentTargetScrollbarValue());
                };
                EndDragExtra = NormalAlignment;
            }
            if (mode == 2)
            {
                JumpToButtonFeature = () =>
                {
                    JumpTo(PageD(Account._AccInfo.ArcadeProcess, StageCount));
                };
                EndDragExtra = FiveSetAlignment;
            }
            JumpToNewStage.onClick.AddListener(JumpToButtonFeature.Invoke);
            RefreshRender();
        }
        
        public void JumpTo(float target)
        {
            DOTween.To(() => _Scrollbar.value, x => _Scrollbar.value = x, target, 0.5f).
                OnComplete((() => {EndDragExtra.Invoke();}));
        }
        
        void RefreshRender()
        {
            foreach (KeyValuePair<int, StageInfo> keyValuePair in ArcadeStages)
            {
                Image buttonImage = keyValuePair.Value.stageButton.GetComponent<Image>();
                Animator buttonAnimator = keyValuePair.Value.stageButton.GetComponent<Animator>();
                if (buttonAnimator != null)
                    buttonAnimator.enabled = Account._AccInfo.ArcadeProcess == keyValuePair.Key;
                if (Account._AccInfo.ArcadeProcess >= keyValuePair.Key)
                {
                    keyValuePair.Value.ChangeColorOfIcons(true);
                }else{
                    keyValuePair.Value.ChangeColorOfIcons(false);
                }
            }
        }
    }
}