using UnityEngine;
using System.Collections;
using Assets.scripts;
using Assets.scripts.character;
using Assets.scripts.level;
using System.Collections.Generic;
using Assets.scripts.UI;
using Assets.scripts.components.registers;
using Assets.scripts.components;

public class SealSpawn : MonoBehaviour, GameEntity, SetSnappingTool {

	public int pathPoints = 3;
	private Vector3[] path;
	private GameObject[] penguinSpawnerGO;
	private GameObject theSeal;
	private const int layermask = 1 << 8;
	public AnimationCurve animCurve;
	private Keyframe[] keys = new Keyframe[100];
	private Vector3 midPoint;
	private Vector3 moving;
	public bool haveReactivatedPenguins = false, hasBeenActivated = false;
	private float startTime, journeyLength;
	public float speedFactor = 5f;
	private CharacterController chrController;
	private List<PenguinSpawner> penguinSpawner = new List<PenguinSpawner>();
	private List<GameObject> penguins = new List<GameObject>();
	private InputManager inputManager;
	private Camera cam;

	void Awake(){
		InjectionRegister.Register(this);
	}

	void Start(){
		cam = Camera.main;

		startTime = Time.time;

		theSeal = GetComponentInChildren<Penguin>().gameObject; //add how it moves here
		theSeal.SetActive(true);

		FindPath();

		journeyLength = Vector3.Distance(path[0], path[1]);
		chrController = theSeal.GetComponent<CharacterController>();
		chrController.enabled = false;


		penguinSpawnerGO = GameObject.FindGameObjectsWithTag(TagConstants.PENGUIN_SPAWNER);
		for(int i=0;i<penguinSpawnerGO.Length;i++){
			penguinSpawner.Add(penguinSpawnerGO[i].GetComponent<PenguinSpawner>());
		}

	}

	void Update(){
		if (theSeal.GetComponent<Penguin>().IsDead() && !haveReactivatedPenguins) {
			inputManager.UnblockCameraMovement();
			StartPenguins();
			haveReactivatedPenguins = true;
		}
	}


	IEnumerator MoveTheSeal(){
		StopPenguins();

		Vector3 moveTo = new Vector3(theSeal.transform.position.x, cam.transform.position.y, cam.transform.position.z);
		Vector3 startPosCam = cam.transform.position;


		startTime = Time.time;
		float distCovered = (Time.time - startTime)*speedFactor;
		float fracJourney = distCovered / journeyLength;
		//print(path[0] + " " + path[1]);
		while(fracJourney<0.85f){
			distCovered = (Time.time - startTime)*speedFactor;
			fracJourney = distCovered / journeyLength;
			theSeal.transform.position = Vector3.Lerp(path[0], path[1], fracJourney);
			cam.transform.position = Vector3.Lerp(startPosCam, moveTo, fracJourney+0.15f);
			yield return new WaitForEndOfFrame();
		}
		path[1] = theSeal.transform.position;
		startTime = Time.time;
		float ndistCovered = (Time.time - startTime)*speedFactor;
		journeyLength = Vector3.Distance(path[1], path[2]);
		float nfracJourney = ndistCovered / journeyLength;
		while(nfracJourney<1){
			ndistCovered = (Time.time - startTime)*speedFactor;
			nfracJourney = ndistCovered / journeyLength;
			theSeal.transform.position = Vector3.Lerp(path[1], path[2], nfracJourney);
			yield return new WaitForEndOfFrame();
		}
		chrController.enabled = true;
		theSeal.GetComponent<Penguin>().SetDirection(new Vector3(1, 0, 0));
	}

	private void FindPath(){
		RaycastHit hit;
		if(!Physics.Raycast(new Vector3(theSeal.transform.position.x, 20f, transform.position.z),-Vector3.up,out hit,40f,layermask)){
			return;
		}
		path = new Vector3[pathPoints];
		path[pathPoints - 1] = new Vector3(hit.point.x,hit.point.y+0.5f,hit.point.z);
		path[0] = theSeal.transform.position;
		midPoint = new Vector3(theSeal.transform.position.x, hit.point.y+2f, transform.position.z-(transform.position.z - theSeal.transform.position.z) / 2);
		path[1] = midPoint; 
		/*for (int i = 1; i < pathPoints - 2; i++) {
			path[i].position = 
		}*/
	}

	private void StopPenguins(){
		for (int u = 0; u < penguinSpawner.Count; u++) {
			penguins = penguinSpawner[u].GetAllPenguins();
			for (int i = 0; i < penguins.Count; i++) {
				penguins[i].GetComponent<Penguin>().ExecuteAction(Assets.scripts.controllers.ControllableActions.Stop);
			}
		}
	}

	private void StartPenguins(){
		for (int u = 0; u < penguinSpawner.Count; u++) {
			penguins = penguinSpawner[u].GetAllPenguins();
			for (int i = 0; i < penguins.Count; i++) {
				penguins[i].GetComponent<Penguin>().ExecuteAction(Assets.scripts.controllers.ControllableActions.Start);
			}
		}
	}


	protected void OnTriggerEnter(Collider other){
		if (other.transform.tag != TagConstants.PENGUIN) {
			return;
		}
		if (!hasBeenActivated) {
			inputManager.BlockCameraMovement();
			hasBeenActivated = true;
			StartCoroutine(MoveTheSeal());
		}
	}
		
	public void SetSnap (SnappingToolInterface snapTool) {
		return;
	}

	public void SetInputManager (InputManager inputManage) {
		this.inputManager = inputManage;
	}

	public string GetTag () {
		return TagConstants.SEAL_SPAWN;
	}

	public void SetupComponents () {
	}

	public GameObject GetGameObject () {
		return gameObject;
	}

	public Actionable<T> GetActionable<T> () {
		return null;
	}
}

