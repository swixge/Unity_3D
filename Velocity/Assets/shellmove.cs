using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shellmove : MonoBehaviour
{
    float speed = 1;
    void Update()
    {
        this.transform.Translate(0, (Time.deltaTime * speed) / 2.0f, Time.deltaTime * speed);

    }
}
