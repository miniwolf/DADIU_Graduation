using UnityEngine;
using System.Collections;
using Assets.scripts.character;
using Assets.scripts.components;
using Assets.scripts.components.registers;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine.UI;


namespace Assets.scripts.level
{
    public class DeathCamTrigger : MonoBehaviour, GameEntity
    {


        public const string Tag = "DeathCamTag"; // tag is not used in unity
        public int deathCamVisibleSeconds = 5;

    /// <summary>
    /// Determines if this trigger object is visible by camera (don't show death cam then) or is in one of {Right, Left}OfCamera sides
    /// </summary>
        private enum TriggerRelativePos
        {
            RightOfCamera, LeftOfCamera, Visible
        }

        private GameObject deathCam ;

        void Awake() {
			deathCam = GameObject.FindGameObjectWithTag(TagConstants.UI.DEATH_CAM);
            HideCameraImmediately();
            InjectionRegister.Register(this);
            UnityEngine.Renderer r = GetComponent<Renderer>();

            Debug.Log(r);
        }

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public string GetTag()
        {
            return Tag;
        }

        public void SetupComponents()
        {
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public Actionable<T> GetActionable<T>()
        {
            return null;
        }

        void OnTriggerEnter(Collider collider)
        {
            // when penguin crosses dangerous zone, display death camera in UI and point it to the penguin
            if (TagConstants.PENGUIN.Equals(collider.gameObject.tag))
            {
                Camera cam = collider.gameObject.GetComponent<Penguin>().GetDeathCam();
                cam.enabled = true;
                deathCam.transform.localScale = Vector3.one;
                cam.targetTexture = (RenderTexture) deathCam.GetComponent<RawImage>().texture;
                StartCoroutine(HideCamera());
            }
        }

        IEnumerator HideCamera()
        {
            yield return new WaitForSeconds(deathCamVisibleSeconds);
			HideCameraImmediately();
        }

		void HideCameraImmediately() {
			deathCam.transform.localScale = Vector3.zero;
		}
    }
}