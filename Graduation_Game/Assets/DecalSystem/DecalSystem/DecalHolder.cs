using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.DecalSystem{

	public class DecalHolder : MonoBehaviour {
		
		private GameObject[] affectedObjects;
		private List<Decal> decals = new List<Decal>();
		public Material material;

		public void StartBuilding(Decal decal){

			StartCoroutine(BuildIt(decal));

		}

		private IEnumerator BuildIt(Decal decal){
			yield return new WaitForSeconds(0.3f);
			BuildDecal(decal);

		}
		private void BuildDecal(Decal decal){
			MeshFilter filter = decal.GetComponent<MeshFilter>();
			if(filter == null) filter = decal.gameObject.AddComponent<MeshFilter>();
			if(decal.GetComponent<Renderer>() == null) decal.gameObject.AddComponent<MeshRenderer>();
			decal.GetComponent<Renderer>().material = decal.material;

			if(decal.material == null || decal.sprite == null) {
				filter.mesh = null;
				return;
			}

			affectedObjects = GetAffectedObjects(decal.GetBounds(),decal.affectedLayers);
			foreach(GameObject go in affectedObjects) {
				DecalBuilder.BuildDecalForObject( decal, go );
			}
			DecalBuilder.Push( decal.pushDistance );

			Mesh mesh = DecalBuilder.CreateMesh();
			if(mesh != null) {
				mesh.name = "DecalMesh";
				filter.mesh = mesh;
			}
		}
		private static GameObject[] GetAffectedObjects(Bounds bounds, LayerMask affectedLayers) {
			MeshRenderer[] renderers = (MeshRenderer[]) GameObject.FindObjectsOfType<MeshRenderer>();
			List<GameObject> objects = new List<GameObject>();
			foreach(Renderer r in renderers) {
				if( !r.enabled ) continue;
				if( !IsLayerContains(affectedLayers, r.gameObject.layer) ) continue;
				if( r.GetComponent<Decal>() != null ) continue;

				if( bounds.Intersects(r.bounds) ) {
					objects.Add(r.gameObject);
				}
			}
			return objects.ToArray();
		}


		private static bool IsLayerContains(LayerMask mask, int layer) {
			return (mask.value & 1<<layer) != 0;
		}
	}
}