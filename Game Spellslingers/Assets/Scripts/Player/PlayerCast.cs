using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCast : MonoBehaviour
{
    private GameObject lightningObject;
    [SerializeField] private GameObject lightningBallPrefab;
    private Lightning lightning;
    private float rate;
    private WaitForSeconds wait;
    private float directionMagnitude;

    private void Start()
    {
        GameObject lightningPrefab = transform.GetChild(0).gameObject;
        this.lightningObject = Instantiate(lightningPrefab);
        this.lightningObject.transform.SetParent(gameObject.transform);
        this.lightningObject.SetActive(false);
        this.lightning = lightningObject.GetComponent<Lightning>();

        this.rate = 1.8f;
        this.wait = new WaitForSeconds(this.rate);
        this.directionMagnitude = 3.5f;
        StartCoroutine(Cast());
        GameObject lightningBall = Instantiate(this.lightningBallPrefab);
        lightningBall.transform.SetParent(Camera.main.transform);
    }

    private IEnumerator Cast()
    {
        while (true)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 playerPosition = gameObject.transform.position;
            Vector2 mouseDirection = mousePosition - playerPosition;
            mouseDirection.Normalize();
            float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg - 90;
            this.lightningObject.transform.rotation = Quaternion.Euler(0, 0, angle);
            this.lightningObject.transform.position = playerPosition + mouseDirection * this.directionMagnitude;
            this.lightningObject.SetActive(true);
            yield return this.wait;
        }
    }

    public void IncreaseRate(float secs)
    {
        this.rate -= secs;
        this.wait = new WaitForSeconds(this.rate);
    }

    public void SetLightningDamage(float damage)
    {
        this.lightning.Damage = damage;
    }

    public float GetLightningDamage()
    {
        return this.lightning.Damage;
    }
    
    public void IncreaseRange(float multiplier)
    {
        this.lightningObject.transform.localScale *= multiplier;
        this.directionMagnitude += 0.25f;
    }
}
