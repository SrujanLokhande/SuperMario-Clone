using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private PlayerMovement _movement;

    [SerializeField]
    private Sprite _idle;

    [SerializeField]
    private Sprite _jump;

    [SerializeField]
    private Sprite _slide;

    [SerializeField]
    private AnimatedSprite _running;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();                   // gets the sprites and playermovement
        _movement = GetComponentInParent<PlayerMovement>();
    }

    private void OnEnable()
    {
        _spriteRenderer.enabled = true;             
    }

    private void OnDisable()
    {
        _spriteRenderer.enabled = false;
        _running.enabled = false;
    }

    private void Update()                               // sets the sprites according to the movement of mario
    {
        _running.enabled = _movement._isRunning;
        if(_movement._isJumping)
        {
            _spriteRenderer.sprite = _jump;
        }
        else if (_movement._isSliding)
        {
            _spriteRenderer.sprite = _slide;
        }
        else if (!_movement._isRunning)  
        {
            _spriteRenderer.sprite = _idle;
        }
    }
}
