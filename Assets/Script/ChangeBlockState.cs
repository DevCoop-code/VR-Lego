using UnityEngine;
using System.Collections;

public class ChangeBlockState : MonoBehaviour 
{
	bool CurrentState = false;
	private float time = 1.0f;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		time -= Time.deltaTime;
		if(time < 0.0f)
			this.gameObject.GetComponent<Renderer> ().material.color = Color.white;
		
	}

	void ChangeColor(bool _C_Mode)
	{
		CurrentState = _C_Mode;
		time = 1.0f;
		if (CurrentState) 
		{
			this.gameObject.GetComponent<Renderer> ().material.color = Color.red;
		}
	}
}
