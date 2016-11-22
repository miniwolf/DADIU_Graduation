using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.UI.screen {
    public abstract class UIController : MonoBehaviour {
        /// <summary>
        /// Helper method for ResolveDependencies() 	/// </summary>
        /// <returns>The text component.</returns>
        /// <param name="tag">Tag.</param>
        protected virtual Text GetTextComponent(string tag) {
            return GameObject.FindGameObjectWithTag(tag).GetComponent<Text>();
        }
    }
}