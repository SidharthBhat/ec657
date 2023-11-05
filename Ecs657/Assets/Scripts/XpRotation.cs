using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpRotation : MonoBehaviour
{
    // Update is called once per frame
    //rotates gameobject
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
