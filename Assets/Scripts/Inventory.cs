using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Keys {
    NoKey,
    Red,
    Yellow,
    Green,
    Blue
}

public class Inventory : MonoBehaviour
{
    GameObject HUD;
    private void Start()
    {
        HUD = GameObject.FindWithTag("HUD");
        AddKey(Keys.NoKey);
    }
    private bool[] keyring = new bool[10];
    public void AddKey(Keys k)
    {
        HUD.GetComponent<HUD>().SetKey(k);
        keyring[(int)k] = true;
    }
    public void RemoveKey(Keys k)
    {
        HUD.GetComponent<HUD>().UnsetKey(k);
        keyring[(int)k] = false;
    }
    public void RemoveAllKeys()
    {
        HUD.GetComponent<HUD>().UnsetAllKeys();
        // starts at 1, because keyring[0] is the NoKey value (we don't want to remove that)
        for (int i=1; i < keyring.Length; i++)
        {
            keyring[i] = false;
        }
    }
    public bool CheckKey(Keys k)
    {
        return keyring[(int)k];
    }
}
