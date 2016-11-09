using UnityEngine;

namespace Assets.scripts.tools.slope
{
    public class SlopeScript : MonoBehaviour
    {
        void OnCollisionEnter(Collision collision)
        {
            Debug.Log(collision.transform.tag);
        }
    }
}