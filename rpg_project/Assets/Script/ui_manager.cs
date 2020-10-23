using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ui_manager : MonoBehaviour
{
    private Canvas menu;
    private void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("DungeonGeneratorScene");
        }
    }

}
