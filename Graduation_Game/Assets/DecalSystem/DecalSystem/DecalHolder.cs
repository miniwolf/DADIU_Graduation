using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.DecalSystem{

	public class DecalHolder : MonoBehaviour {
		
		private GameObject[] affectedObjects;
		private List<Decal> decals = new List<Decal>();
		public Material material;
		public MeshRenderer[] test;
		private GameObject[] go;

		MeshRenderer[] renderers, renders;
		List<MeshRenderer> rend = new List<MeshRenderer>();

		void Start(){
			
			renderers = (MeshRenderer[])GameObject.FindObjectsOfType<MeshRenderer>();
			for (int i = 0; i < renderers.Length; i++) {
				if (renderers[i].enabled) {
					rend.Add(renderers[i]);
				}
			}
			print(renderers.Length);

			renders = rend.ToArray();

			go = GameObject.FindGameObjectsWithTag("ReceiveBlood");
			test = new MeshRenderer[go.Length];
			for(int i=0;i<go.Length;i++){
				test[i] = go[i].GetComponent<MeshRenderer>();
			}

			print(renders.Length);
		}

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
		private GameObject[] GetAffectedObjects(Bounds bounds, LayerMask affectedLayers) {
			List<GameObject> objects = new List<GameObject>();
			//foreach(Renderer r in renders) {
			//	if( !r.enabled ) continue;
			//	if( !IsLayerContains(affectedLayers, r.gameObject.layer) ) continue;
			//	if( r.GetComponent<Decal>() != null ) continue;
			for(int i=0;i<test.Length;i++){
				if( bounds.Intersects(test[i].bounds) ) {
					objects.Add(test[i].gameObject);
				}
			}
			return objects.ToArray();
		}


		private static bool IsLayerContains(LayerMask mask, int layer) {
			return (mask.value & 1<<layer) != 0;
		}
	}
}