using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCrabbot : Enemy
{

    [SerializeField] private GameObject loot;
    private bool isCasting;
    private int prev;
    private float resetDistance;
    private Animator anim;

    #region Combo attack declarations

    [SerializeField] private Transform slamPos1;
    [SerializeField] private Transform slamPos2;
    [SerializeField] private Transform slashPos;
    [SerializeField] private float slamRadius;
    [SerializeField] private float slashRadius;
    [SerializeField] private LayerMask playerLayer;

    #endregion

    public override void Awake()
    {
        base.Awake();
        resetDistance = 35;
        this.anim = GetComponent<Animator>();
        Vector2 spawnPosition = new Vector2(-69, 243);
        gameObject.transform.position = spawnPosition;
        AudioManager.instance.Play("CrabbotSpawn");
    }

    private void Update()
    {
        StartCasting();
    }

    private void StartCasting()
    {
        if (!isCasting && (gameObject.transform.position - Player.instance.transform.position).sqrMagnitude <= resetDistance)
        {
            isCasting = true;
            int skill = Random.Range(0, 3);
            while (skill == this.prev)
            {
                skill = Random.Range(0, 3);
            }
            // Debug.Log("skill chosen" + skill);
            switch (skill)
            {
                case 0:
                    CastTunnel();
                    break;
                case 1:
                    CastComboSlam();
                    break;
                case 2:
                    CastBulletRain();
                    break;
            }
            this.prev = skill;
        }
    }

    private void CastTunnel()
    {
        this.anim.SetBool("Tunnel", true);
    }

    private void CastComboSlam()
    {
        this.anim.SetBool("Slam", true);
    }

    private void CastBulletRain()
    {
        this.anim.SetBool("EyeGlow", true);
    }

    private void Slash()
    {
        Collider2D[] damagedPlayer = Physics2D.OverlapCircleAll(slashPos.position, slashRadius, playerLayer);
        if (damagedPlayer.Length > 0)
        {
            if (damagedPlayer[0].CompareTag("Player"))
            {
                Player.instance.TakeDamage(50f);
            }
        }
    }
    private void Slam()
    {
        AudioManager.instance.Play("CrabbotSlam");
        Collider2D[] slamZone1 = Physics2D.OverlapCircleAll(slamPos1.position, slamRadius, playerLayer);
        Collider2D[] slamZone2 = Physics2D.OverlapCircleAll(slamPos2.position, slamRadius, playerLayer);
        if (slamZone1.Length > 0)
        {
            if (slamZone1[0].CompareTag("Player"))
            {
                Player.instance.TakeDamage(50f);                
            }
        }
        if (slamZone2.Length > 0)
        { 
            if (slamZone2[0].CompareTag("Player"))
            {
                Player.instance.TakeDamage(50f);
            }
        }
    }

    public override void Die()
    {
        AudioManager.instance.Play("CrabbotDeath");
        GameObject loot = Instantiate(this.loot);
        loot.transform.position = this.transform.position;
        // play death animation
        Destroy(gameObject);
    }
    public void AnimEnd()
    {
        this.isCasting = false;
    }

    public void SlamEnd()
    {
        this.isCasting = false;
        this.anim.SetBool("Slam", false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(slamPos1.position, slamRadius);
        Gizmos.DrawWireSphere(slamPos2.position, slamRadius);
    }
    public override IEnumerator HandleStatusEffect(StatusEffect statusEffect)
    {
        if (statusEffect is Slow || statusEffect is Stun)
        {
            yield return null;
        } else
        {
            yield return base.HandleStatusEffect(statusEffect);
        }
    }
}
