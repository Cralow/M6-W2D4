using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    [SerializeField] private Transform colliderBox;
    [SerializeField]private ParticleSystem ps;
    [SerializeField] private float coolDown;
    [SerializeField] private float firetime;
    private bool isShooting;

    private void Awake()
    {
        StartCoroutine(ShootFlames());
    }
    private IEnumerator ShootFlames()
    {
        colliderBox.gameObject.SetActive(false);
        ps.Stop();
        yield return new WaitForSeconds(coolDown);
        colliderBox.gameObject.SetActive(true);
        ps.Play();
        yield return new WaitForSeconds(firetime);
        StartCoroutine(ShootFlames());
    }

    
}
