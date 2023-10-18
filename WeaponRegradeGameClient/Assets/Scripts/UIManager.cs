using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text money;
    public Image weaponIMG;
    private static UIManager instance;
    public static UIManager Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        if (instance ==null)
        {

            if(instance == this)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
        }
        else
        {
            if (instance != this)
            {
                Destroy(this);
            }
        }
    }
    public void chageWeaponIMG(int itemIndex)
    {
        weaponIMG.sprite = Resources.Load<Sprite>("WeaponIMG/Sword"+itemIndex);
    }
}
