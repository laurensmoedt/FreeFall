using UnityEngine;

public class Coin : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Rotate(90 * Time.deltaTime, 0, 0);

        //Destroy object when position is higher than the camera position
        if (transform.position.y > Camera.main.transform.position.y)
        {
            DestroyObject();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Player>().coinCount++;
            FindObjectOfType<AudioManager>().Play("CoinPickup");
            Destroy(gameObject);
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
