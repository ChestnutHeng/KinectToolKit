using UnityEngine;
using System.Collections;

public class level1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ( "Door"))
		{
			Play ();
		}
	}
	void GameStart()
	{
		//Cursor.visible = true;
		Screen.lockCursor = false;
	}
	
	public void Quit()
	{
		Application.Quit();
	}
	
	public void LoadLevel(int _level)
	{
		Application.LoadLevel("Stage" + _level.ToString());
	}
	
	public void Play()
	{
		Application.LoadLevel("Stage2");
	}
}
