using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_resizer : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(mainCamera.orthographicSize);
    }
}
