using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryController : MonoBehaviour
{
    public float timeLimit = 5 * 60;
    public int playerCountForVictory = 4;

    private GoalCameraAnimation finishAnim;
    private HashSet<PlayerScript> finishedPlayers = new HashSet<PlayerScript>();
    private bool finished = false;

    void Start()
    {
        finishAnim = FindObjectOfType<GoalCameraAnimation>();
    }

    void Update()
    {
        if (Time.unscaledTime >= timeLimit && !finished)
        {
            finished = true;
            Debug.Log("Timeout!");
            finishAnim.StartAnimation();
        }
    }

    public void MarkNotFinished(PlayerScript player)
    {
        finishedPlayers.Remove(player);
    }

    public void MarkFinished(PlayerScript player)
    {
        finishedPlayers.Add(player);
        Debug.Log(player.name + " reached the goal!");

        if (finishedPlayers.Count >= playerCountForVictory)
        {
            finished = true;
            finishAnim.StartAnimation();
        }
    }
}
