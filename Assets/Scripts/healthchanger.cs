using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthchanger : MonoBehaviour
{
    SpriteRenderer healthbar;
    public Sprite twoLeft;
    public Sprite oneLeft;
    public Sprite noneLeft;
    public int lives = 3;
    // Start is called before the first frame update
    void Start()
    {
        healthbar = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(lives) {
            case 3:
            break;
            case 2:
            healthbar.sprite = twoLeft;
            break;
            case 1:
            healthbar.sprite = oneLeft;
            break;
            case 0:
            healthbar.sprite = noneLeft;
            break;
        }
    }
}
