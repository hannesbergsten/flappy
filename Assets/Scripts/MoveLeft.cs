using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private GameObject _dragon;

    private const int OffsetLeftFromDragon = 1;
    private const int OffsetLeftUntilDestroy = 4;
    private const int PointValue = 10;
    private bool _pointGiven;

    [SerializeField] private float speed = 5f;

    private void Start()
    {
        _dragon = GameObject.Find("Dragon");
    }

    void Update()
    {
        // ABSTRACTION
        ScorePoints();
        DestroyObject();
        transform.Translate(Vector3.left * (Time.deltaTime * speed));
    }

    private void ScorePoints()
    {
        if (IsBehindDragonWithOffset(OffsetLeftFromDragon) && !_pointGiven)
            if (GameManager.Instance)
            {
                GameManager.Instance.AddScore(PointValue);
                _pointGiven = true;
            }
    }

    private void DestroyObject()
    {
        if (IsBehindDragonWithOffset(OffsetLeftUntilDestroy))
            Destroy(gameObject);
    }

    private bool IsBehindDragonWithOffset(int offsetLeft)
    {
        return _dragon is not null && 
               gameObject.transform.position.x < _dragon.transform.position.x - offsetLeft;
    }
}