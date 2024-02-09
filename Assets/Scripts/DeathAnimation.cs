using UnityEngine;
using System.Collections;

public class DeathAnimation : MonoBehaviour
{
    [SerializeField]
    private Sprite _deadSprite;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private float _elapsedTime = 0f;

    [SerializeField]
    private float _duration = 3f;

    [SerializeField]
    private float _jumpVelocity = 10f;

    [SerializeField]
    private float _gravity = -36f;

    private void Reset()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        UpdateSprite();
        DisablePhysics();        
        StartCoroutine(Animate());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void UpdateSprite()
    {
        _spriteRenderer.enabled = true;
        _spriteRenderer.sortingOrder = 10;                  // to make sure they are always on top when they die

        if(_deadSprite != null)
        {
            _spriteRenderer.sprite = _deadSprite;
        }
    }

    private void DisablePhysics()                       // disable physics and collider after the enemies are dead
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
        GetComponent<Rigidbody2D>().isKinematic = true;

        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        EntityMovement entityMovement = GetComponent<EntityMovement>();

        if(playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        if(entityMovement != null)
        {
            entityMovement.enabled = false;
        }
    }

    private IEnumerator Animate()
    {
        Vector3 velocity = Vector3.up * _jumpVelocity;

        while (_elapsedTime < _duration)
        {
            transform.position += velocity * Time.deltaTime;
            velocity.y += _gravity * Time.deltaTime;
            _elapsedTime += Time.deltaTime;
            yield return null;
        }

    }
}
