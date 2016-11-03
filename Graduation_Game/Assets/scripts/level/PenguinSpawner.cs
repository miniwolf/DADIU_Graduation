using System.Collections;
using Assets.scripts.components.registers;
using UnityEngine;

namespace Assets.scripts.level {
    public class PenguinSpawner : MonoBehaviour {
        [Tooltip("How many penguins should spawn")]
        public int penguinCount = 3;

        [Tooltip("Time between penguins")]
        public float countTime = 3;

        private GameObject penguinObject;

        public void Start() {
            for ( var i = 0; i < transform.childCount; i++ ) {
                var child = transform.GetChild(i);
                if ( child.tag != TagConstants.PENGUIN_TEMPLATE ) {
                    continue;
                }
                penguinObject = child.gameObject;
                break;
            }
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn() {
            // Create an instance of the penguin at the objects position
            while ( penguinCount-- > 0 ) {
                var go = (GameObject) Instantiate(penguinObject, transform.position, Quaternion.identity);
                go.SetActive(true);
                InjectionRegister.Redo();
                yield return new WaitForSeconds(countTime);
            }
        }
    }
}