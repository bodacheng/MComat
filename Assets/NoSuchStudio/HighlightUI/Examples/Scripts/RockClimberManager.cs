using NoSuchStudio.Common;
using UnityEngine;
using UnityEngine.UI;
using NoSuchStudio.UI.Highlight.Extentions;

namespace NoSuchStudio.UI.Highlight.Demo {
    public class RockClimberManager : NoSuchMonoBehaviour {
        
        public OptionsSO optionsSO;

        [SerializeField] RectTransform buttonLines;
        [SerializeField] RectTransform buttonBetOne;
        [SerializeField] RectTransform buttonBetMax;
        [SerializeField] RectTransform buttonDouble;
        [SerializeField] RectTransform buttonAuto;
        [SerializeField] RectTransform buttonStart;
        [SerializeField] RectTransform imagePerson;
        [SerializeField] RectTransform panelGame;

        private bool sawBetHint;
        private bool sawBetStartHint;
        private int timesPlayed;

        public void OnBetOne() {
            LogLog("BetOne");
            if (!sawBetHint) {
                sawBetHint = true;
                buttonStart.GetComponent<Button>().interactable = true;
                HighlightUI.defaultOptions.frameType = Image.Type.Simple;
                HighlightUI.ShowForUI(buttonStart);
                HighlightUIText.Show("<size=24><color=green>Tutorial 4 / 4</color></size>\nPress start to play a round.\n");
            }
        }

        public void OnBetMax() {
            LogLog("BetMax");
            HighlightUI.Dismiss();
        }

        public void OnDouble() {
            LogLog("BetDouble");
            HighlightUI.Dismiss();
        }

        public void OnLines() {
            LogLog("Lines");
            HighlightUI.Dismiss();
        }

        public void OnStart() {
            LogLog("Start");
            if (!sawBetStartHint) {
                sawBetStartHint = true;
                HighlightUI.Dismiss();
            }

            timesPlayed++;
            if (timesPlayed == 3) {
                buttonDouble.GetComponent<Button>().interactable = true;
                HighlightUI.defaultOptions.frameType = Image.Type.Sliced;
                HighlightUI.defaultOptions.dismissOnClick = true;
                HighlightUI.ShowForUI(buttonDouble);
                HighlightUIText.Show("You can bet double from now on!\n");
            }
        }

        public void OnAuto() {
            LogLog("Auto");
            HighlightUI.Dismiss();
        }

        private void OnDismissTutorialOne() {
            HighlightUI.defaultOptions.dismissAction = OnDismissTutorialTwo;
            HighlightUI.ShowForUI(imagePerson);
            HighlightUIText.Show("<size=24><color=green>Tutorial 2 / 4</color></size>\nThis is your avatar.\n<size=12><Tap for next></size>\n");
        }

        private void OnDismissTutorialTwo() {
            buttonBetOne.GetComponent<Button>().interactable = true;
            HighlightUI.defaultOptions.dismissOnClick = false;
            HighlightUI.defaultOptions.dismissAction = null;
            HighlightUI.ShowForUI(buttonBetOne);
            HighlightUIText.Show("<size=24><color=green>Tutorial 3 / 4</color></size>\nMake a bet.\n");
        }

        private void Start() {
            buttonLines.GetComponent<Button>().interactable = false;
            buttonBetMax.GetComponent<Button>().interactable = false;
            buttonAuto.GetComponent<Button>().interactable = false;
            buttonBetOne.GetComponent<Button>().interactable = false;
            buttonStart.GetComponent<Button>().interactable = false;
            buttonDouble.GetComponent<Button>().interactable = false;
            HighlightUI.defaultOptions = new Options(optionsSO.options);

            RunDelayed(1f, () => {
                HighlightUI.defaultOptions.dismissAction = OnDismissTutorialOne;
                HighlightUI.ShowForUI(panelGame);
                HighlightUIText.Show("<size=24><color=green>Tutorial 1 / 4</color></size>\nThis is the game view.\n<size=12><Tap for next></size>\n");
            });
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.X)) {
                ScreenCapture.CaptureScreenshot("screenshot");
            }
        }
    }
}
