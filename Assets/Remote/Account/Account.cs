using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System;
using mainMenu;

namespace dataAccess
{
    public partial class Account
    {
        public static PlayerAccountInfo _AccInfo;//本单例模式的处理对象,一个参照数据库来定值的变量。
    }
}