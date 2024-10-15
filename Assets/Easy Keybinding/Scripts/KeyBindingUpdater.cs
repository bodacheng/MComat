// Copyright 2024 Charged Software LLC

using UnityEngine;
using UnityEngine.UI;

namespace EasyKeyBinding
{
    public class KeyBindingUpdater : MonoBehaviour
    {
        [SerializeField] InputController InputController;

        // These text objects indicate the saved key binding below each indicator in the sample scene.  This text will display the letter or key name associated with that key binding.
        [Header("Keybinding Button Prefab Text Objects")]
        [SerializeField] public Text UpIndicatorText;
        [SerializeField] public Text DownIndicatorText;
        [SerializeField] public Text LeftIndicatorText;
        [SerializeField] public Text RightIndicatorText;
        [SerializeField] public Text JumpIndicatorText;
        [SerializeField] public Text Fire1IndicatorText;
        [SerializeField] public Text Fire2IndicatorText;
        [SerializeField] public Text Fire3IndicatorText;
        [SerializeField] public Text DreamComboIndicatorText;

        bool initkeybindingindicators = false;

        void Update()
        {
            // Update the key binding indicator text at startup for the sample scene indicators.  Only needed for sample scene.
            if (!initkeybindingindicators)
            {
                INI();
            }
        }

        public void INI()
        {
            if (KeyBindings.KeyBindArray != null)
            {
                UpdateKeyBindings();
                initkeybindingindicators = true;
            }
        }

        // Updates the key binding text below every indicator in the sample scene.
        // You don't need to copy this exact method for your project, but you will need to write something similar if you want to automatically update key binding text indicators with the latest binding.
        public void UpdateKeyBindings()
        {
            // Updates each text indicator with the appropriate key binding, based on the key binding index for each action.
            UpIndicatorText.text = InputController.KeyBindingText(0);
            DownIndicatorText.text = InputController.KeyBindingText(1);
            LeftIndicatorText.text = InputController.KeyBindingText(2);
            RightIndicatorText.text = InputController.KeyBindingText(3);
            JumpIndicatorText.text = InputController.KeyBindingText(4);
            Fire1IndicatorText.text = InputController.KeyBindingText(5);
            Fire2IndicatorText.text = InputController.KeyBindingText(6);
            Fire3IndicatorText.text = InputController.KeyBindingText(7);
            DreamComboIndicatorText.text = InputController.KeyBindingText(8);
        }
    }
}

