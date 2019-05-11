using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

	public GameObject buttonsMenu;
	public GameObject buttonsExit;
	public GameObject buttonsSettings;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowExitButtons(){
		buttonsMenu.SetActive(false);
		buttonsSettings.SetActive(false);
		buttonsExit.SetActive(true);
	}

	public void BackInMenu(){
		buttonsMenu.SetActive(true);
		buttonsExit.SetActive(false);
		buttonsSettings.SetActive(false);
	}

	public void ShowSettings(){
		buttonsMenu.SetActive(false);
		buttonsExit.SetActive(false);
		buttonsSettings.SetActive(true);
	}

	public void ExitGame(){
		Application.Quit();
	}

  public void NewGame(){
		Application.LoadLevel("mir");
	}
}
