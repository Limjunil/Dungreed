using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    public Transform target;

    public float speed;

    public Vector2 center;
    public Vector2 size;


    [SerializeField]
    private float cameraHeight = default;
    private float cameraWidth = default;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2;

        cameraHeight = Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Screen.width / Screen.height;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position,
            Time.deltaTime * speed);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);



        // 카메라 이동 범위 제한 => 추후 재수정하기
        float largeX = size.x * 0.5f - cameraWidth;
        float clampX = Mathf.Clamp(transform.position.x,
            -largeX + center.x, largeX + center.x);


        float largeY = size.y * 0.5f - cameraHeight;
        float clampY = Mathf.Clamp(transform.position.y,
            -largeY + center.y, largeY + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
}
