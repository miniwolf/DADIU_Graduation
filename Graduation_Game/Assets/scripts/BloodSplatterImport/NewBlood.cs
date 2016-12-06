﻿using System.Collections;
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
		while(i<numCol){
			Blood(list[i].intersection,list[i].normal,list[i].colliderComponent);
			/*if (list[i].intersection != null) {
				//Destroy(list[i].);
				list.Remove(list[i]);
			}*/
			i++;
		}

	}

	public GameObject drip;
	GameObject splatter;


	void Blood(Vector3 point, Vector3 normal, Component col){
		Debug.DrawRay(point, normal);
		splatter = Instantiate (drip, point + (normal * 0.1f), Quaternion.FromToRotation (Vector3.up, normal));
		splatter.transform.parent = col.transform;
		splatter.GetComponent<MeshRenderer>().material.mainTexture = materials[Random.Range(0, materials.Length)];
		//splatter.transform.localRotation = Quaternion.Euler(new Vector3(90f, 0, 0));


	var scaler = Random.value;
	splatter.transform.localScale *= scaler;
	//splatter.transform.localScale.z *= scaler;

	var rater = Random.Range (0, 180);
		splatter.transform.RotateAround (point, normal, rater);
		//splatter.transform.rotation = Quaternion.Euler(new Vector3(Vector3.Cross(normal, splatter.transform.forward).x,Vector3.Cross(normal, splatter.transform.forward).y + Random.Range(100f,180f),Vector3.Cross(normal, splatter.transform.forward).z));
		//splatter.transform.localRotation = Quaternion.Euler(new Vector3(90f, 0, 0));
	}
}