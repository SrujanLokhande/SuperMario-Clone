using UnityEngine;
using System.Collections;

public class BlockHit : MonoBehaviour
{
    [SerializeField]
    public int _maxHits = -1;

    [SerializeField]
    private Sprite _brokenblock;

    [SerializeField]
    private float _elapsedTime = 0f;

    [SerializeField]
    private float _animationDuration = 0.125f;

    [SerializeField]
    private GameObject _spriteItem;

    private bool _isAnimating;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isAnimating && _maxHits != 0 && collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.DotProduct(transform, Vector2.up))               // if mario hits the block while only moving up
            {
                Hit();
            }
        }
    }

    private void Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;                                              // making the hidden blocks to enable
        --_maxHits;                                                                 // making the blocks to be hit multiple times

        if (_maxHits == 0)
        {
            spriteRenderer.sprite = _brokenblock;                                   // changing the sprite of the block
        }

        if(_spriteItem != null)
        {
            Instantiate(_spriteItem, transform.position, Quaternion.identity);      // spawns  an item at the position of the block when we hit it          
        }

        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        _isAnimating = true;

        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 0.5f;

        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);

        _isAnimating = false;
    }

    private IEnumerator Move(Vector3 from, Vector3 to)                      // making the blocks to move from one position to another
    {
        while(_elapsedTime < _animationDuration)
        {
            float time = _elapsedTime / _animationDuration;

            transform.localPosition = Vector3.Lerp(from, to, time);
            _elapsedTime += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = to;
    }
}
