using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace mainMenu
{
    public class ReturnButtonManager : MonoBehaviour
    {
        public Button ReturnButton;
        UnityEvent unityEvent = new UnityEvent();
        public List<UnityAction> returnMissionList = new List<UnityEngine.Events.UnityAction>();

        public void PUSH(UnityAction onemission)
        {
            returnMissionList.Add(onemission);
            Debug.Log("返回菜单深度" + returnMissionList.Count);
            UnityAction onemissionAndPop = () =>
            {
                unityEvent.RemoveAllListeners();
                unityEvent.AddListener(returnMissionList[returnMissionList.Count - 1]);
                unityEvent.Invoke();
                returnMissionList.RemoveAt(returnMissionList.Count - 1);
                if (returnMissionList.Count == 0)
                    ReturnButton.gameObject.SetActive(false);
            };

            ReturnButton.onClick.RemoveAllListeners();
            ReturnButton.onClick.AddListener(onemissionAndPop);

            ReturnButton.gameObject.SetActive(true);
        }

        public void Clear()
        {
            ReturnButton.gameObject.SetActive(false);
            returnMissionList.Clear();
        }
    }
}