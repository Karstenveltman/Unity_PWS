using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gyroscope: MonoBehaviour {
    [SerializeField] float speed; 
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float deadZone;
    [SerializeField] Camera mainCamera;
    private Vector3 CorrectionVector;
    private bool corrected = false;
    public bool dead = false;
    public int lives = 3;
    public int prevLives = 3;
    bool runOnlyOnce = false;
    public bool keyboard;
    Vector3 acc = Vector3.zero;
    public healthchanger healthbar;

    void Update() {
        if (Input.touchCount > 0) {
            corrected = false;
        }
    }

    void FixedUpdate (){
        // Debug.Log(dead);
        // Debug.Log(SystemInfo.supportsAccelerometer);
        if (prevLives != lives && prevLives != 0 && runOnlyOnce == false) {
            runOnlyOnce = true;
            Invoke("damage", 1f);
        }
        if (dead == false) {
            if (lives <= 0) {
                dead = true;
            }
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
    }
    
    void damage() {
        prevLives--;
        lives = prevLives;
        Debug.Log("lives: " + lives);
        runOnlyOnce = false;
        healthbar.lives = lives;
        //geen idee wat de glitch was die ervoor zorgte dat deze if statement nodig was, maar zonder dit is de speler dood als hij door 2 bommen tegelijkertijd geraakt wordt met 2 levens
        if (lives != 0) {
            dead = false;
        }
    }
 }