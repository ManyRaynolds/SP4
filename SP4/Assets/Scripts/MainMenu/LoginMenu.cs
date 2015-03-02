using UnityEngine;
using System.Collections;
using System;

public class LoginMenu : MonoBehaviour {

	public static string url = "";
	string loginURL = "";

	// Use this for initialization
	void Start () {
		
<<<<<<< HEAD
<<<<<<< HEAD
//		url = "127.0.0.1";
		url = Network.player.ipAddress;
=======
		url = "127.0.0.1";
>>>>>>> 56d2a76de522223448b8882592754fff06503730
=======
		url = "127.0.0.1";
>>>>>>> 56d2a76de522223448b8882592754fff06503730
	//	loginURL = "http://" + url + "/login.php";

	}
	
//	public static string loginURL = "http://127.0.0.1/login.php";


	//string loginURL = "file:///C|/xampp/htdocs/login.php";

	public static string Username = "";
	string Password = "";
	string label = "";
	bool buttonpress = false;

<<<<<<< HEAD
<<<<<<< HEAD

=======
>>>>>>> 56d2a76de522223448b8882592754fff06503730
=======
>>>>>>> 56d2a76de522223448b8882592754fff06503730
	void OnGUI()
	{
		if(!buttonpress)
		{
			GUI.Window(0, new Rect(Screen.width / 4, Screen.height /3, Screen.width/2, Screen.height/100*42), LoginWindow, "Login");
		}
	}

	void LoginWindow(int WindowID)
	{
		//GUI.Label (new Rect (160, 40, 130, 100), "===Username===");
		//Username = GUI.TextField (new Rect (25, 60, 375, 30), Username);

		GUI.Label (new Rect (Screen.width/100*19, Screen.height/100*4, (float)Screen.width/100*20, (float)Screen.height), "===Username===");
		Username = GUI.TextField (new Rect (Screen.width/100*11, Screen.height/100*7, Screen.width/100*31, Screen.width/100*3), Username);

		GUI.Label (new Rect (Screen.width/100*19, Screen.height/100*12, (float)Screen.width/100*20, (float)Screen.height), "===Password===");
		Password = GUI.TextField (new Rect (Screen.width/100*11, Screen.height/100*15, Screen.width/100*31, Screen.width/100*3), Password, '*');

		GUI.Label (new Rect (Screen.width/100*19, Screen.height/100*20, (float)Screen.width/100*20, (float)Screen.height), "===IP Address===");
		url = GUI.TextField (new Rect (Screen.width/100*11, Screen.height/100*23, Screen.width/100*31, Screen.width/100*3), url);

		if((GUI.Button (new Rect (Screen.width/100*11, Screen.height/100*33, Screen.width/100*14, Screen.width/100*5), "Login")))
		{
			StartCoroutine(HandleLogin());
		}

		if(Event.current.keyCode == KeyCode.Return)
		{
			StartCoroutine(HandleLogin());
		}

		if(GUI.Button (new Rect (Screen.width/100*28, Screen.height/100*33, Screen.width/100*14, Screen.width/100*5), "Register"))
		{
<<<<<<< HEAD
<<<<<<< HEAD
			Initiate.Fade("Registration", Color.black, 0.5f);
=======
			Initiate.Fade("LoginMenu", Color.black, 0.5f);
>>>>>>> 56d2a76de522223448b8882592754fff06503730
=======
			Initiate.Fade("LoginMenu", Color.black, 0.5f);
>>>>>>> 56d2a76de522223448b8882592754fff06503730
			//Debug.Log;
			buttonpress = true;
		}

		//GUI.Label(new Rect(55, 224, 250, 100), label);
	}

	IEnumerator HandleLogin()
	{
		label = "Checking Username and Password...";
		loginURL = "http://" + url + "/login.php";
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
			case 0: label = "Invalid Username / Password";
				break;
			case 1: label = "Successfully logged in";
				buttonpress = true;
<<<<<<< HEAD
<<<<<<< HEAD
				Initiate.Fade ("Grid",Color.black, 0.5f);
=======
				Initiate.Fade ("CreateGame",Color.black, 0.5f);

>>>>>>> 56d2a76de522223448b8882592754fff06503730
=======
				Initiate.Fade ("CreateGame",Color.black, 0.5f);

>>>>>>> 56d2a76de522223448b8882592754fff06503730
				break;
			default: label = "Invalid Username / Password";
				break;
			}



		}
	}
}
