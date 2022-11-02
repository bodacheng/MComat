using System;
using DummyLayerSystem;
using UnityEngine;
using TMPro;

public class NickNameLayer : UILayer
{
    [SerializeField] private TMP_InputField nickNameInput;
    [SerializeField] private P3Button OK;
    [SerializeField] public P3Button Cancel;
    
    public void Setup(Action<string> setNickName)
    {
        OK.AddListener(()=>
        {
            setNickName.Invoke(nickNameInput.text);
        });
        Cancel.AddListener(UILayerLoader.Remove<NickNameLayer>);
    }

    public void LoadingRender(bool loading)
    {
        nickNameInput.interactable = !loading;
        OK.interactable = !loading;
    }
}
