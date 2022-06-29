using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCast : MonoBehaviour
{
    private GameObject lightning;
    private float rate;
    private WaitForSeconds wait;

    private void Start()
    {
        GameObject lightningPrefab = transform.GetChild(0).gameObject;
        this.lightning = Instantiate(lightningPrefab);
        this.lightning.transform.SetParent(gameObject.transform);
        this.lightning.SetActive(false);

        this.rate = 1.8f;
        this.wait = new WaitForSeconds(this.rate);
        StartCoroutine(Cast());
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
            this.lightning.transform.rotation = Quaternion.Euler(0, 0, angle);
            this.lightning.transform.position = playerPosition + mouseDirection * 3.5f;
            this.lightning.SetActive(true);
            yield return this.wait;
        }
    }
}
