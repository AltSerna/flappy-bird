using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HttpManager : MonoBehaviour
{

    [SerializeField]
    private string URL;
    [SerializeField]
    private Text[] textsMPScores;
    [SerializeField]
    private GameObject ScoreCanvas;
    
    public void ClickGetScores()
    {
        StartCoroutine(GetScores());
    }

    IEnumerator GetScores()
    {
        ScoreCanvas.SetActive(true);
        string url = URL + "/leaders";
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log("NETWORK ERROR " + www.error);
        }
        else if(www.responseCode == 200){
            //Debug.Log(www.downloadHandler.text);
            Scores resData = JsonUtility.FromJson<Scores>(www.downloadHandler.text);

            int indexScore = 0;
            foreach (ScoreData score in resData.scores)
            {
                textsMPScores[indexScore].text = score.value.ToString() + " points";
                Debug.Log(score.userId +" | "+score.value);
                indexScore++;
            }
        }
        else
        {
            Debug.Log(www.error);
        }
    }
    
}


[System.Serializable]
public class ScoreData
{
    public int userId;
    public int value;

}

[System.Serializable]
public class Scores
{
    public ScoreData[] scores;
}
