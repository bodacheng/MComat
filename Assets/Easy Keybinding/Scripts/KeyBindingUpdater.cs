// Copyright 2024 Charged Software LLC

using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace EasyKeyBinding
{
    public class KeyBindingUpdater : MonoBehaviour
    {
        [FormerlySerializedAs("InputController")] [SerializeField] InputController inputController;

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

        public InputController InputController => inputController;
        private bool _initKeyBindingIndicators = false;

        void Update()
        {
            // Update the key binding indicator text at startup for the sample scene indicators.  Only needed for sample scene.
            if (!_initKeyBindingIndicators)
            {
                INI();
            }
        }

        public void INI()
        {
            if (KeyBindings.KeyBindArray != null)
            {
                UpdateKeyBindings();
                _initKeyBindingIndicators = true;
            }
        }

        // Updates the key binding text below every indicator in the sample scene.
        // You don't need to copy this exact method for your project, but you will need to write something similar if you want to automatically update key binding text indicators with the latest binding.
        public void UpdateKeyBindings()
        {
            // Updates each text indicator with the appropriate key binding, based on the key binding index for each action.
            UpIndicatorText.text = inputController.KeyBindingText(0);
            DownIndicatorText.text = inputController.KeyBindingText(1);
            LeftIndicatorText.text = inputController.KeyBindingText(2);
            RightIndicatorText.text = inputController.KeyBindingText(3);
            JumpIndicatorText.text = inputController.KeyBindingText(4);
            Fire1IndicatorText.text = inputController.KeyBindingText(5);
            Fire2IndicatorText.text = inputController.KeyBindingText(6);
            Fire3IndicatorText.text = inputController.KeyBindingText(7);
            DreamComboIndicatorText.text = inputController.KeyBindingText(8);
        }
    }
}

