﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager instance = null;
    public ink_manager inkManager;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

}