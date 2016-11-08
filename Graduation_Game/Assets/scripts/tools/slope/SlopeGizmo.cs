using UnityEngine;
using System.Collections;

public class SlopeGizmo : MonoBehaviour
{
//    public AnimationCurve slopeCurve;
    private AnimationCurve slopeCurve;

    void Start ()
    {
       slopeCurve = GetComponent<SlopeGenerator>().slopeCurve;
    }
	
    public virtual void OnDrawGizmos()

    {
//        Gizmos.color = Color.blue;
//        Gizmos.DrawLine(new Vector3(0, 0, 0), new Vector3(200,200,200));
//
//        int length = 100;
//
////        Debug.Log("Slope curve: " + slopeCurve.length);
//
//        for (int x = 0; x < length; x++)
//        {
//            float pos = x / (float) length;
//            float y = length * slopeCurve.Evaluate(pos);
//
//            Vector3 start  = new Vector3(x,y , 0);
//            Vector3 end = new Vector3(x + 1, length * slopeCurve.Evaluate((x + 1) / (float) length), 0f);
////            Debug.Log("Start: " + start  + " end: " + end + " pos: "+ pos + " y: " + y);
////            Gizmos.DrawLine(start, end);
//
////            if (x % 5 == 0)
//            {
//                Gizmos.DrawCube(start, Vector3.one / 3);
//                start.z = start.z + 2;
//                Gizmos.DrawCube(start, Vector3.one / 3);
//            }
//        }

    }
}
