using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject food;
    [SerializeField] private float xMax = 8;
    [SerializeField] private Sprite[] sprites;
    private GameObject newFood;
    
    void Start()
    {
        InvokeRepeating("SpawnFood", 0f, 1f);
    }

    void Update()
    {
        
    }

    private void SpawnFood() {
        Vector2 spawnPosition = new Vector2(Random.Range(-xMax, xMax), transform.position.y);
        newFood = Instantiate(food, spawnPosition, transform.rotation);
        newFood.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length - 1)];
    }

}
