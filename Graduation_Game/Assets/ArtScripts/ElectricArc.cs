using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ElectricArc : MonoBehaviour {
	public Transform target;
	LineRenderer line;
	public int vertCount = 10;
	public float scale = 1;
	Vector3[] vertexPos;
	[Range(1,50)]
	public int fps = 5;
	float _fps{
		get{return 1f / fps;}
	}


	void Start () {
		line = GetComponent<LineRenderer>();
		line.SetVertexCount(vertCount);
		vertexPos = new Vector3[vertCount];
		StartCoroutine(UpdatePos());
	}

	void SetPoints (){
		transform.LookAt(target);
		Vector3 r = transform.right;
		Vector3 up = transform.up;

		for ( int i = 0; i < vertCount; i++){
			float d = 1f / (vertCount - 1);
			vertexPos[i] = Vector3.Lerp(transform.position, target.position, d*i);
			float xRandom = Random.Range(-1f, 1f);
			float yRandom = Random.Range(-1f, 1f);
			vertexPos[i] += r*xRandom*scale;
			vertexPos[i] += up*yRandom*scale;

		}
		line.SetPositions(vertexPos);


	}

	IEnumerator UpdatePos (){
		while (true){
			SetPoints();
			yield return new WaitForSeconds(_fps);
		}
	}
}