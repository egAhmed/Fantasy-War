﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginBtnOnlineGame : MonoBehaviour {

    Button loginBtn;

    private void Awake()
    {
        loginBtn = GetComponent<Button>();
        loginBtn.onClick.AddListener(() => {

        });
    }
}
