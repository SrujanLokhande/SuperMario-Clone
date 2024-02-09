using UnityEngine;
using System.Collections;

public class FlagPole : MonoBehaviour
{
    [SerializeField]
    private Transform _flag;

    [SerializeField]
    private Transform _poleBottom;

    [SerializeField]
    private Transform _castle;

    [SerializeField]
    private float _speed = 6f;

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(MoveTo(_flag, _poleBottom.position));                // making the flag to move to the bottom of the pole
            StartCoroutine(LevelCompleteSequence(other.transform));                // making the flag to move to the bottom of the pole
        }
    }

    private IEnumerator LevelCompleteSequence(Transform player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;              // disabling marios movement

        yield return MoveTo(player, _poleBottom.position);
        yield return MoveTo(player, player.position + Vector3.right);
        yield return MoveTo(player, player.position + Vector3.right + Vector3.down);            // making the mario move down once he hits the flag pole
        yield return MoveTo(player, _castle.position);                                          // making the mario to move towards the castle

        player.gameObject.SetActive(false);                                         // trigger the next level or gameOver Screen

        yield return new WaitForSeconds(2f);
        GameManager.Instance.GameOver();                                // loads the game over screen

    }

    private IEnumerator MoveTo(Transform subject, Vector3 destination)
    {
        while(Vector3.Distance(subject.position, destination) > 0.125f)
        {
            subject.position = Vector3.MoveTowards(subject.position, destination, _speed * Time.deltaTime);     // moving the flag
            yield return null;
        }

        subject.position = destination;
    }
}
