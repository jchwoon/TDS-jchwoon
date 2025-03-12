using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    void Update()
    {
        Vector2 mouseWorldPos = GetMouseWorldPos();
        Vector2 gunDirection = mouseWorldPos - (Vector2)transform.position;

        float angle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;
        //��������Ʈ �̹��� ������ó -40��
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 33));
    }

    private Vector2 GetMouseWorldPos()
    {
        Vector2 mouseScreenPosition = Input.mousePosition;

        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }
}
