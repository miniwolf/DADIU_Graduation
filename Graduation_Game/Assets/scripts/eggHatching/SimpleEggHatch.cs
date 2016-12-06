using System.Collections;
using Assets.scripts.UI.inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.eggHatching {
    public class SimpleEggHatch : MonoBehaviour {

        public Text penguinsCountText, hatchCountTextInPanel, hatchCountTextInMainCanvas, timerText;
        public GameObject hatchEggsPanel;
        public GameObject template;
        public float hatchFeedbackSpeed = .5f;

        private int penguinCount, penguinMaxCount;
        private int lastHatchTime; // when user hatched, but when hatchable egg is created
        private int hatchDuration;
        private int timeCount;
        private int hatchableEggs;
        private int maxHatchableEggs;
//        private GameObject pendingFeedback;

        void Start() {
            penguinCount = Inventory.penguinCount.GetValue();
            penguinMaxCount = Inventory.penguinStorage.GetValue();
            hatchDuration = Prefs.GetHatchDuration();
            lastHatchTime = Prefs.GetLastHatchTime();
			maxHatchableEggs = penguinMaxCount - penguinCount;

            template.gameObject.SetActive(false);
            hatchEggsPanel.SetActive(false);
        }

        void Update() {
            UpdateValues();
            UpdateScreen();
        }

        public void CloseDialog() {
            hatchEggsPanel.SetActive(false);
        }

        public void OpenEggHatchingPanel() {
            if(hatchableEggs > 0)
                hatchEggsPanel.SetActive(true);
        }

        public void HatchEggs() {
            StartCoroutine(VisualFeedback());
        }

        private void UpdateScreen() {
            if (penguinsCountText != null) {
                penguinsCountText.text = penguinCount + "/" + penguinMaxCount;
            }

            if (hatchCountTextInPanel != null) {
                hatchCountTextInPanel.text = hatchableEggs + "/" + maxHatchableEggs;
            }

            if (hatchCountTextInMainCanvas != null) {
                hatchCountTextInMainCanvas.text = hatchableEggs + "x";
            }

            if (timerText != null) {
                timerText.text = timeCount + " " + TranslateApi.GetString(LocalizedString.seconds);//+ "/"+ Prefs.GetHatchDuration();
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
//            if(hatchEggsPanel != null) { // this check is here until EggHatching prefab is added to the main screne
//                hatchEggsPanel.SetActive(false);
//                pendingFeedback = Instantiate(template);
//                pendingFeedback.gameObject.SetActive(true);
//                pendingFeedback.transform.parent = transform;
//
//                Vector3 targetLocation = penguinsCountText.gameObject.transform.position;
//                pendingFeedback.transform.position = hatchCountTextInPanel.transform.position;
//
//                float distance = float.MaxValue;
//                float fraction = 0;
//                Debug.DrawRay(hatchCountTextInPanel.transform.position, targetLocation, Color.red, 10000);
//                while(distance > 1) {
//                    if(fraction < 1) {
//                        fraction += Time.deltaTime * hatchFeedbackSpeed;
//                        pendingFeedback.transform.position = Vector3.Lerp(hatchCountTextInPanel.transform.position, targetLocation, fraction);
//                    }
//                    Debug.Log(
//                        "Fraction: " + fraction +
//                        ", Start position: " + hatchCountTextInPanel.transform.position +
//                        ", Current position: " + pendingFeedback.transform.position +
//                        ", Target position: " + targetLocation);
//
//                    distance = Vector3.Distance(pendingFeedback.transform.position, targetLocation);
//                    yield return new WaitForEndOfFrame();
//                }
//            }

//            pendingFeedback = null;
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