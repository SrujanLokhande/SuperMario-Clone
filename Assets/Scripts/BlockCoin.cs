using UnityEngine;
using System.Collections;

public class BlockCoin : MonoBehaviour
{
    [SerializeField]
    private float _elapsedTime = 0f;

    [SerializeField]
    private float _animationDuration = 0.25f;

    private void Start()
    {
        GameManager.Instance.AddCoin();
        StartCoroutine(Animate());
    }
    private IEnumerator Animate()
    {     
        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 2f;       // making the coin move upwards when mario hits the block

        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);

        Destroy(gameObject);                                                // destroying the gameObject after the coin is collected
    }

    private IEnumerator Move(Vector3 from, Vector3 to)                      // making the blocks to move from one position to another
    {
        while (_elapsedTime < _animationDuration)
        {
            float time = _elapsedTime / _animationDuration;

            transform.localPosition = Vector3.Lerp(from, to, time);
            _elapsedTime += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = to;
    }
}
