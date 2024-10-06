// Copyright 2024 Charged Software LLC

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace EasyKeyBinding
{
    // Binds a keyboard key to a command in the Game Options panel
    public class KeyBindingButton : MonoBehaviour, IPointerClickHandler
    {
        public string newkeybinding;
        public string newkeybindingtext;
        private bool buttonclicked;

        [SerializeField] private Text ButtonText = null;
        [SerializeField] private Text PressKeyText = null;
        [SerializeField] private Text ErrorText = null;

        // Update is called once per frame
        void Update()
        {
            // If the button is clicked, listens for a key press.  If a key press is detected, binds the key to the corresponding function and saves it in the KeyBindArray.
            if (buttonclicked)
            {
                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(vKey))
                    {
                        buttonclicked = false;
                        newkeybinding = vKey.ToString();
                        newkeybindingtext = KeyBindingTextButton(newkeybinding);

                        if (Array.Exists(KeyBindings.KeyBindArray, k => k == newkeybinding))
                        {
                            ErrorText.text = newkeybindingtext + " is in use";
                            ErrorText.gameObject.SetActive(true);
                        }
                        else
                        {
                            ErrorText.gameObject.SetActive(false);
                            ButtonText.text = newkeybindingtext;
                            KeyBindings.KeyBindArray[KeyBindings.keybindingindex] = newkeybinding;
                        }

                        PressKeyText.gameObject.SetActive(false);
                        ButtonText.gameObject.SetActive(true);
                    }
                }
            }
        }

        // When the button is clicked, hides the button text and shows the press key text.  When buttonclicked becomes true, the update method will start listening for a key press.
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            ButtonText.gameObject.SetActive(false);
            PressKeyText.gameObject.SetActive(true);
            buttonclicked = true;
        }

        // When the button is pressed but the operation is cancelled, restores the button text based on the saved key binding and turns off the error text.
        void OnDisable()
        {
            ErrorText.gameObject.SetActive(false);
            ButtonText.text = KeyBindings.KeyBindArray[KeyBindings.keybindingindex];
        }

        // Generates a key string based on the keybinding index.  Useful for indicating the key binding to the user in the game.  Use it whenever you need to tell the user what key to press.
        public string KeyBindingTextButton(string keybindstring)
        {
            // Changes the keybind string for certain keys.  Deletes the alpha from the number key names and makes the mouse button names more descriptive.
            if (keybindstring.Contains("Alpha"))
            {
                keybindstring = keybindstring.Substring(5);
            }
            else if (keybindstring.Contains("Mouse0"))
            {
                keybindstring = "Left Click";
            }
            else if (keybindstring.Contains("Mouse1"))
            {
                keybindstring = "Right Click";
            }
            else if (keybindstring.Contains("Mouse2"))
            {
                keybindstring = "Middle Click";
            }

            return keybindstring;
        }
    }
}
