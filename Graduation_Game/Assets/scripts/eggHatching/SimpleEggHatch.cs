using System;
using Assets.scripts.UI.inventory;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.eggHatching {
    public class SimpleEggHatch : MonoBehaviour {

        public Text penguinCountText, penguinMaxCountText, hatchCountText, hatchMaxCountText;
        private int penguinCount, penguinMaxCount;

        private int lastHatchTime; // when user hatched, but when hatchable egg is created
        private int hatchDuration;

        private int hatchableEggs;
        private int maxHatchableEggs;

        void Start() {
            penguinCount = Inventory.penguinCount.GetValue();
            penguinMaxCount = Inventory.penguinStorage.GetValue();
            hatchDuration = Prefs.GetHatchDuration();
            lastHatchTime = Prefs.GetLastHatchTime();
        }

        private void Update() {
            Debug.Log("Date: " + DateTimeUtil.Seconds());
            UpdateValues();
            UpdateScreen();
        }

        public void HatchEggs() {
            lastHatchTime = Prefs.UpdateLastHatchTime();
            penguinCount += hatchableEggs;
            Inventory.penguinCount.SetValue(penguinCount);
        }

        private void UpdateScreen() {
            Debug.Log("Penguins: " + penguinCount + "/" + penguinMaxCount + ", hatchable: " + hatchableEggs + "/" + maxHatchableEggs);
        }

        private void UpdateValues() {
            var currentTime = DateTimeUtil.Seconds();
            hatchableEggs = (currentTime - lastHatchTime) / hatchDuration;
            if (hatchableEggs > maxHatchableEggs) hatchableEggs = maxHatchableEggs;
        }

        private void OnDestroy() {

        }
    }
}