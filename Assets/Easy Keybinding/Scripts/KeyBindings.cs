// Copyright 2024 Charged Software LLC

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EasyKeyBinding
{
    public static class KeyBindings
    {
        public static string[] KeyBindArray;  // The array of all key bindings
        public static string[] DefaultKeyBindArray;  // The default key bindings are stored here.  This can change while keys are being assigned.
        public static string[] LoadedKeyBindArray;  // The key binding array that was loaded with the game options
        public static int keybindingindex; // An index used to determine which key to bind
    }
}
