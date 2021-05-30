using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using dataAccess;
using UniRx;

namespace mainMenu
{
    public partial class ArcadeManager : MonoBehaviour
    {
        void MugenPageFeature()
        {
            autoCommand = new SingleAssignmentDisposable
            {
                Disposable = Observable.EveryUpdate().Subscribe(_ =>
                    {
                        if (FightGlobalSetting.scenestep != 0 || JumpToNewStage.IsDestroyed() || JumpToNewStage == null || JumpToNewStage.gameObject == null)
                        {
                            autoCommand.Dispose();
                            return;
                        }
                        if (!JumpToNewStage.gameObject.activeSelf)
                        {
                            if (Mathf.Abs(CurrentTargetScrollbarValue() - _Scrollbar.value) > 0.1f)
                            {
                                JumpToNewStage.gameObject.SetActive(true);
                            }
                        }
                        else
                        {
                            if (Mathf.Abs(CurrentTargetScrollbarValue() - _Scrollbar.value) <= 0.1f)
                            {
                                JumpToNewStage.gameObject.SetActive(false);
                            }
                        }
                    }
                )
            };
            void temp()
            {
                JumpTo(CurrentTargetScrollbarValue());
            }
            JumpToNewStage.onClick.RemoveAllListeners();
            JumpToNewStage.onClick.AddListener(temp);
            temp();
        }
        
        float CurrentTargetScrollbarValue()
        {
            float targetScrollbarValue;
            if (Account._AccInfo.ArcadeProcess <= 3)
            {
                targetScrollbarValue = 0;
            }else{
                VerticalLayoutGroup verticalLayoutGroup = ButtonsContainer.GetComponent<VerticalLayoutGroup>();
                // 重点在于对Scrollbar.value的理解。这个值是scrollview边界目前超出框的长度与可能超出框框最大长度的比值
                targetScrollbarValue = 
                ((pretab.button.GetComponent<RectTransform>().rect.height + verticalLayoutGroup.spacing) * (Account._AccInfo.ArcadeProcess - 3)) // 分子。如果希望对象关卡不是出现在中间，可调整这个数字。
                / (ButtonsContainer.sizeDelta.y - _ScrollRect.GetComponent<RectTransform>().rect.height); // 分母
            }
            return targetScrollbarValue;
        }
    }
}