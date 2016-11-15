using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.scripts.components;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.controllers.actions.pickups
{
    public class DespawnPlutonium : Action
    {
        private GameObject gameObject;
        private Text plutoniumCounter;
        private readonly CouroutineDelegateHandler delegator;

        public DespawnPlutonium(CouroutineDelegateHandler d)
        {
            delegator = d;
        }

        public void Setup(GameObject gameObject)
        {
            this.gameObject = gameObject;
            plutoniumCounter =
                GameObject.FindGameObjectWithTag(TagConstants.PLUTONIUM_COUNTER_TEXT).GetComponent<Text>();
        }

        public void Execute()
        {
            delegator.StartCoroutine(FeedbackCoroutine());
        }

        private IEnumerator FeedbackCoroutine()
        {
            Camera c = Camera.main;
            GameObject currencyGameObject = gameObject.transform.parent.gameObject;
            Vector3 counterCanvasPos = c.ScreenToViewportPoint(plutoniumCounter.transform.position);
            Vector3 currencyCanvasPos = currencyGameObject.transform.position;

            float fraction = 0;
            float speed = .1f;

			while (Vector3.Distance(currencyGameObject.transform.position, counterCanvasPos) > 1)
            {
                Debug.Log(counterCanvasPos + "|" + currencyCanvasPos);
                if (fraction < 1)
                {
                    fraction += Time.deltaTime * speed;
                    currencyGameObject.transform.position = Vector3.Lerp(currencyCanvasPos, counterCanvasPos, fraction);
                }

                yield return new WaitForEndOfFrame();
            }

//            currencyGameObject.SetActive(false);
            plutoniumCounter.text = (int.Parse(plutoniumCounter.text) + 1).ToString();
            yield return null;
        }
    }
}