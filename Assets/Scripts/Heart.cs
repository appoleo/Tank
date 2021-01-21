using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{

    private SpriteRenderer sr;

    public Sprite brokenSprite;

    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        sr.sprite = brokenSprite;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
    }
}
