using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour
{
    public float autoLoadNextLevel;

	void Start ()
    {
		if (autoLoadNextLevel != 0)
        {
            Invoke("LoadNextLevel", autoLoadNextLevel);
        }
	}
	
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitRequest()
    {
        Application.Quit();
        print("Quit requested");
    }
}
