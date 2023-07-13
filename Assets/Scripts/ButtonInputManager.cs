using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInputManager : MonoBehaviour
{
    public void InputGoLeft()
    {
        FindObjectOfType<Group>().InputGoLeft();
    }

    public void InputGoRight()
    {
        FindObjectOfType<Group>().InputGoRight();
    }

    public void InputRotate()
    {
        FindObjectOfType<Group>().InputRotate();
    }

    public void InputGoDown()
    {
        FindObjectOfType<Group>().InputGoDown();
    }
}