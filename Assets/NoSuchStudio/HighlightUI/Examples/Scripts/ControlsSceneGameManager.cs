using NoSuchStudio.Common;
using UnityEngine;
using UnityEngine.UI;
using NoSuchStudio.UI.Highlight.Extentions;
namespace NoSuchStudio.UI.Highlight.Demo {
    public class ControlsSceneGameManager : NoSuchMonoBehaviour {

        [SerializeField] Text textTitle;
        [SerializeField] Button buttonMiddle;
        [SerializeField] Transform cube;

        public void OnHighlightTitle() {
            HighlightUI.ShowForUI(textTitle.transform as RectTransform);
            HighlightUIText.Show("Highlight title.");
        }

        public void OnHighlightMidButton() {
            HighlightUI.ShowForUI(buttonMiddle.transform as RectTransform);
            HighlightUIText.Show("Highlight middle button.");
        }

        public void OnHighlightRandom() {
            int n = 8;
            float x = Random.Range(1, n) * Screen.width / n;
            float y = Random.Range(1, n) * Screen.height / n;
            HighlightUI.ShowForFixedRect(new Rect(x, y, Screen.width / 8, Screen.height / 8));
            HighlightUIText.Show("Highlight random rect.");
        }

        public void OnHighlightCube() {
            HighlightUI.ShowFor3DObject(cube);
            HighlightUIText.Show("Highlight cube.");
        }
    }
}