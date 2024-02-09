using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Type                                        // enum for different powerup types
    {
        Coin,
        ExtraLife,
        MagicMushroom,
        Starpower
    }

    [SerializeField]
    private Type _powerUpType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Collect(other.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        switch(_powerUpType)                                        // doing a switch on the powerup type
        {
            case Type.Coin:
                GameManager.Instance.AddCoin();                     // incrementing the coins
                break;
            case Type.ExtraLife:
                GameManager.Instance.AddLife();                     // adding more lives
                break;
            case Type.MagicMushroom:
                player.GetComponent<Player>().Grow();
                break;
            case Type.Starpower:
                break;
        }
        Destroy(gameObject);                            // destroy the game object when it gets collected                      
    }
}
