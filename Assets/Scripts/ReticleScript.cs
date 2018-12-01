using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleScript : MonoBehaviour
{
    public PlayerScript otherPlayer;

    private Vector3 vec;
    private Vector3 normVec;
    private SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>();
        rend.color = otherPlayer.myColor; 
    }

    // Update is called once per frame
    void Update()
    {
        NormilizedVector();
        transform.position = transform.parent.position + normVec * 10;
        transform.LookAt(otherPlayer.transform);
        transform.rotation *= Quaternion.Euler(90, 0, 0);
        if (vec.magnitude < 10)
        {
            rend.enabled = false;
        }
        else
        {

            rend.enabled = true;
        }
    }


    void NormilizedVector()
    {
        vec = otherPlayer.transform.position - transform.parent.position;
         normVec = (vec).normalized;
    }
}
