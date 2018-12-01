using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleScript : MonoBehaviour
{
    public float checkDistance = 10;
    public float blendDistance = 2;
    public PlayerScript otherPlayer;

    private Vector3 vec;
    private Vector3 normVec;
    private SpriteRenderer rend;
    private SpriteRenderer circleRenderer;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.color = otherPlayer.myColor;
        for (int i = 0; i < otherPlayer.transform.childCount; ++i)
        {
            Transform child = otherPlayer.transform.GetChild(i);
            if (child.gameObject.layer == gameObject.layer)
            {
                circleRenderer = child.GetComponent<SpriteRenderer>();
                break;
            }
        }
        circleRenderer.enabled = false;
        circleRenderer.color = otherPlayer.myColor;
    }

    void Update()
    {
        NormalizedVector();
        transform.position = transform.parent.position + normVec * checkDistance;
        transform.LookAt(otherPlayer.transform);
        transform.rotation *= Quaternion.Euler(90, 0, 0);
        rend.enabled = vec.magnitude > checkDistance+blendDistance;
        circleRenderer.enabled = !rend.enabled;
    }


    void NormalizedVector()
    {
            vec = otherPlayer.transform.position - transform.parent.position;
         normVec = (vec).normalized;
    }
}
