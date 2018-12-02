using UnityEngine;

public class TitleScreenController : MonoBehaviour
{
    public float rotateX, rotateY, animationTime, offset;
    private Quaternion orgRotation;
    private Vector3 pos;
    private SceneLoad load;
    private SceneLoad unload;

    private void Start()
    {
        orgRotation = transform.rotation;
        pos = transform.position;
        SceneLoad[] loads = FindObjectsOfType<SceneLoad>();
        foreach (SceneLoad l in loads)
        {
            if (l.level.Equals("TitleScreen"))
                unload = l;
            else if (l.level.Equals("GameScene"))
                load = l;
        }
    }

    void Update()
    {
        transform.rotation = orgRotation * Quaternion.Euler(Mathf.Sin(Time.time / animationTime) * rotateX, Mathf.Cos(Time.time / animationTime) * rotateY, 0);
        transform.position = pos + new Vector3(Mathf.Sin(Time.time / animationTime * 1.333F) * offset, Mathf.Cos(Time.time / animationTime * 0.5F) * offset * 0.5F, Mathf.Cos(Time.time / animationTime * 0.25F) * offset);

        if (Input.GetButtonDown("Signal 1") || Input.GetButtonDown("Signal 2") || Input.GetButtonDown("Signal 3") || Input.GetButtonDown("Signal 4"))
        {
            load.enabled = true;
            unload.enabled = false;
        }
    }
}
