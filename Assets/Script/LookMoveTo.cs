using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class LookMoveTo : MonoBehaviour 
{
	public GameObject ground;

	Transform camera;
	Ray ray;
	RaycastHit[] hits;
	GameObject hitObject;
	RaycastHit hit;

	int Intpoint_X=0, Intpoint_Y=0,Intpoint_Z=0;
	float Xpoint, Ypoint, Zpoint;

	float DirX=0, DirY=0, DirZ=0;

	Vector3 LocateCubePosition;

	Vector3 LocateBlockPosition;
	Transform LocateBlockTrans;
	public GameObject CubeBlock;

	string mode;

	Renderer rend;

	bool _C_Mode;

	private GameObject AddButton, DeleteButton, MoveButton;

	GameObject hitButton = null;
	GameObject currentButton = null;

	GameObject ToMovePosObj = null;

	void Start () 
	{
		camera = Camera.main.transform;
		LocateBlockTrans = CubeBlock.GetComponent<Transform> ();
		mode = "None";

		rend = GetComponent<Renderer> ();
		_C_Mode = false;

		AddButton		= GameObject.Find ("AddButton");
		DeleteButton	= GameObject.Find ("DeleteButton");
		MoveButton 		= GameObject.Find ("MoveButton");
	}
		
	void Update () 
	{
		rend.enabled = true;

		Debug.DrawRay (camera.position, camera.rotation * Vector3.forward * 100.0f, Color.green);
		ray = new Ray (camera.position, camera.rotation * Vector3.forward);

		if (mode == "None") 
		{
			rend.enabled = false;
		}
		if (mode == "Add") 
		{
			if (Physics.Raycast (ray, out hit)) 
			{
				hitObject = hit.collider.gameObject;
				Vector3 TestTrans = hit.point - hitObject.transform.position;

				if (hitObject.tag == "Floor") 
				{
					Intpoint_X = (int)hit.point.x;
					Intpoint_Z = (int)hit.point.z;
					Intpoint_Y = (int)hit.point.y;

					if (hit.point.x >= 0.0f)
						Xpoint = (float)Intpoint_X + 0.5f;
					else
						Xpoint = (float)Intpoint_X - 0.5f;
					if (hit.point.z >= 0.0f)
						Zpoint = (float)Intpoint_Z + 0.5f;
					else
						Zpoint = (float)Intpoint_Z - 0.5f;
					Ypoint = (float)Intpoint_Y + 0.5f;

					hitObject = hit.collider.gameObject;

					LocateCubePosition = new Vector3 (Xpoint, Ypoint, Zpoint);

					transform.position = LocateCubePosition;
				}

				if (hitObject.tag == "Block") 
				{
					if (TestTrans.x == 0.5f || TestTrans.x == -0.5f) 
					{
						DirX = TestTrans.x * 2;
						DirY = 0;
						DirZ = 0;
					}
					if (TestTrans.y == 0.5f || TestTrans.y == -0.5f) 
					{
						DirX = 0;
						DirY = TestTrans.y * 2;
						DirZ = 0;
					}
					if (TestTrans.z == 0.5f || TestTrans.z == -0.5f) 
					{
						DirX = 0;
						DirY = 0;
						DirZ = TestTrans.z * 2;
					}

					LocateCubePosition = new Vector3 (hitObject.transform.position.x + DirX, hitObject.transform.position.y + DirY, hitObject.transform.position.z + DirZ);

					transform.position = LocateCubePosition;
				}
			}

			if (Input.GetButtonDown ("Fire3") && hitObject.tag == "Floor") 
			{
				GenerateBlockFromFloor ();
			}
			if (Input.GetButtonDown ("Fire3") && hitObject.tag == "Block") 
			{
				GenerateBlockFromBlock ();
			}
		}
		if (mode == "Delete") 
		{
			rend.enabled = false;

			if (Physics.Raycast (ray, out hit)) 
			{
				hitObject = hit.collider.gameObject;

				if (hitObject.tag == "Block") 
				{
					//Debug.Log ("Change Color");
					_C_Mode = true;
					hitObject.SendMessage ("ChangeColor", _C_Mode, SendMessageOptions.DontRequireReceiver);
				}
			}
			if (Input.GetButtonDown ("Fire3") && hitObject.tag == "Block")
				Destroy (hit.collider.gameObject);
		}
		/*
		if (mode == "Move") 
		{
			rend.enabled = false;

			if (Physics.Raycast (ray, out hit)) 
			{
				hitObject = hit.collider.gameObject;

				if (hitObject.tag == "Block") 
				{
					hitObject.transform.position = ToMovePosObj.transform.position;
				}
			}
		}
		*/

		PointerEventData data = new PointerEventData (EventSystem.current);

		if(Physics.Raycast(ray, out hit))
		{
			if (hit.transform.gameObject.tag == "Button") 
			{
				hitButton = hit.transform.parent.gameObject;
			} 
		}
		try
		{
			if (hitButton.tag == "AddBtn") 
			{
				mode = "Add";
			}
			if (hitButton.tag == "DelBtn") 
			{
				mode = "Delete";
			}
			if (hitButton.tag == "MoveBtn") 
			{
				mode = "Move";
			}
		} 
		catch(NullReferenceException ex) 
		{
			Debug.Log ("hitButton has null");
		}
	}

	void GenerateBlockFromFloor()
	{
		LocateBlockPosition = new Vector3 (Xpoint, Ypoint, Zpoint);
		LocateBlockTrans.position = LocateBlockPosition;
		Instantiate (CubeBlock, LocateBlockTrans.position, LocateBlockTrans.rotation);
	}

	void GenerateBlockFromBlock ()
	{
		LocateBlockPosition = new Vector3 (hitObject.transform.position.x+DirX,hitObject.transform.position.y+DirY,hitObject.transform.position.z+DirZ);
		LocateBlockTrans.position = LocateBlockPosition;
		Instantiate (CubeBlock, LocateBlockTrans.position, LocateBlockTrans.rotation);		
	}
}