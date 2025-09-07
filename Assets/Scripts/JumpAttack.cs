using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : MonoBehaviour
{
    [Header("Settings")]
    public float attackDelay = 1f;
    public float revertDelay = 0.5f;
    public float animationLeadTime = 0.2f;

    [Header("Materials")]
    public Material originalMaterial;
    public Material smashMaterial;
    private LavaBounce lavaTerrain;

    [Header("Tags")]
    public string smashTag = "Lava";

    private string originalTag;         
    private Renderer rend;
    public Animator JumpToweranimator;

    void Awake()
    {
        rend = GetComponent<Renderer>();

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

        // Schianto: cambia materiale e tag
        if (rend != null && smashMaterial != null)
            rend.material = smashMaterial;

        gameObject.tag = smashTag;

        yield return new WaitForSeconds(revertDelay);

        // Ripristina materiale, tag e ferma animazione
        if (rend != null && originalMaterial != null)
            rend.material = originalMaterial;

        gameObject.tag = originalTag;

        if (JumpToweranimator != null)
            JumpToweranimator.SetBool("isAttacking", false);

        //looop
        TriggerSmash();
    }
}