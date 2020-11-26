using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Big Shot Studios/Managers/Input Manager")]
public class InputManager : ScriptableObject
{
    public KeyCode running;
    public KeyCode crouching;
    public KeyCode jump;
}
