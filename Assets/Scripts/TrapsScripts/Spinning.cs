using UnityEngine;
using System.Collections;
using Unity.VisualScripting;


public class Spinning : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 90f;
    [SerializeField] string playerString = "Player";

    private float currentY = 0f;

    void Start()
    {
        currentY = transform.eulerAngles.y;
    }

    void Update()
    {
        currentY += rotationSpeed * Time.deltaTime;
        currentY %= 360f;

        Vector3 euler = transform.eulerAngles;
        euler.y = currentY;
        transform.rotation = Quaternion.Euler(euler);
       
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(playerString))
        {
            Transform player = collision.transform;
            Quaternion originalRotation = player.rotation;

            player.SetParent(transform, true);

            player.rotation = originalRotation;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag(playerString))
        {
            Transform player = collision.transform;
            Quaternion originalRotation = player.rotation;

            player.SetParent(null, true);

            player.rotation = originalRotation;
        }
    }
}