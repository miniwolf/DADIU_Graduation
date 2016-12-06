using UnityEngine;
using System.Collections.Generic;
using Assets.scripts;

namespace Assets.DecalSystem{

	[RequireComponent( typeof(MeshFilter) )]
	[RequireComponent( typeof(MeshRenderer) )]


	public class Decal : MonoBehaviour {

		//DecalEditor builder = new DecalEditor();

		public Material material;
		public Sprite sprite;
		public Sprite[] texja;

		public float maxAngle = 90.0f;
		public float pushDistance = 0.009f;
		public LayerMask affectedLayers = -1;
		private GameObject[] affectedObjects;
		private List<GameObject> gos = new List<GameObject>();


		void Start(){
			texja = Resources.LoadAll<Sprite>("blood.psd");

			GameObject.FindGameObjectWithTag("Seal").GetComponent<DecalHolder>().StartBuilding(this);
			material = GameObject.FindGameObjectWithTag("Seal").GetComponent<DecalHolder>().material;
		}

	/*	void OnDrawGizmosSelected() {
			Gizmos.matrix = transform.localToWorldMatrix;
			Gizmos.DrawWireCube( Vector3.zero, Vector3.one );
		}
*/
		public Bounds GetBounds() {
			Vector3 size = transform.lossyScale;
			Vector3 min = -size/2f;
			Vector3 max =  size/2f;

			Vector3[] vts = new Vector3[] {
				new Vector3(min.x, min.y, min.z),
				new Vector3(max.x, min.y, min.z),
				new Vector3(min.x, max.y, min.z),
				new Vector3(max.x, max.y, min.z),

				new Vector3(min.x, min.y, max.z),
				new Vector3(max.x, min.y, max.z),
				new Vector3(min.x, max.y, max.z),
				new Vector3(max.x, max.y, max.z),
			};

			for(int i=0; i<8; i++) {
				vts[i] = transform.TransformDirection( vts[i] );
			}

			min = max = vts[0];
			foreach(Vector3 v in vts) {
				min = Vector3.Min(min, v);
				max = Vector3.Max(max, v);
			}
			SetAffectedObjects();
			return new Bounds(transform.position, max-min);
		}

		public MeshRenderer[] GetAffectedObjects(){
			List<MeshRenderer> mesh = new List<MeshRenderer>();
			for (int i = 0; i < gos.Count; i++) {
				if (gos[i].GetComponent<MeshRenderer>() == null || gos[i].GetComponent<MeshFilter>() == null) {
					gos.Remove(gos[i]);
				} else {
					mesh.Add(gos[i].GetComponent<MeshRenderer>());
				}
			}
			return mesh.ToArray();
		}

		private void SetAffectedObjects(){
			RaycastHit[] hit;
			hit = Physics.SphereCastAll(transform.position,0.01f, Vector3.up, 1f);
			for(int i=0;i<hit.Length;i++){
				if (hit[i].transform.gameObject.GetComponent<MeshFilter>() == null || hit[i].transform.gameObject.GetComponent<MeshRenderer>() == null) {
					return;
				}
				//print(hit[i].transform.gameObject.name);
				gos.Add(hit[i].transform.gameObject);
			}	

		}


		/*void OnTriggerStay(Collider other){
			
			if (gos.Contains(other.gameObject) || other.GetComponent<MeshRenderer>()==null || other.GetComponent<MeshFilter>()==null) {
				return;
			}
			gos.Add(other.gameObject);
			print(other.gameObject.name);
//			Debug.Log("hej");
//			affectedObjects = 
		}*/
	}


}