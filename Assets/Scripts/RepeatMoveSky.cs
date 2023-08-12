using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatMoveSky : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    [SerializeField] float skySpeed = 14f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * skySpeed * Time.deltaTime);
        if (transform.position.y < (startPos.y - repeatWidth * 2))
        {
            Debug.Log("startPos" + startPos);
            Debug.Log("repeatWidth " + repeatWidth);
            Debug.Log($"if {transform.position.y}" + " < " + (startPos.y - repeatWidth * 2));
            transform.position = startPos;
        }
    }
}
