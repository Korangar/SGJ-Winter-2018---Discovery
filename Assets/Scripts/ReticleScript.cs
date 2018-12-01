using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleScript : MonoBehaviour
{
    public Transform otherPlayer;

    private Vector3 normVec;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NormilizedVector();
        transform.position = transform.parent.position + normVec * 10;
        transform.LookAt(otherPlayer);
        transform.rotation *= Quaternion.Euler(90, 0, 0);
    }

    void NormilizedVector()
    {
         normVec = (otherPlayer.transform.position - transform.parent.position).normalized;
    }
}
