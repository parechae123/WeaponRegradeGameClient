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
public class ItemTableList
{
    public ItemTable[] results;
}

[System.Serializable]
public class ItemTable
{
    public uint Index;
    public string itemName;
    public string codeName;
    public int sellValue;
    public int buyValue;
    public float regradePercent;
    public int regradeValue;
}
