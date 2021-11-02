using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleManager : MonoBehaviour
{
    public GameObject[] toggleList;

    void Start()
    {
        for(int i = 0; i < 2; i++)
        {
            //zbiernie toggli z komponentow i wkladanie ich do listy
            //GameObject[i] = GetComponentInChildren<GameObject>();
        }
    }

    void Update()
    {
        
    }
}
