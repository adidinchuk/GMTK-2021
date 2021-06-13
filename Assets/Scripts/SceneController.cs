using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SceneController: MonoBehaviour {

	public static SceneController Instance { get; private set; }

    private void Awake()
    {
		Instance = this;
    }

    public void LoadLevel(string name){
		Debug.Log ("Load level requested for: " + name);
        SceneManager.LoadScene(name);
    }
	
	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit();
	}

	public void LoadCurrentLevel(){
        
	}

	public void LoadNextLevel(Scene scene, float time, int score, int difficulty){
        
	}



	public void reloadLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
