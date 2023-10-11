using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private MazeGenerator _mazeGenerator;
    
    [SerializeField]
    private float playerSpeed = 1;
    private int multiplier = 6;
    // Start is called before the first frame update
    void Start()
    {
        _mazeGenerator = GameObject.Find("MazeGenerator").GetComponent<MazeGenerator>();
        transform.position = new Vector3(Random.Range(0, _mazeGenerator._mazeWidth), 0, Random.Range(0, _mazeGenerator._mazeDepth));
        transform.position *= multiplier;
        //spawn at random location
    }

    // Update is called once per frame
    void Update()
    {
        //get the Input from Horizontal axis
        float horizontalInput = Input.GetAxis("Horizontal");
        //get the Input from Vertical axis
        float verticalInput = Input.GetAxis("Vertical");

        //update the position
        transform.position = transform.position + new Vector3(horizontalInput * playerSpeed * Time.deltaTime, 0, verticalInput * playerSpeed * Time.deltaTime);

    }
}
