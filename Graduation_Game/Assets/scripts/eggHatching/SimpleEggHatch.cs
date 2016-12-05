using System.Collections;
using Assets.scripts.UI.inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.eggHatching {
    public class SimpleEggHatch : MonoBehaviour {

        public Text penguinsCountText, hatchCountText, timerText;
        public GameObject hatchEggsPanel;

        private int penguinCount, penguinMaxCount;
        private int lastHatchTime; // when user hatched, but when hatchable egg is created
        private int hatchDuration;
        private int timeCount;
        private int hatchableEggs;
        private int maxHatchableEggs;
        private ParticleSystem template;
        private ParticleSystem pendingFeedback;

        void Start() {
            penguinCount = Inventory.penguinCount.GetValue();
            penguinMaxCount = Inventory.penguinStorage.GetValue();
            hatchDuration = Prefs.GetHatchDuration();
            lastHatchTime = Prefs.GetLastHatchTime();
			maxHatchableEggs = penguinMaxCount - penguinCount;

            template = GetComponentInChildren<ParticleSystem>();
            template.gameObject.SetActive(false);
            hatchEggsPanel.SetActive(false);
        }

        void Update() {
            UpdateValues();
            UpdateScreen();
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

            if (hatchCountText != null) {
                hatchCountText.text = hatchableEggs + "/" + maxHatchableEggs;
            }

            if (timerText != null) {
                timerText.text = ""+timeCount ;//+ "/"+ Prefs.GetHatchDuration();
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
            hatchEggsPanel.SetActive(false);
            pendingFeedback = Instantiate(template);
            pendingFeedback.gameObject.SetActive(true);

            GameObject targetLocation = penguinsCountText.gameObject;
            pendingFeedback.transform.position = hatchCountText.transform.position;

            float distance = float.MaxValue;
            float fraction = 0;
            float speed = .3f;
//            Debug.DrawRay(hatchCountText.transform.position, targetLocation.transform.position, Color.red, 10000);
            while(distance > 1) {
                if(fraction < 1) {
                    fraction += Time.deltaTime * speed;
                    pendingFeedback.transform.position = Vector3.Lerp(hatchCountText.transform.position, targetLocation.transform.position, fraction);
                }
//                Debug.Log(
//                    "Fraction: " + fraction +
//                    ", Start position: " + Camera.main.ScreenToWorldPoint(hatchCountText.transform.position) +
//                    ", Current position: " + pendingFeedback.transform.position +
//                    ", Target position: " + targetLocation.transform.position);

                distance = Vector3.Distance(pendingFeedback.transform.position, targetLocation.transform.position);
                yield return new WaitForEndOfFrame();
            }

            pendingFeedback = null;
            lastHatchTime = Prefs.UpdateLastHatchTime();
            penguinCount += hatchableEggs;
            maxHatchableEggs -= hatchableEggs;
            hatchableEggs = 0;
            Inventory.penguinCount.SetValue(penguinCount);
            yield return null;
        }
    }
}