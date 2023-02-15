using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPCodeManager : MonoBehaviour
{
    //reads join code from the input field and stores it for when joining
    public void ReadInput(string joinCode){
        PlayerPrefs.SetString("joinCode", joinCode);
    }
}
