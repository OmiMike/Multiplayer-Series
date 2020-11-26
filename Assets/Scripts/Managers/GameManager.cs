using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InputManager iMan;

    void Start()
    {
        iMan.running = KeyCode.LeftShift;
        iMan.crouching = KeyCode.C;
        iMan.jump = KeyCode.Space;
    }

    void Update()
    {
        
    }
}
