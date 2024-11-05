using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePersist : MonoBehaviour
{
    void Awake()
    {
        int numberGamePersist = FindObjectsOfType<GamePersist>().Length;
        if(numberGamePersist > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ResetGamePersist()
    {
        Destroy(gameObject);
    }
}
