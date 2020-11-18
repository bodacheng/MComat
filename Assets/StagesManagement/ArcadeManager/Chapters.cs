using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace mainMenu
{
    public partial class ArcadeManager : MonoBehaviour
    {
        public IEnumerator PageRefresh()
        {
             // 假设有100关，然后按钮应该是越往下拖关卡数越大，才能和JumpToNewest()堆起来
            for (int i = 100; i > -1; i--)
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
            
            JumpToNewStage.onClick.RemoveAllListeners();
            JumpToNewStage.onClick.AddListener(JumpTo);

            RefreshRender();
            JumpTo();

            yield break;
        }
    }
}