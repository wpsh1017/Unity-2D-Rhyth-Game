using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Game Manager를 싱글톤 처리 
    public static GameManager instance { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this) Destroy(gameObject);
    }

    public float noteSpeed;

    public enum judges { NONE = 0,  BAD,  GOOD,  PERFECT,  MISS };
    /*
     * BAD : 1 
     * GOOD : 2
     * Perfect : 3
     * 
     */
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
