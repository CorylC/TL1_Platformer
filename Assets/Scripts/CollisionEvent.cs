using UnityEngine;
using UnityEngine.Events;

public class CollisionEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent _collisionEvent;
    [SerializeField] private string _collideTag = "Player";

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(_collideTag))
        {
            _collisionEvent.Invoke();
           gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_collideTag))
        {
            _collisionEvent.Invoke();
        }
    }
}
