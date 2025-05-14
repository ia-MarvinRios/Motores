using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return null;

        SceneManager.LoadScene("MainGame");
    }
}
