using UnityEngine;
using System.Collections;
using Assets.scripts.character;
using Assets.scripts.components;
using Assets.scripts.components.registers;
using Assets.scripts.controllers.actions;
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
            RightOfCamera,
            LeftOfCamera,
            Visible
        }

        private GameObject deathCamLeft, deathCamRight;

        void Awake()
        {
            deathCamLeft = GameObject.FindGameObjectWithTag(TagConstants.UI.DEATH_CAM_LEFT);
            deathCamRight = GameObject.FindGameObjectWithTag(TagConstants.UI.DEATH_CAM_RIGHT);
            HideCamerasImmediately();

            InjectionRegister.Register(this);
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
                TriggerRelativePos currentPos = GetCurrentPos();

                switch (currentPos)
                {
                    case TriggerRelativePos.Visible: // no DeadCam if user sees the death
                        return;
                    case TriggerRelativePos.LeftOfCamera:
                        break;
                    case TriggerRelativePos.RightOfCamera:
                        break;
                    default:
                        Debug.Log("Fuck");
                        break;
                }

                Camera cam = collider.gameObject.GetComponent<Penguin>().GetDeathCam();
                cam.enabled = true;

                // figure out if object is visible by camera

                if (currentPos == TriggerRelativePos.LeftOfCamera)
                {
					Debug.Log("DeathCam pos: " + deathCamLeft.transform.position);
                    deathCamLeft.transform.localScale = Vector3.one;
                    cam.targetTexture = (RenderTexture) deathCamLeft.GetComponent<RawImage>().texture;
                    StartCoroutine(HideCamera(HideLeftCameraImmediately));
                } else if (currentPos == TriggerRelativePos.RightOfCamera)
                {
					Debug.Log("DeathCam pos: " + deathCamRight.transform.position);
                    deathCamRight.transform.localScale = Vector3.one;
                    cam.targetTexture = (RenderTexture) deathCamRight.GetComponent<RawImage>().texture;
                    StartCoroutine(HideCamera(HideRightCameraImmediately));
                }
            }
        }

        private TriggerRelativePos GetCurrentPos()
        {
            var viewport = Camera.main.WorldToViewportPoint(transform.position);
            Debug.Log("Current viewport: " + viewport);
// x is an offset of how where on the screen from (0,1) is the object displayed. Anything outside this bound is off the screen
            if (viewport.x < 0)
                return TriggerRelativePos.LeftOfCamera;
            if (viewport.x > 1)
                return TriggerRelativePos.RightOfCamera;
            return TriggerRelativePos.Visible;
        }

        IEnumerator HideCamera(System.Action a)
        {
            yield return new WaitForSeconds(deathCamVisibleSeconds);
            a();
        }

        void HideCamerasImmediately()
        {
            HideLeftCameraImmediately();
            HideRightCameraImmediately();
        }

        void HideLeftCameraImmediately()
        {
            HideCamera(deathCamLeft);
        }
        void HideRightCameraImmediately()
        {
            HideCamera(deathCamRight);
        }

        void HideCamera(GameObject o )
        {
            o.transform.localScale  = Vector3.zero;
        }

    }
}