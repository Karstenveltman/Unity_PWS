using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class borderScaler : MonoBehaviour
{
    [SerializeField] EdgeCollider2D ec;
    Vector2[] colliderPoints;
    [SerializeField] Camera mainCamera;
    void Start()
    {
        colliderPoints = ec.points;
        colliderPoints[0] = new Vector2(mainCamera.aspect * mainCamera.orthographicSize, mainCamera.orthographicSize);
        colliderPoints[1] = new Vector2(mainCamera.aspect * mainCamera.orthographicSize, -mainCamera.orthographicSize);
        colliderPoints[2] = new Vector2(-mainCamera.aspect * mainCamera.orthographicSize, -mainCamera.orthographicSize);
        colliderPoints[3] = new Vector2(-mainCamera.aspect * mainCamera.orthographicSize, mainCamera.orthographicSize);
        colliderPoints[4] = new Vector2(mainCamera.aspect * mainCamera.orthographicSize, mainCamera.orthographicSize);
        ec.points = colliderPoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
