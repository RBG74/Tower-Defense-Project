using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one GameManager in scene!");
            return;
        }
        instance = this;
    }

    //[HideInInspector]
    public float coldPercent = 50;
    private float coldDamageDone = 0;
    //[HideInInspector]
    public float hotPercent = 50;
    private float hotDamageDone = 0;

    public void IncreaseDamageDone(string damageType, float damage)
    {
        if (damageType == "Cold")
            coldDamageDone += damage;
        else if (damageType == "Hot")
            hotDamageDone += damage;
    }

    public void UpdatePercentages()
    {
        if(coldDamageDone == 0 && hotDamageDone == 0)
        {
            coldPercent = 50;
            hotPercent = 50;
            return;
        }
        var total = coldDamageDone + hotDamageDone;
        coldPercent = coldDamageDone / total * 100;
        hotPercent = hotDamageDone / total * 100;
    }

    public void ResetDamageDone()
    {
        coldDamageDone = 0;
        hotDamageDone = 0;
    }
}
