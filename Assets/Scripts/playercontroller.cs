using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playercontroller: NetworkBehaviour {
    [SerializeField] float speed; 
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float deadZone;
    [SerializeField] Camera mainCamera;
    private Vector3 CorrectionVector;
    private bool corrected = false;
    bool stunned = false;
    public int lives = 3;
    public bool hit = false;
    bool invulnerable = false;
    public bool keyboard;
    Vector3 acc = Vector3.zero;
    public healthchanger healthbar;

    void Update() {
        if (!IsOwner) return;
        if (Input.touchCount > 0) {
            corrected = false;
        }
    }

    void FixedUpdate (){
        if (!IsOwner) return;
        if (hit == true && lives != 0 && invulnerable == false) {
            invulnerable = true;
            stunned = true;
            lives--;
            healthbar.lives = lives;
            Invoke("invulnerability", 3f);
            Invoke("revive", 1f);
        }
        if (stunned == false) {
            if (lives <= 0) {
                stunned = true;
                SceneManager.LoadScene(3);
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
        hit = false;
    }
    void revive() {
        stunned = false;
    }
 }