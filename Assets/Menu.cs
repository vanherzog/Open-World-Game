using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    public GameObject menu;

    // Start is called before the first frame update
    void Update()
    {

        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            menu.SetActive(false);
        }
    }
}