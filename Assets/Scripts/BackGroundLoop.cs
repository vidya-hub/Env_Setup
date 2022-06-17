using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    public GameObject backGround;
    private GameObject player;
    public float distanceFactor = 5f;
    private Camera mainCamera;
    private Vector2 screenBounds;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // TODO: get the player postion 
            Vector2 playerPosition = player.transform.position;

            // TODO: get the width of the background and find the right edge position 
            RectTransform rt = backGround.GetComponent<RectTransform>();
            float width = rt.rect.width;
            float height = rt.rect.height;
            Vector2 backgroundPosition = new Vector2(width, height);

            // TODO: find the right edge of the camera
            float cameraEdgePosition = getCameraEdgePoint(mainCamera.transform.position.x);

            // TODO: find the distance between right edge and mainCamera Edge position
            float distance = backgroundPosition.x - cameraEdgePosition;
            Debug.Log(distance);

            // TODO: compare the distance with distance factor 
            // TODO: if the distance is > df and distance > 0 - display single  
            // TODO: if the distance is < df or distance <= 0 - copy and place that game object on right side
        }
        float getCameraEdgePoint(float camMidPoint)
        {
            float camHeight = 2f * mainCamera.orthographicSize;
            float camWidth = camHeight * mainCamera.aspect;
            return (camMidPoint + camWidth / 2);
        }
    }
}
