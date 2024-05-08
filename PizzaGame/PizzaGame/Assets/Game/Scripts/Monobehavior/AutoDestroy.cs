using System.Collections;
using UnityEngine;


public class AutoDestroy : MonoBehaviour
{
    [Header("Config life time")]
    [Range(0, 10f)][SerializeField] float lifeTime = 1f;

    // fields
    float _count;

    // Use this for initialization
    void Start()
    {
        _count = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        _count -= Time.deltaTime;
        if (_count <= 0)
        {
            Destroy(gameObject);
        }
    }
}
