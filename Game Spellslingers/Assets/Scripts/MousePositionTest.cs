using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionTest : MonoBehaviour
{
    private Vector3 target;
    public GameObject playerObject;
    // Start is called before the first frame update
    void Start()
    {
        this.playerObject = Player.instance.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, transform.position.z));

        Vector3 direction = target - playerObject.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    }
}
