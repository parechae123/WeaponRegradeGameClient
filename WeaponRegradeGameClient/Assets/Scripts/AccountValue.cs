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
}
