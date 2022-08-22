using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetScoreHttp : MonoBehaviour
{
    [SerializeField] private string URL;
    private string Token, Username;

    IEnumerator CheckScore()
    {
        string url= URL + "/api/usuarios/score" + "";
        UnityWebRequest www = UnityWebRequest.Get(url);
        
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log("NETWORK ERROR " + www.error);
        }
        else if(www.responseCode == 200){
            //Debug.Log(www.downloadHandler.text);
            Scores resData = JsonUtility.FromJson<Scores>(www.downloadHandler.text);
            
        }
        else
        {
            Debug.Log(www.error);
        }
    }
    
    IEnumerator SetScore()
    {
        Debug.Log("PATCH SCORE: ");

        string url = URL + "/api/usuarios" + "";
        UnityWebRequest www = UnityWebRequest.Put(url, "{\'score\':"+Score.score+ ",\'username\'" + Username + "\'}"); // acá va el score
        www.method = "PATCH";
        www.SetRequestHeader("content-type","application/json");
        www.SetRequestHeader("x-token",Token);

        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log("NETWORK ERROR "+ www.error);
        }
        else if (www.responseCode == 200)
        {
            AuthData resData = JsonUtility.FromJson<AuthData>(www.downloadHandler.text);
        }
        else
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
    }
}
