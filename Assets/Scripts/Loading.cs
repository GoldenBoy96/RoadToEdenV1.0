using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AsyncOperation = UnityEngine.AsyncOperation;

public class Loading : MonoBehaviour
{
    private int _cooldown;

    public Image _loadingBar;

    // Start is called before the first frame update
    void Start()
    {
        _cooldown = 10;
        
    }

    private void Update()
    {
        _cooldown--;
        if (_cooldown == 0)
        {
            StartCoroutine(LoadGame());
        }
    }

    IEnumerator LoadGame()
    {
        AsyncOperation loadLevel = SceneManager.LoadSceneAsync("Main");
        loadLevel.allowSceneActivation = true;

        while (!loadLevel.isDone)
        {
            //Debug.Log("Loading progress: " + (loadLevel.progress * 100) + "%");
            yield return null;
        }
        
    }
}
