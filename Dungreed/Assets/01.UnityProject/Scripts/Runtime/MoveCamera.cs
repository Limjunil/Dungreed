using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
        GameObject gameObjs_ = GFunc.GetRootObj("GameObjs");
        target = gameObjs_.FindChildObj("Player").transform;

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
        

    public void SetCameraRange(int dungeonNumer)
    {
        switch (dungeonNumer)
        {
            
            case 0:

                // 던전 입구의 카메라 사이즈 00번째
                center.x = 4f;
                center.y = 3.5f;

                size.x = 20f;
                size.y = 12f;

                break;

            case 1:
                // 던전 1의 카메라 사이즈 01번째
                center.x = 5f;
                center.y = 3.5f;

                size.x = 22f;
                size.y = 12f;

                break;

            case 2:
                // 던전 2의 카메라 사이즈 02번째
                center.x = 10f;
                center.y = -1f;

                size.x = 30f;
                size.y = 18f;

                break;

            case 3:
                // 보스방 입장의 카메라 사이즈 03번째
                center.x = 9f;
                center.y = 4.5f;

                size.x = 28f;
                size.y = 14f;

                break;

            case 4:
                // 스테이지 1 보스방 진입 전 방의 카메라 사이즈 04번째
                center.x = 9f;
                center.y = 2f;

                size.x = 28f;
                size.y = 9f;

                break;

            case 5:
                // 스테이지 1 보스방의 카메라 사이즈 05번째
                center.x = 10f;
                center.y = 8f;

                size.x = 30f;
                size.y = 21f;
                break;


            case 6:
                // 엔딩의 카메라 사이즈
                center.x = 4f;
                center.y = 3.3f;

                size.x = 18f;
                size.y = 10f;

                break;

            case 7:
                // 마을의 카메라 사이즈

                break;
        }
    }
}
