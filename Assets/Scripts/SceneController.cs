using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SceneController: MonoBehaviour {


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

	public void loadOptionsMenu(){
		SceneManager.LoadScene("Options", LoadSceneMode.Additive);
	}

	public void unloadOptionsMenu(){
		SceneManager.UnloadSceneAsync("Options");
	}

	public void reloadLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
