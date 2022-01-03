using NoSuchStudio.Common;
using UnityEngine;
using UnityEngine.UI;

namespace NoSuchStudio.UI.Highlight.Demo {

    public class ButtonSound : NoSuchMonoBehaviour {

        private Button button;
        private void Awake() {
            button = GetComponent<Button>();
        }

        private void OnEnable() {
            button.onClick.AddListener(OnClick);
        }

        private void OnDisable() {
            button.onClick.RemoveListener(OnClick);
        }

        private void OnClick() {
            var audio = GetComponent<AudioSource>();
            audio.Play();
        }
    }

}