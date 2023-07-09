using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public Team team = Team.None;
    protected Collider objectCollider;
    protected OutOfBoundsAction outOfBoundsAction = OutOfBoundsAction.None;

    void Start()
    {
        objectCollider = (Collider)transform.GetComponent(typeof(Collider));
    }

   
    protected void TurnToOther(Transform other) //horizontal only
    {
        TurnToPosition(other.position);
    }

    protected void TurnToPosition(Vector3 otherPos) //horizontal only
    {
        TurnToPosition(new Vector2(otherPos.x, otherPos.z));
    }

    protected void TurnToPosition(Vector2 otherPos)
    {
        Vector3 thisPos = transform.position;
        Vector2 difference = new Vector2(otherPos.x - thisPos.x, otherPos.y - thisPos.z);
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        rotationZ -= 90;
        transform.rotation = Quaternion.Euler(0.0f, -rotationZ, 0.0f);
    }

    protected Quaternion GetDirectionTowardsOther(Transform other)
    {
        Vector3 thisPos = transform.position;
        Vector2 difference = new Vector2(other.position.x - thisPos.x, other.position.z - thisPos.z);
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        rotationZ -= 90;
        return Quaternion.Euler(0.0f, -rotationZ, 0.0f);
    }
    protected Quaternion GetDirectionTowardsPoint(Vector3 point)
    {
        Vector3 thisPos = transform.position;
        Vector2 difference = new Vector2(point.x - thisPos.x, point.z - thisPos.z);
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        rotationZ -= 90;
        return Quaternion.Euler(0.0f, -rotationZ, 0.0f);
    }

}
