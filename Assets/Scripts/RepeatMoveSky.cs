using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatMoveSky : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatHeight;
    public float skySpeed = 14f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        repeatHeight = GetComponent<BoxCollider>().size.y / 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.down * skySpeed * Time.deltaTime);
        if (transform.position.y < (startPos.y - repeatHeight * 2))
        {
            transform.position = startPos;
        }
    }
}
