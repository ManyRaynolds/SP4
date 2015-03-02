using UnityEngine;
using System.Collections;
using System;

public class Registration : MonoBehaviour {
	
	string url = "";
	public string loginURL = "";
	public bool buttonpress = false;
	
	//string loginURL = "file:///C|/xampp/htdocs/login.php";
	
	string Username = "";
	string Password = "";
	string Password2 = "";
	string label = "";

	// Use this for initialization
	void Start () {
		url = Network.player.ipAddress;
	}

	void OnGUI()
	{
		GUI.Window(0, new Rect(Screen.width / 4, Screen.height /3, Screen.width/2, Screen.height/100*52), LoginWindow, "");
	}
	
	void LoginWindow(int WindowID)
	{
		GUI.Label (new Rect (Screen.width/100*22, Screen.height/100*3, Screen.width, Screen.height), "===Username===");
		Username = GUI.TextField (new Rect (Screen.width/100*12, Screen.height/100*7, Screen.width/100*35, 30), Username);
		GUI.Label (new Rect (Screen.width/100*22, Screen.height/100*12, Screen.width, Screen.height), "===Password===");
		Password = GUI.TextField (new Rect (Screen.width/100*12, Screen.height/100*15, Screen.width/100*35, 30), Password, '*');
		GUI.Label (new Rect (Screen.width/100*19, Screen.height/100*20, Screen.width, Screen.height), "===Re-enter Password===");
		Password2 = GUI.TextField (new Rect (Screen.width/100*12, Screen.height/100*24, Screen.width/100*35, 30), Password2, '*');
		GUI.Label (new Rect (Screen.width/100*16, Screen.height/100*29, Screen.width, Screen.height), "===IP Address to Register to===");
		url = GUI.TextField (new Rect (Screen.width/100*12, Screen.height/100*33, Screen.width/100*35, 30), url, '*');
		
		//if(GUI.Button (new Rect (25, 175, 180, 50), "Login"))
		//{
		//	StartCoroutine(HandleLogin(Username, Password));
		//}
		
		if(GUI.Button (new Rect (Screen.width/100*14, Screen.height/100*42, Screen.width/100*30, Screen.height/100*8), "Register"))
		{
			if(Password== Password2)
			{
				StartCoroutine(HandleRegistration());
			}
			else
			{
				Debug.Log("Non Similar Password");
			}
			//Initiate.Fade ("Registration", Color.black, 0.5f);
			//Initiate.Fade("", Color.black, 0.5f);
			//Debug.Log;
		}

		if(Event.current.keyCode == KeyCode.Return)
		{
			if(Password == Password2)
			{
				StartCoroutine(HandleRegistration());
			}
			else
			{
				Debug.Log("Non Similar Password");
			}
		}
		
		GUI.Label(new Rect(55, 224, 250, 100), label);
	}

//	IEnumerator HandleRegistration(string Userame, string Password, String Password2)
//	{
//		label = "Checking Username and Password with database";
//		string registerURL = this.loginURL + "?Username=" + Username;
//	}

	IEnumerator HandleRegistration()
	{
		label = "Checking Username and Password...";
		loginURL = "http://" + url + "/Register.php";
		string loginURL2 = loginURL + "?Username=" + Username + "&Password=" + Password;
		Debug.Log (loginURL2);
		WWW loginReader = new WWW (loginURL2);
		//loginReader = loginURL;
		yield return loginReader; //Returns login reader details
		
		if(loginReader.error != null)
		{
			label = "Error connecting database server";
		}
		else
		{
			int serverResponse = Convert.ToInt16(loginReader.text.Trim ());
			switch (serverResponse)
			{
			case 0: label = "Registration Successful!";
							Debug.Log (loginReader);
							buttonpress = true;
				Initiate.Fade("LoginMenu", Color.black, 0.5f);
				//Initiate.Fade("LoginMenu", Color.black, 0.5f);
				break;
			case 1: label = "Username / Password is taken";
				break;
			default: label = "Invalid Username / Password";
				break;
			}
			
			
			
		}
	}
}
