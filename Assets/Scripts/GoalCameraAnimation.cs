﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCameraAnimation : MonoBehaviour
{
    public float startY, endY;
    public float animationDuration;
    Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        cam.enabled = false;
    }

    public void StartAnimation()
    {
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        cam.enabled = true;
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, startY, pos.z);
        for (float f = 0; f < animationDuration; f += Time.deltaTime)
        {
            float t = 1 - f / animationDuration;
            t = 1 - t * t;
            transform.position = new Vector3(pos.x, startY + t * (endY - startY), pos.z);
            yield return new WaitForEndOfFrame();
        }
        transform.position = new Vector3(pos.x, endY, pos.z);
    }
}