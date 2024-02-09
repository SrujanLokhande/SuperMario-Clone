using UnityEngine;

public class Koopa : MonoBehaviour
{
    [SerializeField]
    private Sprite _shellSprite;

    //[SerializeField]
    //private float _destroyComponentInterval = 0.5f;

    [SerializeField]
    private float _shellSpeed = 12f;

    private bool _isInShell;
    private bool _isShellMoving;
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isInShell && collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (collision.transform.DotProduct(transform, Vector2.down))             // doing dot product to see if mario is on koopa's head
            {
                ShellAnimation();                                                       // if mario is on the top of koopa then goomba is goes into shell and slided
            }
            else
            {
                player.Hit();                                                       // if mario is not on top then mario decreses in size
            }
        }
        else if (!_isInShell && collision.gameObject.layer == LayerMask.NameToLayer("Shell"))       // this koopa is not in shell and is moving so it can be destroyed
        {
            Hit();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_isInShell && other.CompareTag("Player"))
        {
            if (!_isShellMoving)
            {
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f);
                PushShell(direction);
            }
            else
            {
                Player player = other.GetComponent<Player>();
                player.Hit();
            }
        }
        else if (!_isInShell && other.gameObject.layer == LayerMask.NameToLayer("Shell"))                       // when a koopa who is in shell collided with a koppa who is not in shell
        {
            Hit();
        }
    }

    private void ShellAnimation()                                                       // when the goomba is dead  make all its attributes to false
    {
        _isInShell = true;
        GetComponent<SpriteRenderer>().sprite = _shellSprite; 
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;

    }

    private void PushShell(Vector2 direction)                                   // mario pushes the shell when koopa is in shell
    {
        _isShellMoving = true;

        GetComponent<Rigidbody2D>().isKinematic = false;

        EntityMovement movement = GetComponent<EntityMovement>();
        movement._direction = direction.normalized;                                 // making the direction to be normalized so as to move it
        movement._speed = _shellSpeed;
        movement.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("Shell");                      // making a new shell layer to make it collide with other enemies

    }
    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;                         // when koopa gets hit disable the sprite
        GetComponent<DeathAnimation>().enabled = true;                           // when goomba gets hit enable the death animation
        Destroy(gameObject, 3f);                                                // destroy the koopa rigid body after 3 seconds
    }

    private void OnBecameInvisible()                                        // if koopa is in shell and out of camera it gets destroyed
    {
        if(_isShellMoving)
        {
            Destroy(gameObject);
        }
    }
}
