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
        int pos = 0;
        while (pos < Length)
        {
            pos += Random.Range(minDistance, maxDistance);
            if (pos < Length)
                SpawnCoin(pos);
        }
        //blockCount++;
        currentPosition += direction * (Length - 0.5f);
        //if (blockCount == nextBlockToSpawnCoint) SpawnCoin();
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
}
