using System;
using UnityEngine;
using TMPro;

public class NickNameLayer : UILayer
{
    [SerializeField] private TMP_InputField nickNameInput;
    [SerializeField] private P3Button OK;
    
    public void Setup(Action<string> setNickName)
    {
        OK.AddListener(()=>
        {
            setNickName.Invoke(nickNameInput.text);
        });
    }

    public void LoadingRender(bool loading)
    {
        nickNameInput.interactable = !loading;
        OK.interactable = !loading;
    }
}
