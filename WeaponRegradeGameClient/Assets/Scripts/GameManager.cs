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
    public const string apiUrl = "http://127.0.0.1:3000";  //Node.js 주소
    [SerializeField]public AccountValue userValue;
    [SerializeField]public PlayerInventory playerInven;
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
            StartCoroutine(GetItemTable());
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
            Debug.Log("로딩 진행률 : " + (progress * 100) + "%");
            yield return null;
        }
        
        UIDataManager.Instance.InGameUIOnOFF(true);
        Debug.Log("로딩 완료");
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
                Debug.Log("유저 아이디 : " + tempInven.userID + "최대 강화치 : " + tempInven.maxRegrade + "가진 돈 : " + tempInven.money);
                GameManager.Instance.PlayerInven = tempInven;
                UIDataManager.Instance.SetAccountValue(tempInven);
            }
        }
    }
    public IEnumerator GetItemTable()
    {
        WWWForm form = new WWWForm();
        ItemTableList itemList;
        using (UnityWebRequest webRequest = UnityWebRequest.Post(apiUrl + "/getItemTable", form))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string json = webRequest.downloadHandler.text;
                Debug.Log("테이블 리스트 : " + json);

//                itemList = JsonUtility.FromJson<ItemTableList>("{\"results\":" + json + "}");
                itemList = JsonConvert.DeserializeObject<ItemTableList>(webRequest.downloadHandler.text);
                for (int i = 0; i < itemList.results.Length; i++)
                {
                    UIDataManager.Instance.itemDictionary.Add(itemList.results[i].Index,itemList.results[i]);
                    Debug.Log(UIDataManager.Instance.itemDictionary[itemList.results[i].Index].itemName);
                }
                
            }
        }
    }


}
