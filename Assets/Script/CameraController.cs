using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Grid grid;
    Camera main;

    [SerializeField]
    float camSpeed = 5f;
    float width;
    float height;


    void Start()
    {
        main = Camera.main;
        height = main.orthographicSize * 2;
        width = main.aspect * height;
        grid = FindObjectOfType<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(horizontal, vertical);

        main.transform.position += moveDir * camSpeed * Time.deltaTime;

        float curX = Mathf.Clamp(transform.position.x, grid.TopLeft.x + width / 2, (grid.TopLeft.x + grid.mapImg.width * grid.gridRadius * 2) - width / 2 );
        float curY = Mathf.Clamp(transform.position.y , (grid.TopLeft.y - grid.mapImg.height * grid.gridRadius * 2) + height / 2 , grid.TopLeft.y - height / 2);
    
        transform.position = new Vector3(curX, curY);
    }
}
