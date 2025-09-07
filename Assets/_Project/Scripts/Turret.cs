using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Turret : MonoBehaviour
{
    [Header("animation")]
    [SerializeField] private string paramIsShooting = "IsShooting";
    private Animator anim;
    [SerializeField] private float timerSyncro;

    [Header("References e Variables")]
    public Transform player;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float detectionRange = 10f;
    public float fireRate = 1f;
    public float firePower = 1f;
    private float fireCooldown = 0f;
    float velocita = 5f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (player == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * velocita);

            if (fireCooldown <= 0f)
            {
                Shoot();
                fireCooldown = 1f / fireRate;
            }
        }

        fireCooldown -= Time.deltaTime;
    }

    void Shoot()
    {
        anim.SetBool(paramIsShooting, true);
        StartCoroutine(HandleShooting());
    }
    IEnumerator HandleShooting()
    {
        fireCooldown = 1f / fireRate;

        yield return new WaitForSeconds(timerSyncro);

        var a = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        a.GetComponent<Rigidbody>().AddForce(transform.forward * firePower, ForceMode.Impulse);

        yield return new WaitForSeconds(0.1f);
        anim.SetBool(paramIsShooting, false);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}

