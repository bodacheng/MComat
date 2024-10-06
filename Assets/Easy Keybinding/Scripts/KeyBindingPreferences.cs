// Copyright 2024 Charged Software LLC

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyKeyBinding
{
    [System.Serializable]
    public class KeyBindingPreferences
    {
        public string[] KeyBinds;

        public KeyBindingPreferences(string[] kbinds)
        {
            KeyBinds = kbinds;
        }
    }
}


