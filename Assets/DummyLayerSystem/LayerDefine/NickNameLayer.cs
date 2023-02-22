using System;
using System.Text.RegularExpressions;
using DummyLayerSystem;
using UnityEngine;
using Crosstales.BWF;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

public class NickNameLayer : UILayer
{
    [SerializeField] private InputField nickNameInput;
    [SerializeField] private Button OK;
    [SerializeField] private Button Cancel;
    
    public void Setup(Action<string> setNickName, bool showCloseBtn)
    {
        OK.onClick.AddListener(async ()=>
        {
            if (String.IsNullOrEmpty(nickNameInput.text))
            {
                return;
            }
            
            BWFManager.Instance.Load();
            await UniTask.WaitUntil(()=> BWFManager.Instance.isReady);
            var filteredWord = BadWordFilter(nickNameInput.text);
            Debug.Log(filteredWord);
            if (filteredWord.Contains("*"))
            {
                PopupLayer.ArrangeWarnWindow(Translate.Get("illegalword"));
            }
            else
            {
                setNickName.Invoke(filteredWord);
            }
        });
        Cancel.gameObject.SetActive(showCloseBtn);
        Cancel.onClick.AddListener(UILayerLoader.Remove<NickNameLayer>);
    }

    string BadWordFilter(string currentTxt)
    {
        var outPutTxt= Regex.Replace(currentTxt, "[\\s\\p{P}\n\r=<>$>+￥^]", "");
        outPutTxt = BWFManager.Instance.ReplaceAll(outPutTxt);
        return outPutTxt;
    }
    
    public void LoadingRender(bool loading)
    {
        nickNameInput.interactable = !loading;
        OK.interactable = !loading;
    }
}
