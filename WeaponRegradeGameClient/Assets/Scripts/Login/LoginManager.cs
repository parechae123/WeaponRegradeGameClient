using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine;
using TMPro;
public class LoginManager : MonoBehaviour
{
    [Header("�α���")]
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TextMeshProUGUI loginStatusText;
    [Header("ȸ������")]
    public TMP_InputField usernameRegist;
    public TMP_InputField passwordRegist;
    public TextMeshProUGUI loginRegist;

    public string token;

    //�Ʒ� URL�� ����� ���� ȣ��Ʈ �ּҷ� �����������
    private const string apiUrl = "http://localhost:3000";  //Node.js �ּ�
    public void Regist()
    {
        StartCoroutine(AttemptRegist(usernameRegist.text,passwordRegist.text));
    }
    public void Login()
    {
        StartCoroutine(AttemptLogin(usernameInput.text, passwordInput.text));
    }
    IEnumerator AttemptRegist(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(apiUrl + "/resgist", form))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                loginStatusText.text = "Login failed : " + webRequest.error;
            }
            else
            {
                loginStatusText.text = "Login successful!";
                string responseText = webRequest.downloadHandler.text;
                //JSON ���信�� ��ū ���� ����
                var responseData = JsonConvert.DeserializeObject<ResponseData>(responseText);
                token = responseData.token;

                Debug.Log("Login successful! Token : " + token);
            }
        }
    }
    IEnumerator AttemptLogin(string username,string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(apiUrl + "/login", form))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                loginStatusText.text = "Login failed : " + webRequest.error;
            }
            else
            {
                loginStatusText.text = "Login successful!";
                string responseText = webRequest.downloadHandler.text;
                //JSON ���信�� ��ū ���� ����
                var responseData = JsonConvert.DeserializeObject<ResponseData>(responseText);
                token = responseData.token;

                Debug.Log("Login successful! Token : " + token);
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
        if (webRequest.result == UnityWebRequest.Result.ConnectionError|| webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("���� ��� ���� " + webRequest.downloadHandler.text);
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
}
