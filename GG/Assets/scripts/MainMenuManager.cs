using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartLevel()
    {
        SceneManager.LoadScene("testScene1");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
