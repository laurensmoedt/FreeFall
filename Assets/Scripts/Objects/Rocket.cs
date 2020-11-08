using UnityEngine;

public class Rocket : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Translate(0, 60 * Time.deltaTime, 0, Space.Self);

        DestroyObject();
    }

    private void DestroyObject()
    {
        //Destroy object when position is higher than the camera position
        if (transform.position.y > Camera.main.transform.position.y)
        {
            Destroy(gameObject);
        }
    }
}
