using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSelection : MonoBehaviour {

    TurretManager turretManager;

    void Start()
    {
        turretManager = TurretManager.instance;
    }

    public void GetColdTurret()
    {
        Debug.Log("Standard Turret Selected");
        turretManager.SetTurretToBuild(turretManager.coldTurret);
    }

    public void GetHotTurret()
    {
        Debug.Log("Another Turret Selected");
        turretManager.SetTurretToBuild(turretManager.hotTurret);
    }
}
