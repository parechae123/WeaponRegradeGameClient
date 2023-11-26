using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
public class SellButton : MonoBehaviour
{
    public GameManager gameManager;
    private void Awake()
    {
        gameManager = GameObject.Find("Managers").GetComponent<GameManager>();
    }
    public void OnClickSellBTN()
    {
        Debug.Log("now sellValue" + UIDataManager.Instance.nowWeapon.sellValue);
        if (UIDataManager.Instance.nowWeapon.Index != 1)
        {
            GameManager.Instance.playerInven.money += UIDataManager.Instance.nowWeapon.sellValue;
            UIDataManager.Instance.changeNowWeapon(1);
        }

        string userID = gameManager.userValue.userID;
        string money = gameManager.PlayerInven.money.ToString();
        string maxRegrade = gameManager.PlayerInven.maxRegrade.ToString();
        string weaponIndex = gameManager.PlayerInven.WeaponIndex.ToString();

        StartCoroutine(SendDataToServer(userID, money, maxRegrade, weaponIndex));
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
