using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScene : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKey)
            SceneLoader.LoadScene("Intro");
    }
}
