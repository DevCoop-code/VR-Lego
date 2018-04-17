using UnityEngine;
using System.Collections;

public class HeadLookWalk : MonoBehaviour 
{
	public float velocity = 0.2f;
	//public bool isWalking = false;

	private CharacterController controller;
	//private Clicker clicker = new Clicker();

	private float h = 0.0f;
	private float v = 0.0f;
	private float j = 0.0f;

	private Transform tr;

	// Use this for initialization
	void Start () 
	{
		tr = GetComponent<Transform> ();
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		h = Input.GetAxis ("Horizontal");
		v = Input.GetAxis ("Vertical");
		j = Input.GetAxis ("Jump");

		controller.Move ( (Camera.main.transform.forward * v + Camera.main.transform.right*h + new Vector3(0,1,0)*j) * velocity - new Vector3(0,0.05f,0));

		/*
		Vector3 moveDirection = Camera.main.transform.forward;
		moveDirection *= velocity * Time.deltaTime;
		moveDirection.y = 0.0f;
		transform.position += moveDirection;
		controller.Move(moveDirection);
		*/
		/*
		if (clicker.clicked ()) 
		{
			isWalking = !isWalking;
		}
		if(isWalking)
			controller.SimpleMove (Camera.main.transform.forward * velocity);
		*/
	}
}
