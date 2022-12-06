using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detonation : MonoBehaviour
{
    [SerializeField] Object explosion;
    [SerializeField] Object bomb;
    [SerializeField] Camera mainCamera;
    [SerializeField] float fusetime;
    void Start()
    {
        Invoke("spawning", fusetime);
        Destroy(bomb, fusetime);
    }


    void spawning() {
        Instantiate(explosion, transform.localPosition, Quaternion.identity);
    }
    void Update()
    {

    }
}
