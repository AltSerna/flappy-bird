using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreButton : MonoBehaviour
{
    public GameObject canvasScore;

    public void CloseCanvas()
    {
        canvasScore.SetActive(false);
    }
}
