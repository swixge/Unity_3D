using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScript : MonoBehaviour
{
    void PlayAndDestroy(ParticleSystem part)
    {
        part = GetComponent<ParticleSystem>();
        part.Play();
        Destroy(gameObject, part.main.duration);
    }
}
