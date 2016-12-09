using System.Collections;
using Assets.scripts.UI.inventory;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.camera;

namespace Assets.scripts.eggHatching {
    public class SimpleEggHatch : MonoBehaviour {

        public Text hatchCountTextInPanel, hatchCountTextInMainCanvas, timerText, timerPopupText;
        public GameObject hatchEggsPanel;
        public GameObject template;
        public float hatchFeedbackSpeed = .5f;

        private int penguinCount, penguinMaxCount;
        private int lastHatchTime; // when user hatched, but when hatchable egg is created
        private int hatchDuration;
        private int timeCount;
        private int hatchableEggs;
        private int maxHatchableEggs;
		private MainCameraFreeMove cameraMove;
		//        private GameObject pendingFeedback;

		void Start() {
            penguinCount = Inventory.penguinCount.GetValue();
            penguinMaxCount = Inventory.penguinStorage.GetValue();
            hatchDuration = Prefs.GetHatchDuration();
            lastHatchTime = Prefs.GetLastHatchTime();
			maxHatchableEggs = penguinMaxCount - penguinCount;

            template.gameObject.SetActive(false);
            hatchEggsPanel.SetActive(false);
			cameraMove = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<MainCameraFreeMove>();
		}

        void Update() {
            UpdateValues();
            UpdateScreen();
        }

        public void CloseDialog() {
            hatchEggsPanel.SetActive(false);
			cameraMove.popUpOn = false;
		}

        public void OpenEggHatchingPanel() {
			AkSoundEngine.PostEvent("button_pressed", gameObject);
			if (hatchableEggs > 0) {
				hatchEggsPanel.SetActive(true);
				cameraMove.popUpOn = true;
			}
		}

        public void HatchEggs() {
			if (hatchableEggs > 1) {
				AkSoundEngine.PostEvent("egg_hatch_multiple", gameObject);
			}
			else {
				AkSoundEngine.PostEvent("egg_hatch_single", gameObject);
			}
			StartCoroutine(VisualFeedback());
        }

        private void UpdateScreen() {
          /*  if (penguinsCountText != null) {
                penguinsCountText.text = penguinCount + "/" + penguinMaxCount;
            }*/

            if (hatchCountTextInPanel != null) {
                hatchCountTextInPanel.text = "+" + hatchableEggs + "x";
            }

            if (maxHatchableEggs <= 0 || hatchableEggs >= maxHatchableEggs) {
                if (timerText != null)
                    timerText.gameObject.SetActive(false);
                if (timerPopupText != null)
                    timerPopupText.gameObject.SetActive(false);
            } else {
                if (timerText != null)
                    timerText.gameObject.SetActive(true);
                if (timerPopupText != null)
                    timerPopupText.gameObject.SetActive(true);
            }

            if (hatchCountTextInMainCanvas != null) {
                hatchCountTextInMainCanvas.text = hatchableEggs + "x";
            }
            var txt = timeCount + " " + TranslateApi.GetString(LocalizedString.seconds);
            if (timerText != null) {
                timerText.text = txt;
            }

            if (timerPopupText != null) {
                timerPopupText.text = txt;
            }

//            Debug.Log("Penguins: " + penguinCount + "/" + penguinMaxCount + ", hatchable: " + hatchableEggs + "/" + maxHatchableEggs + ", timer: " + timeCount + "/" + Prefs.GetHatchDuration() );
        }

        private void UpdateValues() {
            var currentTime = DateTimeUtil.Seconds();
            var timeDifference = (currentTime - lastHatchTime);
            hatchableEggs =  timeDifference/ hatchDuration;
            timeCount = Prefs.GetHatchDuration() - timeDifference % Prefs.GetHatchDuration();
            if (hatchableEggs > maxHatchableEggs) hatchableEggs = maxHatchableEggs;
        }

        private IEnumerator VisualFeedback() {
			CloseDialog();
            lastHatchTime = Prefs.UpdateLastHatchTime();
            penguinCount += hatchableEggs;
            maxHatchableEggs -= hatchableEggs;
            hatchableEggs = 0;
            Inventory.penguinCount.SetValue(penguinCount);
            yield return null;
        }
    }
}