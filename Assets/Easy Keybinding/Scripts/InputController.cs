// Copyright 2024 Charged Software LLC

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

namespace EasyKeyBinding
{
    public class InputController : MonoBehaviour
    {
        // Key binding indices:  Each key you want to configure will require a unique index.  Use the comments below to keep track of each index.  Modify the comments below as needed.  The indices below are only for the sample scene.
        /*
        Up - 0
        Down - 1
        Left - 2
        Right - 3
        Jump - 4
        Fire 1 - 5
        Fire 2 - 6
        Fire 3 - 7
        */

        // These are the indicators used for the sample Keybindings scene that comes with the asset.  These should be deleted in your project, they're only for the sample scene for demonstration purposes.
        [Header("Indicators (for sample scene only)")]
        [SerializeField] public Image UpIndicator;
        [SerializeField] public Image DownIndicator;
        [SerializeField] public Image LeftIndicator;
        [SerializeField] public Image RightIndicator;
        [SerializeField] public Image JumpIndicator;
        [SerializeField] public Image Fire1Indicator;
        [SerializeField] public Image Fire2Indicator;
        [SerializeField] public Image Fire3Indicator;

        // The text object in each key binding prefab should be included here.  Add the text object for new key binding prefabs here.  This text will display the letter or key name associated with that key binding.
        [Header("Keybinding Button Prefab Text Objects")]
        [SerializeField] public Text UpKeyBindingText;
        [SerializeField] public Text DownKeyBindingText;
        [SerializeField] public Text LeftKeyBindingText;
        [SerializeField] public Text RightKeyBindingText;
        [SerializeField] public Text JumpKeyBindingText;
        [SerializeField] public Text Fire1KeyBindingText;
        [SerializeField] public Text Fire2KeyBindingText;
        [SerializeField] public Text Fire3KeyBindingText;

        // Initializes the key codes.  Each key in your project should have a keycode defined here.  Add new keycodes here.  Delete unused ones.
        KeyCode UpKeyCode;
        KeyCode DownKeyCode;
        KeyCode LeftKeyCode;
        KeyCode RightKeyCode;
        KeyCode JumpKeyCode;
        KeyCode Fire1KeyCode;
        KeyCode Fire2KeyCode;
        KeyCode Fire3KeyCode;

        void Start()
        {
            // Set the factory default key bindings here.  This array will set the key bindings for new users who opened the program for the first time or for resetting the key bindings to factory default.
            // Every key you plan to use must be assigned a factory default.
            // See the KeyCode page in the Unity documentation for the various key names that can be used here:  https://docs.unity3d.com/ScriptReference/KeyCode.html
            KeyBindings.DefaultKeyBindArray = new string[] { "W", "S", "A", "D", "Space", "Mouse0", "Mouse1", "Mouse2" };

            // Loads the saved key bindings
            LoadKeyBindings();
        }

        // Update is called once per frame
        void Update()
        {
            // This section decides what actions are taken when each key is pressed.  Add all your actions here.  Delete the ones below which are only for the sample scene.

            // Executes when the up key is pressed
            if (Input.GetKeyDown(UpKeyCode))
            {
                UpIndicator.color = Color.green;
            }

            // Executes when the up key is released
            if (Input.GetKeyUp(UpKeyCode))
            {
                UpIndicator.color = Color.white;
            }

            // Executes when the down key is pressed
            if (Input.GetKeyDown(DownKeyCode))
            {
                DownIndicator.color = Color.green;
            }

            // Executes when the down key is released
            if (Input.GetKeyUp(DownKeyCode))
            {
                DownIndicator.color = Color.white;
            }

            // Executes when the left key is pressed
            if (Input.GetKeyDown(LeftKeyCode))
            {
                LeftIndicator.color = Color.green;
            }

            // Executes when the left key is released
            if (Input.GetKeyUp(LeftKeyCode))
            {
                LeftIndicator.color = Color.white;
            }

            // Executes when the right key is pressed
            if (Input.GetKeyDown(RightKeyCode))
            {
                RightIndicator.color = Color.green;
            }

            // Executes when the right key is released
            if (Input.GetKeyUp(RightKeyCode))
            {
                RightIndicator.color = Color.white;
            }

            // Executes when the jump key is pressed
            if (Input.GetKeyDown(JumpKeyCode))
            {
                JumpIndicator.color = Color.green;
            }

            // Executes when the jump key is released
            if (Input.GetKeyUp(JumpKeyCode))
            {
                JumpIndicator.color = Color.white;
            }

            // Executes when the fire1 key is pressed
            if (Input.GetKeyDown(Fire1KeyCode))
            {
                Fire1Indicator.color = Color.green;
            }

            // Executes when the fire1 key is released
            if (Input.GetKeyUp(Fire1KeyCode))
            {
                Fire1Indicator.color = Color.white;
            }

            // Executes when the fire2 key is pressed
            if (Input.GetKeyDown(Fire2KeyCode))
            {
                Fire2Indicator.color = Color.green;
            }

            // Executes when the fire2 key is released
            if (Input.GetKeyUp(Fire2KeyCode))
            {
                Fire2Indicator.color = Color.white;
            }

            // Executes when the fire3 key is pressed
            if (Input.GetKeyDown(Fire3KeyCode))
            {
                Fire3Indicator.color = Color.green;
            }

            // Executes when the fire3 key is released
            if (Input.GetKeyUp(Fire3KeyCode))
            {
                Fire3Indicator.color = Color.white;
            }
        }

        // Loads the saved key bindings
        void LoadKeyBindings()
        {
            FileStream file;
            string keybindingspath = Application.persistentDataPath + "/KeyBindingPreferences.dat";

            // If the key bindings file exists, read it, otherwise set the factory default key bindings, save a new file and return.
            if (File.Exists(keybindingspath))
            {
                file = File.OpenRead(keybindingspath);
            }
            else
            {
                KeyBindings.KeyBindArray = KeyBindings.DefaultKeyBindArray;
                SaveKeyBindings();
                return;
            }

            // Opens the key bindings file
            BinaryFormatter bf = new BinaryFormatter();
            KeyBindingPreferences keybindingsloaddata = (KeyBindingPreferences)bf.Deserialize(file);
            file.Close();

            // Copies the key bindings from the file into memory
            KeyBindings.KeyBindArray = keybindingsloaddata.KeyBinds;
            KeyBindings.LoadedKeyBindArray = new string[KeyBindings.KeyBindArray.Length];
            Array.Copy(KeyBindings.KeyBindArray, KeyBindings.LoadedKeyBindArray, KeyBindings.KeyBindArray.Length);

            // Assigns the key bindings to key codes so they can be called
            AssignKeyBindings();
        }

        // Saves the key bindings
        public void SaveKeyBindings()
        {
            string keybindingspath = Application.persistentDataPath + "/KeyBindingPreferences.dat";

            FileStream file;

            if (File.Exists(keybindingspath))
            {
                file = File.OpenWrite(keybindingspath);
            }
            else
            {
                file = File.Create(keybindingspath);
            }

            KeyBindingPreferences keybindingsdata = new KeyBindingPreferences(KeyBindings.KeyBindArray);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, keybindingsdata);
            file.Close();

            LoadKeyBindings();
        }

        // Assigns the key bindings to key codes so they can be called.  Any new key codes should be added here.
        void AssignKeyBindings()
        {
            UpKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), KeyBindings.KeyBindArray[0]);
            DownKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), KeyBindings.KeyBindArray[1]);
            LeftKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), KeyBindings.KeyBindArray[2]);
            RightKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), KeyBindings.KeyBindArray[3]);
            JumpKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), KeyBindings.KeyBindArray[4]);
            Fire1KeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), KeyBindings.KeyBindArray[5]);
            Fire2KeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), KeyBindings.KeyBindArray[6]);
            Fire3KeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), KeyBindings.KeyBindArray[7]);
        }

        // Loads the fields for the key bindings panel when it is opened
        public void OpenKeybindingPanel()
        {
            // Do not modify this line
            Array.Copy(KeyBindings.LoadedKeyBindArray, KeyBindings.KeyBindArray, KeyBindings.LoadedKeyBindArray.Length);

            // Add, modify or delete these lines based on your project's needs.  Each key binding prefab text will need to be updated here.
            UpKeyBindingText.text = KeyBindingText(0);
            DownKeyBindingText.text = KeyBindingText(1);
            LeftKeyBindingText.text = KeyBindingText(2);
            RightKeyBindingText.text = KeyBindingText(3);
            JumpKeyBindingText.text = KeyBindingText(4);
            Fire1KeyBindingText.text = KeyBindingText(5);
            Fire2KeyBindingText.text = KeyBindingText(6);
            Fire3KeyBindingText.text = KeyBindingText(7);
        }

        // Sets the index of the key to be bound in the key bindings panel.  This method should be selected under On Click () in the Button component of the inspector for the BindKey prefab.
        public void SetKeyBindingIndex(int keyindex)
        {
            KeyBindings.keybindingindex = keyindex;
        }

        // Generates a key string based on the keybinding index.  Useful for indicating the key binding to the user in the game.  Use it whenever you need to tell the user what key to press.
        public string KeyBindingText(int keybindid)
        {
            // Gets they full keybinding string from the keybindarray
            string outputtext = KeyBindings.KeyBindArray[keybindid];

            // Changes the keybind string for certain keys.  Deletes the alpha from the number key names and makes the mouse button names more descriptive.
            if (outputtext.Contains("Alpha"))
            {
                outputtext = outputtext.Substring(5);
            }
            else if (outputtext.Contains("Mouse0"))
            {
                outputtext = "Left Click";
            }
            else if (outputtext.Contains("Mouse1"))
            {
                outputtext = "Right Click";
            }
            else if (outputtext.Contains("Mouse2"))
            {
                outputtext = "Middle Click";
            }

            return outputtext;
        }

        // Resets the key bindings to factory default, saves the bindings, and refreshes the key binding panel
        public void ResetKeyBindings()
        {
            KeyBindings.KeyBindArray = KeyBindings.DefaultKeyBindArray;
            SaveKeyBindings();
            OpenKeybindingPanel();
        }
    }
}