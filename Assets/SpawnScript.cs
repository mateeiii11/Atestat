using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] GameObject[] obj;
    float spawnerTime1 = 0f, spawnerTime2 = 3f, spawnerTime3 = 5f;
    public float spawnerDelay1, spawnerDelay2, spawnerDelay3;
    public float radius;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= spawnerTime1)
        {
            spawnerTime1 = Time.time + spawnerDelay1;
            SpawnEnemy(0);
        }

        if (Time.time >= spawnerTime2)
        {
            spawnerTime2 = Time.time + spawnerDelay2;
            SpawnEnemy(1);
        }

        if (Time.time >= spawnerTime3)
        {
            spawnerTime3 = Time.time + spawnerDelay3;
            SpawnEnemy(2);
        }
    }
    private void SpawnEnemy(int index)
    {
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        Vector2 positionOnCircle = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        GameObject EnemyIns = Instantiate(obj[index], positionOnCircle, Quaternion.identity);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, radius);
    }
}
