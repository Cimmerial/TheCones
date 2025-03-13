using UnityEngine;
using System.Collections.Generic;

public class LibraryCones : MonoBehaviour
{
    //-+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- VARIABLES -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+-
    private static LibraryCones instance;
    public static LibraryCones Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LibraryCones>();

                if (instance == null)
                {
                    GameObject go = new GameObject("LibraryCones");
                    instance = go.AddComponent<LibraryCones>();
                }
            }
            return instance;
        }
    }
    //===================================================================================================================
    // [Header("VARS")]
    

    //-+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- BASE FUNCTIONS -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+-

    private void Awake()
    {
        // Singleton pattern implementation
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    //-+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- CLASS FUNCTIONS -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+- -+-
   

}