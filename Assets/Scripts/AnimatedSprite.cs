using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _sprites;

    [SerializeField]
    private float _frameRate = 1f / 6f;

    private SpriteRenderer _spriteRenderer;
    private int _frame;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        InvokeRepeating(nameof(Animate), _frameRate, _frameRate);           // invoke function every frame, using nameof if we want to rename
    }

    private void OnDisable()
    {
        CancelInvoke();                         // disables the active invoke
    }

    private void Animate()
    {
        ++_frame;

        if(_frame >= _sprites.Length)
        {
            _frame = 0;
        }

        if(_frame >=0 && _frame < _sprites.Length)
        {
             _spriteRenderer.sprite = _sprites[_frame];
        }
    }
}
