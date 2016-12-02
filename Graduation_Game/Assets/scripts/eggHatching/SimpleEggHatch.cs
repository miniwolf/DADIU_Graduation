using System;
using Assets.scripts.UI.inventory;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.eggHatching {
    public class SimpleEggHatch : MonoBehaviour {

        public Text penguinsCountText, hatchCountText, timerText;

        private int penguinCount, penguinMaxCount;
        private int lastHatchTime; // when user hatched, but when hatchable egg is created
        private int hatchDuration;
        private int timeCount;
        private int hatchableEggs;
        private int maxHatchableEggs;

        void Start() {
            penguinCount = Inventory.penguinCount.GetValue();
            penguinMaxCount = Inventory.penguinStorage.GetValue();
            hatchDuration = Prefs.GetHatchDuration();
            lastHatchTime = Prefs.GetLastHatchTime();
			maxHatchableEggs = penguinMaxCount - penguinCount;
        }

        void Update() {
            UpdateValues();
            UpdateScreen();
        }

        public void HatchEggs() {
            lastHatchTime = Prefs.UpdateLastHatchTime();
            penguinCount += hatchableEggs;
			maxHatchableEggs -= hatchableEggs;
			hatchableEggs = 0;
            Inventory.penguinCount.SetValue(penguinCount);
        }

        private void UpdateScreen() {
            if (penguinsCountText != null) {
                penguinsCountText.text = penguinCount + "/" + penguinMaxCount;
            }

            if (hatchCountText != null) {
                hatchCountText.text = hatchableEggs + "/" + maxHatchableEggs;
            }

            if (timerText != null)
            {
                timerText.text = timeCount + "/";//+ Prefs.GetHatchDuration();
            }

            Debug.Log("Penguins: " + penguinCount + "/" + penguinMaxCount + ", hatchable: " + hatchableEggs + "/" + maxHatchableEggs + ", timer: " + timeCount + "/" + Prefs.GetHatchDuration() );
        }

        private void UpdateValues() {
            var currentTime = DateTimeUtil.Seconds();
            var timeDifference = (currentTime - lastHatchTime);
            hatchableEggs =  timeDifference/ hatchDuration;
            timeCount = Prefs.GetHatchDuration() - timeDifference % Prefs.GetHatchDuration();
            if (hatchableEggs > maxHatchableEggs) hatchableEggs = maxHatchableEggs;
        }

        private void OnDestroy() {

        }
    }
}