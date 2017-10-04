using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    private float speed = 10f;
    private Vector2 v;
    private Rigidbody2D rb2D;
    public GameObject enemy;
    private Animator animator;
    bool facingRight = true;
    private int numberOfEnemies = 0;

    // Use this for initialization
    void Start () {


        Rigidbody2D rb2d = enemy.GetComponent<Rigidbody2D>();
        Animator animate = enemy.GetComponent<Animator>();


    }

   
	
	void Update () {



        if (numberOfEnemies == 0)
            spawnEnemies();

    }

    void spawnEnemies()
    {
        GameObject instance = Instantiate(enemy) as GameObject;
        numberOfEnemies++;
    }
    
}
