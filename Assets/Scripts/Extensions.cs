using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions 
{
    [SerializeField]
    static float radius = 0.25f;

    [SerializeField]
    static float distance = 0.37f;

    private static LayerMask _layerMask = LayerMask.GetMask("Default");

    public static bool CircleCast(this Rigidbody2D _RigidBody, Vector2 direction)
    {
        if(_RigidBody.isKinematic)
        {
            return false;
        }
        RaycastHit2D hit = Physics2D.CircleCast(_RigidBody.position, radius, direction.normalized, distance, _layerMask);      // doing a circle cast
        return hit.collider != null && hit.rigidbody != _RigidBody;                             // making sure the circle cast is not colliding with mario collider
    }

    public static bool DotProduct(this Transform _transform, Transform _other, Vector2 _testDirection)
    {
        Vector2 direction = _other.position - _transform.position;                          // getting direction from mario to block or whatever is above mario
        return Vector2.Dot(direction.normalized, _testDirection) > 0.25f;                   // if do product is lower than 0.25 they are almost in the same direction
    }
}
