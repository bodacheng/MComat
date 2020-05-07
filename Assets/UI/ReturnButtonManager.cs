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
        static UnityEvent UnityEvent = new UnityEvent();
        public static readonly List<UnityAction> ReturnMissionList = new List<UnityAction>();
        
        void Awake()
        {
            ToUseReturnButton = ReturnButton;
        }
        
        public static void POP()
        {
            if (ReturnMissionList.Count == 0)
                return;
            UnityEvent.RemoveAllListeners();
            UnityEvent.AddListener(ReturnMissionList[ReturnMissionList.Count - 1]);
            UnityEvent.Invoke();
            ReturnMissionList.RemoveAt(ReturnMissionList.Count - 1);
            if (ReturnMissionList.Count == 0)
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
            ReturnMissionList.Add(onemission);
            AddFeatureToReturnButton();
        }
        
        public void Clear()
        {
            ReturnButton.gameObject.SetActive(false);
            ReturnMissionList.Clear();
        }
    }
}