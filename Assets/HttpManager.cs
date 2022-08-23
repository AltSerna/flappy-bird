/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class HttpManager : MonoBehaviour
{

    [SerializeField]
    private string URL;
    [SerializeField]
    private Text[] textsMPScores;
    [SerializeField]
    private GameObject ScoreCanvas;

    private string Token;
    private string Username;

    private void Start()
    {
        Token = PlayerPrefs.GetString("token");
        Username = PlayerPrefs.GetString("username");
        Debug.Log("TOKEN: " + Token);

        StartCoroutine(GetPerfil());
    }

    private string GetInputData()
    {
        AuthData
    }
    
    IEnumerator GetPerfil()
    {
        string url = URL + "/api/usuarios/" + Username;
        UnityWebRequest www = UnityWebRequest.Get(url);
        www.SetRequestHeader("x-token", Token);

        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log("NETWORK ERROR " + www.error);
        }
        else if (www.responseCode == 200)
        {
            AuthData resData = JsonUtility.FromJson<AuthData>(www.downloadHandler.text);
            SceneManager.Loa
        }
    }
    
    IEnumerator LogIn(string postData)
    {
        Debug.Log("LOG IN :"+postData);
        string url = URL + "api/auth/login";

        UnityWebRequest www = UnityWebRequest.Put(url, postData);
        www.method = "POST";
        www.SetRequestHeader("content-type","application/json");

        yield return www.SendWebRequest();

        /*if (www.isNetworkError)
        {
            Debug.Log("NETWORK ERROR "+www.error);
        }
        else if (www.responseCode == 200)
        {
            //Debug.log(www.downloadHandler.text);
            
            //AuthData resData
        }
    }

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
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HttpManager : MonoBehaviour
{

    [SerializeField]
    private string URL;
    [SerializeField]
    private Text[] textsMPScores;
    [SerializeField]
    private GameObject ScoreCanvas;

    private string Token;
    private string Username;

    void Start()
    {
        Token = PlayerPrefs.GetString("token");
        Username = PlayerPrefs.GetString("username");
        Debug.Log("TOKEN: " + Token);

        StartCoroutine(GetPerfil());
    }

    public void ClickGetScores()
    {
        StartCoroutine(GetScores());
    }

    public void ClickSignUp()
    {

        AuthData data = new AuthData();

        data.username = GameObject.Find("InputFieldUsuario").GetComponent<InputField>().text;
        data.password = GameObject.Find("InputFieldContrasena").GetComponent<InputField>().text;

        string postData = JsonUtility.ToJson(data);
        StartCoroutine(SignUp(postData));
    }
    
    private string GetInputData()
    {
        AuthData data = new AuthData();

        data.username = GameObject.Find("InputFieldUsuario").GetComponent<InputField>().text;
        data.password = GameObject.Find("InputFieldContrasena").GetComponent<InputField>().text;

        string postData = JsonUtility.ToJson(data);
        return postData;
    }
    
    public void ClickLogIn()
    {
        string postData = GetInputData();
        StartCoroutine(LogIn(postData));
    }

    IEnumerator LogIn(string postData)
    {
        Debug.Log("LOG IN :"+postData);
        string url = URL + "/api/auth/login";
        UnityWebRequest www = UnityWebRequest.Put(url,postData);
        www.method = "POST";
        www.SetRequestHeader("content-type", "application/json");

        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log("NETWORK ERROR " + www.error);
        }
        else if (www.responseCode == 200)
        {
            //Debug.Log(www.downloadHandler.text);
            AuthData resData = JsonUtility.FromJson<AuthData>(www.downloadHandler.text);

            Debug.Log("Autenticado " + resData.usuario.username + ", id" + resData.usuario._id);
            Debug.Log("TOKEN "+resData.token);
            
            PlayerPrefs.SetString("token",resData.token);
            PlayerPrefs.SetString("username",resData.usuario.username);
            SceneManager.LoadScene("Game");
        }
        else
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
    }
    
    IEnumerator GetPerfil()
    {
        string url = URL + "/api/usuarios/" + Username;
        UnityWebRequest www = UnityWebRequest.Get(url);
        www.SetRequestHeader("x-token", Token);

        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log("NETWORK ERROR " + www.error);
        }
        else if (www.responseCode == 200)
        {
            AuthData resData = JsonUtility.FromJson<AuthData>(www.downloadHandler.text);
            
            Debug.Log("Token valido "+resData.usuario.username+", id:"+resData.usuario._id);
            SceneManager.LoadScene("Game");
        }
        else
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
    }
    
    

    IEnumerator GetScores()
    {
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

            foreach (ScoreData score in resData.scores)
            {
                Debug.Log(score.name +" | "+score.value);
            }
        }
        else
        {
            Debug.Log(www.error);
        }
    }

    

    IEnumerator SignUp(string postData)
    {
        Debug.Log(postData);
        
        string url = URL + "/api/usuarios";
        UnityWebRequest www = UnityWebRequest.Put(url, postData);
        www.method = "POST";
        www.SetRequestHeader("content-type", "application/json");

        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log("NETWORK ERROR " + www.error);
        }
        else if (www.responseCode == 200)
        {
            //Debug.Log(www.downloadHandler.text);
            AuthData resData = JsonUtility.FromJson<AuthData>(www.downloadHandler.text);

            Debug.Log("Bienvenido "+ resData.usuario.username +", id:"+resData.usuario._id);
        }
        else
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
    }
}


[System.Serializable]
public class ScoreData
{
    public int userId;
    public int value;
    public string name;

}

[System.Serializable]
public class Scores
{
    public ScoreData[] scores;
}

[System.Serializable]
public class AuthData
{
    public string username;
    public string password;
    public UserData usuario;
    public string token;
}

[System.Serializable]
public class UserData
{
    public string _id;
    public string username;
    public bool estado;
    public int score;
}

