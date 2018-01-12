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
        turretManager.SetTurretToBuild(turretManager.coldTurret);
    }

    public void GetHotTurret()
    {
        turretManager.SetTurretToBuild(turretManager.hotTurret);
    }
}
