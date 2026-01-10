using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject enemyPrefab;

    [Header("Spawn Points (optional, will auto-generate if empty)")]
    public Transform[] sps;

    [Header("Legacy Settings")]
    public bool randomizeIfNoPoints = true;
    public float spawnRadiusIfNoPoints = 7f;

    int _idx = 0;

    void Awake()
    {
        if (enemyPrefab == null)
        {
            var anyEnemy = GameObject.FindGameObjectWithTag(GlobalVars.EnemyTag);
            if (anyEnemy != null) enemyPrefab = anyEnemy;
        }

        if (sps == null || sps.Length == 0)
        {
            sps = new Transform[4];
            for (int i = 0; i < sps.Length; i++)
            {
                var go = new GameObject("SpawnPoint_" + i);
                go.transform.position = i switch
                {
                    0 => new Vector3(8, 0, 0),
                    1 => new Vector3(-8, 0, 0),
                    2 => new Vector3(0, 8, 0),
                    _ => new Vector3(0, -8, 0),
                };
                sps[i] = go.transform;
            }
        }
    }

    // Called via SendMessage from GameStuff
    public void SpawnOneEnemy()
    {
        if (enemyPrefab == null) return;

        Vector3 pos = getspawnposition_legacy();
        var go = Instantiate(enemyPrefab, pos, Quaternion.identity);

        go.tag = GlobalVars.EnemyTag;

        var enemy = go.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.game = FindObjectOfType<GameStuff>();
        }
    }

    Vector3 getspawnposition_legacy()
    {
        if (sps != null && sps.Length > 0)
        {
            _idx++;
            if (_idx >= sps.Length) _idx = 0;

            if (sps[_idx] != null)
                return sps[_idx].position;
        }

        if (randomizeIfNoPoints)
        {
            var r = Random.insideUnitCircle.normalized * spawnRadiusIfNoPoints;
            return new Vector3(r.x, r.y, 0f);
        }

        return Vector3.zero;
    }
}
