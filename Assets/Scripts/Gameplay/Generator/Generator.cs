using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [Header("Track Generation")]
    public Transform target;
    public Vector3 offset;
    [Min(1)]
    public int minLength, maxLength;
    [Space]
    [Header("Coin Generation")]
    public int minDistance;
    public int maxDistance;
    public FoliageGenerator foliageGenerator;
    public int foliageDistance;
    Vector3 currentPosition;
    int blockCount;
    int nextBlockToSpawnCoint = 6;
    int foliagePos;
    int foliageCount;
    Vector3 direction = Vector3.forward;
    void Start()
    {
        SpawnLine(3);
    }

    private void Update()
    {
        while (currentPosition.magnitude < (target.position + offset).magnitude)
        {
            SpawnChecker();
            SpawnRandomLine(minLength, maxLength);
        }
    }
    void ChangeDirection()
    {
        if (direction == Vector3.forward) direction = Vector3.right;
        else if (direction == Vector3.right) direction = Vector3.forward;
    }
    void SpawnRandomLine(int min, int max)
    {
        int Length = Random.Range(min, max);
        GameObject obj = ObjectPool.Instance.Spawn("Cube", currentPosition, Quaternion.LookRotation(direction));
        obj.transform.localScale = new Vector3(1, 1, Length);
        blockCount += Length - 1;
        while (blockCount >= foliagePos && foliageDistance > 0)
        {
            if (foliageCount < foliageGenerator.onScreenAmount)
            {
                for (int i = 0; i < System.Enum.GetNames(typeof(WorldType)).Length; i++)
                {
                    foliageGenerator?.SpawnFoliage(i, currentPosition + direction * (foliagePos - (blockCount - Length + 1.5f)));
                }
                foliageCount++;
            }
            else
                foliageGenerator?.SpawnFoliage((int)WorldManager.instance.currentType, currentPosition + direction * (foliagePos - (blockCount - Length + 1.5f)));
            foliagePos += foliageDistance;
        }
        while (blockCount >= nextBlockToSpawnCoint)
        {
            SpawnCoin(nextBlockToSpawnCoint - (blockCount - Length + 1));
            nextBlockToSpawnCoint += Random.Range(minDistance, maxDistance);
        }
        currentPosition += direction * (Length - 0.5f);
        ChangeDirection();

        currentPosition -= direction * 0.5f;
    }
    void SpawnLine(int length)
    {
        int c = Random.Range((int)0, 2);
        if (c == 0)
        {
            ChangeDirection();
            target.gameObject.GetComponent<CharacterControl>().SetDirection(direction);
        }
        SpawnRandomLine(length, length + 1);
    }
    void SpawnCoin(int pos)
    {
        ObjectPool.Instance.Spawn("Coin", currentPosition + Vector3.up * 0.85f + direction * (pos + 0.5f), Quaternion.identity);
    }
    void SpawnChecker()
    {
        GameObject obj = ObjectPool.Instance.Spawn("Checker", currentPosition, Quaternion.LookRotation(direction));
        obj.GetComponent<Checker>().GetId();
    }
}
