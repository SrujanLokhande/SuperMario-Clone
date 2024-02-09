using UnityEngine;

public class Goomba : MonoBehaviour
{
    [SerializeField]
    private Sprite _flatSprite;

    [SerializeField]
    private float _destroyComponentInterval = 0.5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if(collision.transform.DotProduct(transform, Vector2.down))             // doing dot product to see if mario is on goomba's head
            {
                FlatGoomba();                                                       // if mario is on the top of goomba then goomba is deadd
            }
            else
            {
                player.Hit();                                                       // if mario is not on top then mario decreses in size
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }

    private void FlatGoomba()                                                       // when the goomba is dead destroy the component and make all its attributes to false
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = _flatSprite;
        Destroy(gameObject, _destroyComponentInterval);
    }

    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;                         // when goomba gets hit by koopa disable the sprite
        GetComponent<DeathAnimation>().enabled = true;                           // when goomba gets hit by koopa enable the death animation
        Destroy(gameObject, 3f);                                                // destroy the koopa rigid body after 3 seconds
    }


}
