using NoSuchStudio.Common;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace NoSuchStudio.UI.Highlight.Demo {
    public class ControlsSceneControlsManager : NoSuchMonoBehaviour {
        public OptionsSO optionsSO;

        public Text textFadeDuration;
        public Text textPadding;

        public Slider sliderFadeDuration;
        public Slider sliderPadding;
        public Toggle toggleSimple;
        public Toggle toggleSliced;
        public Toggle toggleDismiss;

        private void Start() {
            var options = new Options(optionsSO.options);
            HighlightUI.defaultOptions = options;
            toggleDismiss.SetIsOnWithoutNotify(options.dismissOnClick);
            (options.frameType == Image.Type.Sliced ? toggleSliced : toggleSimple).SetIsOnWithoutNotify(true);
            sliderPadding.value = options.padding.top;
            sliderFadeDuration.value = options.fadeDuration;
        }

        public void OnFadeDurationChange(float v) {
            HighlightUI.defaultOptions.fadeDuration = v;
            textFadeDuration.text = $"{v}";
        }

        public void OnPaddingChange(float v) {
            HighlightUI.defaultOptions.padding.SetAll(v);
            textPadding.text = $"{(int)v}";
        }

        public void OnDismissButton() {
            HighlightUI.Dismiss();
        }

        public void OnFrameTypeSimple(bool v) {
            if (!v) return;
            HighlightUI.defaultOptions.frameType = Image.Type.Simple;
        }

        public void OnFrameTypeSliced(bool v) {
            if (!v) return;
            HighlightUI.defaultOptions.frameType = Image.Type.Sliced;
        }

        public void OnToggleDismiss(bool v) {
            HighlightUI.defaultOptions.dismissOnClick = v;
        }
    }
}