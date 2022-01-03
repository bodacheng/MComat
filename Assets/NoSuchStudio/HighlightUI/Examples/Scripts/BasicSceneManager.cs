using NoSuchStudio.Common;
using UnityEngine;
using UnityEngine.UI;
using NoSuchStudio.UI.Highlight.Extentions;
namespace NoSuchStudio.UI.Highlight.Demo {
    public class BasicSceneManager : NoSuchMonoBehaviour {

        public OptionsSO optionsSO;

        [SerializeField] Text textTitle;
        [SerializeField] Button buttonMiddle;
        [SerializeField] Transform cube;

        private void Start() {
            HighlightUI.defaultOptions = new Options(optionsSO.options);
        }

        public void OnHighlightTitle() {
            HighlightUI.ShowForUI(textTitle.transform as RectTransform);
        }

        public void OnHighlightMidButton() {
            HighlightUI.ShowForUI(buttonMiddle.transform as RectTransform);
        }

        public void OnHighlightCube() {
            HighlightUI.ShowFor3DObject(cube);
        }
    }
}