using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boarder : MonoBehaviour
{
    private Vector2 screenBoarder;
    // Start is called before the first frame update
    void Start()
    {
        screenBoarder = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBoarder.x, screenBoarder.x * -1);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBoarder.y, screenBoarder.y * -1);
        transform.position = viewPos;
    }
}
