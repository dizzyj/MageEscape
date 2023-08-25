using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public float rayDistance;
    public bool isChasing;
    private float timer = 0.0f;
    public float chaseDuration;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= chaseDuration) {
            isChasing = false;
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward) * rayDistance;
        Debug.DrawRay(transform.position, forward, Color.green);

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.gameObject.CompareTag("Player")) {
                timer = 0.0f;
                isChasing = true;
            }
        }
    }
}
