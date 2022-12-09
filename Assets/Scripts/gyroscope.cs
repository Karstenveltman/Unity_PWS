using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gyroscope: MonoBehaviour {
    [SerializeField] float speed; 
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float deadZone;
    [SerializeField] Camera mainCamera;
    private Vector3 CorrectionVector;
    private bool corrected = false;
    public bool dead = false;
    public bool keyboard;
    Vector3 acc = Vector3.zero;

    void Update() {
        if (Input.touchCount > 0) {
            corrected = false;
        }
    }

    void FixedUpdate (){
        // Debug.Log(dead);
        // Debug.Log(SystemInfo.supportsAccelerometer);
        if(dead == false) {
            if (!keyboard) {
                if ((Input.acceleration != Vector3.zero) && (!corrected)){
                    CorrectionVector = new Vector3(Input.acceleration.x, Input.acceleration.y, 0);
                    corrected = true;
                }
                Vector3 acc = new Vector3(Input.acceleration.x, Input.acceleration.y, 0) - CorrectionVector;
                if (acc.magnitude >= deadZone) {
                    rb.velocity = acc * speed;
                }
                else {
                    rb.velocity = Vector3.zero;
                }
            }
            if (keyboard) {
                Vector3 acc = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
                rb.velocity = acc * speed;
            }
        }  
        if (dead) {
            SceneManager.LoadScene(0);
        } 
    }
    
 }