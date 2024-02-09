using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerSpriteRenderer _smallRenderer;

    [SerializeField]
    private PlayerSpriteRenderer _bigRenderer;

    [SerializeField]
    private float _elapsedTime = 0f;

    [SerializeField]
    private float _duration = 0.5f;

    private PlayerSpriteRenderer _activeRenderer;
    private CapsuleCollider2D _capsuleCollider;
    
    public DeathAnimation _deathAnimation { get; private set; }

    public bool big => _bigRenderer.enabled;
    public bool small => _smallRenderer.enabled;
    public bool dead => _deathAnimation.enabled;

    private void Awake()
    {
        _deathAnimation = GetComponent<DeathAnimation>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    public void Hit()
    {
        if (big)
        {
            Shrink();                       // making the mario shrink if he hits the enemy
        }
        else
        {
            Death();
        }
    }

    private void Shrink()                                       // make the mario shrink in size
    {
        _smallRenderer.enabled = true;
        _bigRenderer.enabled = false;
        _activeRenderer = _smallRenderer;

        _capsuleCollider.size = new Vector2(1f, 1f);            // making the capsule collider size to decrease when mario is small
        _capsuleCollider.offset = new Vector2(0f, 0f);

        StartCoroutine(MarioAnimation());

    }

    public void Grow()                                          // to make the mario grow by enabling the big sprite renderer
    {
        _smallRenderer.enabled = false;
        _bigRenderer.enabled = true;
        _activeRenderer = _bigRenderer;

        _capsuleCollider.size = new Vector2(1f, 2f);            // making the capsule collider size to increase when mario is big
        _capsuleCollider.offset = new Vector2(0f, 0.5f);

        StartCoroutine(MarioAnimation());
    }

    private void Death()
    {
        _smallRenderer.enabled = false;
        _bigRenderer.enabled = false;
        _deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(3f);                    // resets the level after 3 seconds
    }

    private IEnumerator MarioAnimation()
    {
        while (_elapsedTime < _duration)                                                // making the sprites to come up
        {
            _elapsedTime += Time.deltaTime;

            if(Time.frameCount % 4 == 0)                        // making an animation of alternating grow or shrink in size every 4 frame
            {
                _smallRenderer.enabled = !_smallRenderer.enabled;
                _bigRenderer.enabled = !_smallRenderer.enabled;
            }          
            
            yield return null;
        }

        _smallRenderer.enabled = false;                         // disabling both the mario form but renabling immedietly
        _bigRenderer.enabled = false;
        _activeRenderer.enabled = true;
    }

}
