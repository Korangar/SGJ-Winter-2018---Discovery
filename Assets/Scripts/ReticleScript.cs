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

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.color = otherPlayer.myColor; 
    }

    // Update is called once per frame
    void Update()
    {
        NormilizedVector();
        transform.position = transform.parent.position + normVec * checkDistance;
        transform.LookAt(otherPlayer.transform);
        transform.rotation *= Quaternion.Euler(90, 0, 0);
        rend.enabled = vec.magnitude > checkDistance+blendDistance;
    }


    void NormilizedVector()
    {
        vec = otherPlayer.transform.position - transform.parent.position;
         normVec = (vec).normalized;
    }
}
