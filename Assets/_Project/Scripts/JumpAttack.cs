using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : MonoBehaviour
{
    [Header("References")]
    private LavaBounce lavaTerrain;
    private Rigidbody rb;
    private Renderer rend;

    [Header("Settings")]
    public float attackDelay = 1f;
    public float revertDelay = 0.5f;
    public float animationLeadTime = 0.2f;

    [Header("Materials")]
    public Material originalMaterial;
    public Material smashMaterial;

    [Header("Tags")]
    public string smashTag = "Lava";

    private string originalTag;         
    public Animator JumpToweranimator;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();

        if (originalMaterial == null && rend != null)
            originalMaterial = rend.material;

        originalTag = gameObject.tag;

        TriggerSmash();
    }

    public void TriggerSmash()
    {
        StartCoroutine(StartSmashAttack());
    }

    private IEnumerator StartSmashAttack()
    {
        // Attiva animazione
        if (JumpToweranimator != null)
        {
            float waitBeforeAnim = Mathf.Max(attackDelay - animationLeadTime, 0f);
            yield return new WaitForSeconds(waitBeforeAnim);
            JumpToweranimator.SetBool("isAttacking", true);

            //tempo rimanente per sincronizzare lo schianto
            yield return new WaitForSeconds(attackDelay - waitBeforeAnim);
        }
        else
        {
            yield return new WaitForSeconds(attackDelay);
        }

        // Schianto: cambia materiale e tag e rigidbody collision detection
        if (rend != null && smashMaterial != null)
        {
            rend.material = smashMaterial;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }

        gameObject.tag = smashTag;

        yield return new WaitForSeconds(revertDelay);

        // Ripristina materiale, tag e ferma animazione reimposta rigidbody collision detection
        if (rend != null && originalMaterial != null)
        {
            rend.material = originalMaterial;
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        }

        gameObject.tag = originalTag;

        if (JumpToweranimator != null)
            JumpToweranimator.SetBool("isAttacking", false);

        //looop
        TriggerSmash();
    }
}