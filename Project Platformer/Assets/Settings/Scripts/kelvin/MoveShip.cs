using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveShip : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;

    public float speed;

    private float step;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, endPos, step);
        if (transform.position == endPos)
        {
            SceneManager.LoadScene("Startmeneu");
        }
    }
}
