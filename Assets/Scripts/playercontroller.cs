using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playercontroller: MonoBehaviour {
    [SerializeField] float speed; 
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float deadZone;
    [SerializeField] Camera mainCamera;
    private Vector3 CorrectionVector;
    private bool corrected = false;
    public bool stunned = false;
    public int lives = 3;
    public int prevLives = 3;
    bool invulnerable = false;
    public bool keyboard;
    Vector3 acc = Vector3.zero;
    public healthchanger healthbar;

    void Update() {
        if (Input.touchCount > 0) {
            corrected = false;
        }
    }

    void FixedUpdate (){
        Debug.Log(stunned);
        if (prevLives != lives && prevLives != 0 && invulnerable == false) {
            invulnerable = true;
            stunned = true;
            prevLives--;
            lives = prevLives;
            healthbar.lives = lives;
            //geen idee wat de glitch was die ervoor zorgte dat deze if statement nodig was, maar zonder dit is de speler dood als hij door 2 bommen tegelijkertijd geraakt wordt met 2 levens
            Invoke("invulnerability", 3f);
            Invoke("revive", 1f);
        }
        if (stunned == false) {
            if (lives <= 0) {
                stunned = true;
                SceneManager.LoadScene(0);
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
    
    void invulnerability() {
        invulnerable = false;
        if (lives != 0) {
            stunned = false;
        }
    }
    void revive() {
        stunned = false;
    }
 }