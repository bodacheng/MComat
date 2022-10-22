using System;
using DummyLayerSystem;
using UnityEngine;
using mainMenu;
using TMPro;

public class NickNameLayer : UILayer
{
    [SerializeField] private TMP_InputField nickNameInput;
    [SerializeField] private P3Button OK;
    
    
    public static NickNameLayer Open(Action<string> setNickName)
    {
        var returnValue = UILayerLoader.Get<NickNameLayer>();
        if (returnValue != null)
        {
            return returnValue;
        }
        returnValue = UILayerLoader.Load(PreScene.target.T.gameObject,"NickNameLayer") as NickNameLayer;
        returnValue.OK.AddListener(()=>
        {
            setNickName.Invoke(returnValue.nickNameInput.text);
        });
        
        return returnValue;
    }
}
