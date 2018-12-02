using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    SceneLoad title, game;

    void Start()
    {
        SceneLoad[] loads = FindObjectsOfType<SceneLoad>();
        foreach (SceneLoad l in loads)
        {
            if (l.level.Equals("TitleScreen"))
                title = l;
            else if (l.level.Equals("GameScene"))
                game = l;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Reset"))
        {
            title.enabled = !title.enabled;
            game.enabled = !game.enabled;
        }
    }
}
