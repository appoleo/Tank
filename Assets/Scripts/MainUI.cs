using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{

    public float moveSpeed = 200f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.up * moveSpeed * Time.deltaTime, Space.World);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 432, 0), moveSpeed * Time.deltaTime);
    }
}
