using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace mainMenu
{
    public class ReturnButtonManager : MonoBehaviour
    {
        public Button ReturnButton;
        static Button ToUseReturnButton;
        static UnityEvent unityEvent = new UnityEvent();
        public static readonly List<UnityAction> returnMissionList = new List<UnityAction>();
        
        void Awake()
        {
            ToUseReturnButton = ReturnButton;
        }
        
        public static void POP()
        {
            if (returnMissionList.Count == 0)
                return;
            unityEvent.RemoveAllListeners();
            unityEvent.AddListener(returnMissionList[returnMissionList.Count - 1]);
            unityEvent.Invoke();
            returnMissionList.RemoveAt(returnMissionList.Count - 1);
            if (returnMissionList.Count == 0)
            {
                ToUseReturnButton.gameObject.SetActive(false);
            }else{
                ToUseReturnButton.gameObject.SetActive(true);
            }
        }
        
        public static void AddFeatureToReturnButton()
        {
            ToUseReturnButton.onClick.RemoveAllListeners();
            ToUseReturnButton.onClick.AddListener(POP);
            ToUseReturnButton.gameObject.SetActive(true);
        }

        public static void PUSH(UnityAction onemission)
        {
            returnMissionList.Add(onemission);
            AddFeatureToReturnButton();
        }
        
        public void Clear()
        {
            ReturnButton.gameObject.SetActive(false);
            returnMissionList.Clear();
        }
    }
}