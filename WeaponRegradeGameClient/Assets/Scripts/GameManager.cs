using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }

    }
    public const string apiUrl = "http://127.0.0.1:3000";  //Node.js �ּ�
    [SerializeField]private AccountValue userValue;
    [SerializeField]private PlayerInventory playerInven;
    public PlayerInventory PlayerInven
    {
        get { return playerInven; }
        set { playerInven = value; }
    }
    public AccountValue UserValue 
    {
        get
        {
            return userValue;
        }
        set
        {
            StartCoroutine(mainToGameScene(value));
            StartCoroutine(GetInvenInfo(value.userID));
            userValue = value;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void GetInfo(uint index,string ID,string name)
    {
        userValue.index = index; 
        userValue.userID = ID;
        userValue.userName = name;
    }
    private IEnumerator mainToGameScene(AccountValue info)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene");
        float progress;
        while (!asyncLoad.isDone)
        {
            progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            Debug.Log("�ε� ����� : " + (progress * 100) + "%");
            yield return null;
        }
        
        UIDataManager.Instance.InGameUIOnOFF(true);
        Debug.Log("�ε� �Ϸ�");
    }
    public IEnumerator GetInvenInfo(string userID)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);
        PlayerInventory tempInven;
        using (UnityWebRequest webRequest = UnityWebRequest.Post(apiUrl + "/invenInfo", form))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                tempInven = JsonConvert.DeserializeObject<PlayerInventory>(webRequest.downloadHandler.text);
                Debug.Log("���� ���̵� : " + tempInven.userID + "�ִ� ��ȭġ : " + tempInven.maxRegrade + "���� �� : " + tempInven.money);
                GameManager.Instance.PlayerInven = tempInven;
                UIDataManager.Instance.SetAccountValue(tempInven);
            }
        }
    }

}
