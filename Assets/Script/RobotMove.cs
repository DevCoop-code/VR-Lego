using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RobotMove : MonoBehaviour 
{
	public GameObject MovePointObject;
	private NavMeshAgent nvAgent;
	public Transform ControlUI;
	Transform camera;

	// Use this for initialization
	void Start () 
	{
		nvAgent = this.gameObject.GetComponent<NavMeshAgent> ();
		camera = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		nvAgent.destination = MovePointObject.transform.position;

		ControlUI.LookAt (camera.position);
		ControlUI.Rotate (0.0f, 180.0f, 0.0f);
	}
}