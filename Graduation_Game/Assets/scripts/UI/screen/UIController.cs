using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Assets.scripts.UI.screen {

    public abstract class UIController : MonoBehaviour {

        void Start() {
            ResolveDependencies();
            RefreshText();
        }

        void Destroy() {

        }

        /// /// <summary>
        /// This is called whenever we need to set correct texts in the canvas
        /// (screen is rendered for the first time or we change language)
        /// </summary>
        abstract public void RefreshText();

        /// <summary>
        /// In this callback you should fetch all the views you need to manipulate
        /// </summary>
        abstract public void ResolveDependencies();

        /// <summary>
        /// Helper method for ResolveDependencies() 	/// </summary>
        /// <returns>The text component.</returns>
        /// <param name="tag">Tag.</param>
        virtual protected Text GetTextComponent(string tag) {
            return GameObject.FindGameObjectWithTag(tag).GetComponent<Text>();
        }
    }
}