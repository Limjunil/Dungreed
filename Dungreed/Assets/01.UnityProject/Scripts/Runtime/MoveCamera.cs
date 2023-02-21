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


    public void SetCameraRange(string dungeonNumer)
    {
        switch (dungeonNumer)
        {
            case "Town":
                // 마을의 카메라 사이즈

                break;

            case "DungeonInGrid":

                // 던전 입구의 카메라 사이즈
                center.x = 5.5f;
                center.y = 2.5f;

                size.x = 21f;
                size.y = 10f;

                break;

            case "Dungeon1Grid":
                // 던전 1의 카메라 사이즈
                center.x = 5.5f;
                center.y = 3f;

                size.x = 21f;
                size.y = 12f;

                break;

            case "Dungeon2Grid":
                // 던전 2의 카메라 사이즈
                center.x = 10f;
                center.y = -1f;

                size.x = 30f;
                size.y = 18f;

                break;

            case "DungeonBossGateGrid":
                // 보스방 입장의 카메라 사이즈
                center.x = 9f;
                center.y = 6f;

                size.x = 30f;
                size.y = 17f;

                break;

            case "DungeonBossInGrid":
                // 스테이지 1 보스방 진입 전 방의 카메라 사이즈
                center.x = 9f;
                center.y = 2f;

                size.x = 30f;
                size.y = 10f;

                break;

            case "DungeonBoss1Grid":
                // 스테이지 1 보스방의 카메라 사이즈
                center.x = 10f;
                center.y = 8f;

                size.x = 30f;
                size.y = 21f;
                break;

            default:
                break;
        }
    }
}
