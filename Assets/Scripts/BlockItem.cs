using System.Collections;
using UnityEngine;

public class BlockItem : MonoBehaviour
{

    [SerializeField]
    private float _elapsedTime = 0f;

    [SerializeField]
    private float _duration = 0.5f;
    
    void Start()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();                            // getting references to the components
        CircleCollider2D physicsCollider = GetComponent<CircleCollider2D>();
        BoxCollider2D triggerCollider = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        rigidbody.isKinematic = true;                                                   // setting the components to be false initially
        physicsCollider.enabled = false;
        triggerCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(0.25f);

        spriteRenderer.enabled = true;

        Vector3 startPosition = transform.localPosition;
        Vector3 endPosition = transform.localPosition + Vector3.up;

        while (_elapsedTime < _duration)                                                // making the sprites to come up
        {
            float t = _elapsedTime / _duration;

            transform.localPosition = Vector3.Lerp(startPosition, endPosition, t);           // using lerp between start and the end position
            _elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = endPosition;                                      // updates the position

        rigidbody.isKinematic = false;                                              // when the items are out they start moving so renable all the components
        physicsCollider.enabled = true;
        triggerCollider.enabled = true;     


    }
}
