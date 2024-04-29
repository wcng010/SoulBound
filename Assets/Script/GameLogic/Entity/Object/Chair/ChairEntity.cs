using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairEntity : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;
    [SerializeField] private Sprite changeSprite; 
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
    }

    public void OnChange()
    {
        _spriteRenderer.sprite = changeSprite;
        _collider.size = new Vector2(2f,0.1f);

    }
}
