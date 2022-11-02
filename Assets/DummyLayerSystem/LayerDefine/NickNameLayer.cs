using System;
using System.Text.RegularExpressions;
using DummyLayerSystem;
using UnityEngine;
using Crosstales.BWF;
using UnityEngine.UI;

public class NickNameLayer : UILayer
{
    [SerializeField] private InputField nickNameInput;
    [SerializeField] private P3Button OK;
    [SerializeField] public P3Button Cancel;
    
    public void Setup(Action<string> setNickName)
    {
        OK.AddListener(()=>
        {
            setNickName.Invoke(nickNameInput.text);
        });
        Cancel.AddListener(UILayerLoader.Remove<NickNameLayer>);
        nickNameInput.onEndEdit.AddListener(BadWordFilter);
    }

    void BadWordFilter(string currentTxt)
    {
        var outPutTxt= Regex.Replace(currentTxt, "[\\s\\p{P}\n\r=<>$>+￥^]", "");
        outPutTxt = BWFManager.Instance.ReplaceAll(outPutTxt);
        OK.interactable = !outPutTxt.Contains("*");
        nickNameInput.text = outPutTxt;
    }

    public void LoadingRender(bool loading)
    {
        nickNameInput.interactable = !loading;
        OK.interactable = !loading;
    }
}
