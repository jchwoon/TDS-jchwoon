using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    TextMeshPro _text;
    [SerializeField]
    Color _hitFontColor;

    //private void Update()
    //{
    //    transform.rotation = Camera.main.transform.rotation;
    //}

    private void OnDisable()
    {
        _text.alpha = 0.0f;
    }

    public void SetInfo(float value)
    {
        _text = GetComponent<TextMeshPro>();
        transform.localPosition = new Vector2(0, 2);
        _text.alpha = 1.0f;
        _text.sortingOrder = 100;
        _text.fontSize = 3;
        _text.color = _hitFontColor;
        _text.text = $"{value}";
    }

    public void OnCompleteAnimation()
    {
        Manager.Resource.Destroy(gameObject);
    }
}
