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
        //스프라이트 이미지 보정수처 -40도
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 33));
    }

    private Vector2 GetMouseWorldPos()
    {
        Vector2 mouseScreenPosition = Input.mousePosition;

        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }
}
