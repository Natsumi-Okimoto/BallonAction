using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverZone : MonoBehaviour
{
    [SerializeField]
    private AudioManager audioManager;
    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.name);
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerController>().GameOver();   

            Debug.Log("Game Over");

            StartCoroutine(audioManager.PlayBGM(3));
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
