using UnityEngine;
using System.Collections;

public class Clicker : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool clicked()
	{
		//public static bool anyKeyDown
		return Input.anyKeyDown;
	}
}
