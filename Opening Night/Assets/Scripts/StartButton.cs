using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("SampleScene"); //add name of scene to jump to
    }
}
