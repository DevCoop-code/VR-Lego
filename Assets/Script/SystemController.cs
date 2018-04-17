using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class SystemController : MonoBehaviour 
{
	private GameObject currentButton;

	Transform camera;
	Ray ray;
	RaycastHit hit;
	GameObject hitButton = null;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		camera = Camera.main.transform;
		ray = new Ray (camera.position, camera.rotation * Vector3.forward);
		hitButton = null;
		PointerEventData data = new PointerEventData (EventSystem.current);

		if (Physics.Raycast (ray, out hit)) 
		{
			if (hit.transform.gameObject.tag == "Button") 
			{
				hitButton = hit.transform.parent.gameObject;
			}
		}

		if (currentButton != hitButton) 
		{
			if (currentButton != null) 
			{
				ExecuteEvents.Execute<IPointerExitHandler> (currentButton, data, ExecuteEvents.pointerExitHandler);
			}
			currentButton = hitButton;
			if (currentButton != null) 
			{
				ExecuteEvents.Execute<IPointerEnterHandler> (currentButton, data, ExecuteEvents.pointerEnterHandler);
			}
		}
	}
}
