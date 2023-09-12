using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorVisual : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float bounceSpeed = 10f;
    [SerializeField] private float bounceHeight = 0.5f;

    private float initY;

    void Start()
    {
        initY = gameObject.transform.localPosition.y;
    }
    
    // Update is called once per frame
    void Update()
    {
        float moveDir = Mathf.PingPong(Time.time * 0.1f * bounceSpeed, bounceHeight);

        // anchor visual will rotate and jump
        transform.Rotate(Vector3.up * 5f * rotationSpeed * Time.deltaTime);
        transform.localPosition.Set(transform.localPosition.x, initY + moveDir, transform.localPosition.z);

    }
}
