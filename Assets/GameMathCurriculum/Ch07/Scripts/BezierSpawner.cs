using UnityEngine;

public class BezierSpawner : MonoBehaviour
{
    public GameObject spherePrefab; //구체
    public Transform startPoint; //구체 스폰 시작점
    public Transform endPoint; // 구체 도착점

    public float controlPointRange = 5f; //곡선의 휘어짐 정도 

    public float minDuration = 1f; // 최소 이동시간
    public float maxDuration = 5f; // 최대이동시간

    public Material[] materials; //머테리얼 배열 (구체의 색깔종류 배열)

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //스페이스바를 누를때 스폰메서드 실행
        {
            Spawn();
        }
    }

    void Spawn() //본격적인 구체 생성 메서드
    {
        int count = Random.Range(3, 10); //구체 생성 갯수의 범위를 랜덤하게 지정
        for (int i = 0; i < count; i++) //랜덤범위수에 따른 반복횟수로 구체의 랜덤한 갯수만큼생성
        {
            GameObject obj = Instantiate(spherePrefab, startPoint.position, Quaternion.identity);

            // 랜덤 제어점 생성
            Vector3 p1 = startPoint.position + Random.insideUnitSphere * controlPointRange;
            Vector3 p2 = endPoint.position + Random.insideUnitSphere * controlPointRange;

            // 랜덤 이동 시간 랜덤범위를 미리선언한 duration값으로 정하기
            float duration = Random.Range(minDuration, maxDuration);

            // Mover 설정
            BezierMover mover = obj.GetComponent<BezierMover>();
            mover.Init(startPoint.position, p1, p2, endPoint.position, duration);

            // 랜덤 머티리얼
            if (materials.Length > 0)
            {
                Renderer r = obj.GetComponent<Renderer>();
                r.material = materials[Random.Range(0, materials.Length)];
            }
        }
    }
}