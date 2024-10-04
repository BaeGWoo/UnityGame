using UnityEngine;

public interface AnimalInterface
{
    void Move(Vector3[] movePoints, Vector3[] moveDirections, Animator animator);
    void Attack();

}
