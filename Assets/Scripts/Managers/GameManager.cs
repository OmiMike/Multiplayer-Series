using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InputManager iMan;

    void Start()
    {
        iMan.running = KeyCode.LeftShift;    
    }

    void Update()
    {
        
    }
}
