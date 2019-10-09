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
    Vector3 currentPosition;
    int blockCount;
    int nextBlockToSpawnCoint = 6;
    Vector3 direction = Vector3.forward;
    void Start()
    {
        SpawnLine(3);
    }

    private void Update()
    {
        while (currentPosition.magnitude < (target.position + offset).magnitude)
        {
            SpawnRandomLine();
        }
    }
    void ChangeDirection()
    {
        if (direction == Vector3.forward) direction = Vector3.right;
        else if (direction == Vector3.right) direction = Vector3.forward;
    }
    void SpawnRandomLine()
    {
        int Length = Random.Range(minLength, maxLength);
        for (int i = 0; i < Length; i++)
        {
            ObjectPool.Instance.Spawn("Cube", currentPosition, Quaternion.identity);
            blockCount++;
            currentPosition += direction;
            if (blockCount == nextBlockToSpawnCoint) SpawnCoin();

        }
        ChangeDirection();
    }
    void SpawnLine(int length)
    {
        int c = Random.Range((int)0, 2);
        if (c == 0)
        {
            ChangeDirection();
            target.gameObject.GetComponent<CharacterControl>().SetDirection(direction);
        }
        for (int i = 0; i < length; i++)
        {
            ObjectPool.Instance.Spawn("Cube", currentPosition, Quaternion.identity);
            blockCount++;
            currentPosition += direction;
        }
        ChangeDirection();
    }
    void SpawnCoin()
    {
        ObjectPool.Instance.Spawn("Coin", currentPosition + Vector3.up * 0.85f, Quaternion.identity);
        int distance = Random.Range(minDistance, maxDistance);
        nextBlockToSpawnCoint += distance;
    }
}
