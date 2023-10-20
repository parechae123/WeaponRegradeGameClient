using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AccountValue
{
    [SerializeField] public uint index;
    [SerializeField] public string userID;
    [SerializeField] public string userName;
}
[System.Serializable]
public class PlayerInventory
{
    [SerializeField] public string userID;
    [SerializeField] public int money;
    [SerializeField] public uint maxRegrade;
    [SerializeField] public uint WeaponIndex;
}
[System.Serializable]
public class ItemTable
{
    [SerializeField]public uint Index;
    [SerializeField] public string ItemName;
    [SerializeField] public string codeName;
    [SerializeField] public int sellValue;
    [SerializeField] public int buyValue;
    [SerializeField] public float regradePercent;
}
