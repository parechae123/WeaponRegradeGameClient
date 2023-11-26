using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
public class RegradeButton : MonoBehaviour
{
    public GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("Managers").GetComponent<GameManager>();
    }
    public void OnClickRegradeBTN()
    {
        GameManager.Instance.playerInven.money -= UIDataManager.Instance.nowWeapon.regradeValue;
        if (isRegradeSucces()&& GameManager.Instance.playerInven.money - UIDataManager.Instance.nowWeapon.regradeValue > 0)
        {
            UIDataManager.Instance.changeNowWeapon(UIDataManager.Instance.nowWeapon.Index+1);
            
        }
        else if(isRegradeSucces() && GameManager.Instance.playerInven.money - UIDataManager.Instance.nowWeapon.regradeValue > 0)
        {
            UIDataManager.Instance.changeNowWeapon(1);
        }
        string userID = gameManager.userValue.userID;
        string money = gameManager.PlayerInven.money.ToString();
        string maxRegrade = gameManager.PlayerInven.maxRegrade.ToString();
        string weaponIndex = gameManager.PlayerInven.WeaponIndex.ToString();

        StartCoroutine(SendDataToServer(userID, money, maxRegrade, weaponIndex));
    }

    private bool isRegradeSucces()
    {
        int tempNum = Random.Range((int)0, (int)101);
        Debug.Log("³ª¿Â È®·ü°ª" + tempNum + "¹«±â È®·ü" +(100 - (UIDataManager.Instance.nowWeapon.regradePercent)));
        if (tempNum >= 100-(UIDataManager.Instance.nowWeapon.regradePercent))
        {

            return true;
        }
        else
        {
            return false;
        }
    }
    IEnumerator SendDataToServer(string userID, string money, string maxRegrade, string weaponIndex)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);
        form.AddField("money", money);
        form.AddField("maxRegrade", maxRegrade);
        form.AddField("WeaponIndex", weaponIndex);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(gameManager.apiUrl + "/updateInven", form))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to save data to the server: " + webRequest.error);
            }
            else
            {
                Debug.Log("Data saved successfully!");
            }
        }
    }
}