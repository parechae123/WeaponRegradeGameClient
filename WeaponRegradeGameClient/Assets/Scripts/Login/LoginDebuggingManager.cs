using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine;
using TMPro;
using System.Net; 

public class LoginDebuggingManager : MonoBehaviour
{
    [Header("로그인")]
    public TMP_InputField userIDInput;
    public TMP_InputField passwordInput;
    public TextMeshProUGUI loginStatusText;
    public string token;

    [Header("회원가입")]
    public TMP_InputField userIDRegist;
    public TMP_InputField usernameRegist;
    public TMP_InputField passwordRegist;
    public TextMeshProUGUI loginRegist;

    [Header("판넬")]
    public GameObject loginPanel;
    public GameObject registrationPanel;

    //아래 URL은 응용시 본인 호스트 주소로 변경행줘야함

    public const string apiUrl = "http://127.0.0.1:3000";  //Node.js 주소

    public void Regist()
    {
        StartCoroutine(AttemptRegist(userIDRegist.text, usernameRegist.text, passwordRegist.text));
    }
    public void Login()
    {
        StartCoroutine(AttemptLogin(userIDInput.text, passwordInput.text));
    }
    IEnumerator AttemptRegist(string userID, string userName, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);
        form.AddField("userName", userName);
        form.AddField("userPassword", password);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(apiUrl + "/regist", form))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                loginStatusText.text = "Login failed : " + webRequest.error;
            }

        }
    }
    IEnumerator AttemptLogin(string userID, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);
        form.AddField("userPassword", password);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(apiUrl + "/login", form))
        {
            yield return webRequest.SendWebRequest();
            Debug.Log(webRequest.responseCode);
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                loginStatusText.text = "Login failed : " + webRequest.downloadHandler.text;
            }
            else
            {
                loginStatusText.text = "Login successful!";
                string responseText = webRequest.downloadHandler.text;
                //JSON 응답에서 토큰 값을 추출
                var responseData = JsonConvert.DeserializeObject<ResponseData>(responseText);
                token = responseData.token;
                StartCoroutine(SuccessLogin(userID));
                Debug.Log("Login successful! Token : " + token);
            }
        }
    }
    IEnumerator SuccessLogin(string userID)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);
        AccountValue tempACC;
        using (UnityWebRequest webRequest = UnityWebRequest.Post(apiUrl + "/loginSuccess", form))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                tempACC = JsonConvert.DeserializeObject<AccountValue>(webRequest.downloadHandler.text);
                Debug.Log("인덱스 : " +tempACC.index+"유저 아이디 : " +tempACC.userID+ "유저네임 : " + tempACC.userName);
                
                GameManager.Instance.UserValue = tempACC;
            }
        }
    }

    public void SendAutienticatedRequest(string endpoint)
    {
        if (string.IsNullOrEmpty(token))
        {
            Debug.LogError("Token is missing");
            return;
        }

        UnityWebRequest www = UnityWebRequest.Get(apiUrl + endpoint);
        www.SetRequestHeader("Authorization", token);

        StartCoroutine(SendRequest(www));
    }
    IEnumerator SendRequest(UnityWebRequest webRequest)
    {
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("서버 통신 에러 " + webRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log("Request successful" + webRequest.downloadHandler.text);
        }
    }

    public void SendAuthenticatedRequestToProtectedEndpint()
    {
        if (string.IsNullOrEmpty(token))
        {
            Debug.LogError("Token is missing");
            return;
        }
        SendAutienticatedRequest("/protected");
    }
    [System.Serializable]
    private class ResponseData
    {
        public string token;
    }
    public void ShowLoginPanel()
    {
        loginPanel.SetActive(true);
        registrationPanel.SetActive(false);
    }

    public void ShowRegistrationPanel()
    {
        loginPanel.SetActive(false);
        registrationPanel.SetActive(true);
    }
   
}
