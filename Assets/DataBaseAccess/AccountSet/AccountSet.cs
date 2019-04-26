using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.Linq;

public partial class AccountSet {

    public static AccountSet instance;
    public playerinfoReferenceMode _playerinfoReferenceMode;//至关重要的逻辑。其他所有玩家信息索引模块将参考本单例的该值来决定玩家存档读取模式。

    public PlayerAccountInfo localCustomerInfo;//本单例模式的处理对象,一个参照数据库来定值的变量。

    private AccountSet()
    {
    }

    public static AccountSet Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AccountSet();
            }
            return instance;
        }
    }

    public IEnumerator loadCustomerInfo()
    {
        switch (_playerinfoReferenceMode)
        {
            case playerinfoReferenceMode.formalVersion:
                break;
            case playerinfoReferenceMode.localTestSaveData:
                yield return loadCustomerInfoViaLocalFile();
                break;
            case playerinfoReferenceMode.remoteTestPlayer:
                yield return loadCustomerInfoFromRemoteServer("http://47.245.7.100:8080/player/1/detail");
                break;
        }
    }

    public IEnumerator saveCustomerInfo()
    {
        switch (_playerinfoReferenceMode)
        {
            case playerinfoReferenceMode.formalVersion:
                break;
            case playerinfoReferenceMode.localTestSaveData:
                yield return overrideAccountOnLocalFile();
                break;
            case playerinfoReferenceMode.remoteTestPlayer:
                break;
        }
    }
}
