using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBlood : MonoBehaviour {

	List<ParticleCollisionEvent> list;
	public ParticleSystem part;

	public Texture[] materials;


	void Start(){
		list = new List<ParticleCollisionEvent>();
	}


	void OnParticleCollision(GameObject other){
		int numCol = part.GetCollisionEvents(other, list);
		int i = 0;
		print(numCol);
		while(i<numCol){
			Blood(list[i].intersection,list[i].normal,list[i].colliderComponent);
			/*if (list[i].intersection != null) {
				//Destroy(list[i].);
				list.Remove(list[i]);
			}*/
			i++;
		}

	}

	void OnTriggerEnter(Collider other){
		print(other.name);
	}

	public GameObject drip;
	GameObject splatter;


	void Blood(Vector3 point, Vector3 normal, Component col){
		
		splatter = Instantiate (drip, point + (normal * 0.1f), Quaternion.FromToRotation (Vector3.up, normal));
		splatter.transform.parent = col.transform;
		splatter.GetComponent<MeshRenderer>().material.mainTexture = materials[Random.Range(0, materials.Length)];

	var scaler = Random.value;
	splatter.transform.localScale *= scaler;
	//splatter.transform.localScale.z *= scaler;

	var rater = Random.Range (0, 359);
		splatter.transform.RotateAround (point, normal, rater);
	}
}