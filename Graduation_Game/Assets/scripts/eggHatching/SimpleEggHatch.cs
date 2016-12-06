﻿using System.Collections;
using Assets.scripts.UI.inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.eggHatching {
    public class SimpleEggHatch : MonoBehaviour {

        public Text penguinsCountText, hatchCountTextInPanel, hatchCountTextInMainCanvas, timerText, timerPopupText;
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

            if (maxHatchableEggs <= 0) {
                timerText.gameObject.SetActive(false);
            } else {
                timerText.gameObject.SetActive(true);
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