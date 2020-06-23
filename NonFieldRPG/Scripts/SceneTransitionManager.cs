﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public void LoadTo(string sceneName)  // ~~シーンを読み込みなさい
    {
        FadeIOManager.instance.FadeOutToIn(() => Load(sceneName));
    }

    void Load(string sceneName)  // ☆
    {
        SoundManager.instance.PlayBGM(sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
