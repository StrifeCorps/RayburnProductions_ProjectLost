using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameManager GameManager;

    void Start()
    {
        GameManager = GameManager.Instance;
    }
}
