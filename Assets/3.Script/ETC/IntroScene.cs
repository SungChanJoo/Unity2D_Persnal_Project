using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class IntroScene : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKey)
            SceneLoader.LoadScene("MainGame");
    }
}
