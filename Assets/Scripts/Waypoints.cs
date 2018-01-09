using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour {

    public static Transform[] instances;

    void Awake () {
        instances = new Transform[transform.childCount];
        for (int i = 0; i < instances.Length; i++)
        {
            instances[i] = transform.GetChild(i);
        }
    }
}
