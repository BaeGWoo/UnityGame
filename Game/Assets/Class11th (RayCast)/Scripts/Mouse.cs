using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    [SerializeField] Ray ray;
    [SerializeField] RaycastHit rayCastHit;
    [SerializeField] LayerMask layerMask;

    [SerializeField] Texture2D texture2D;

    [SerializeField] IInteractable interactable;

    void Start()
    {
        Cursor.SetCursor(texture2D,new Vector2(0,0) ,CursorMode.Auto);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray,out rayCastHit,Mathf.Infinity,layerMask))
            {
                //Debug.Log(rayCastHit.collider.name);
                interactable = rayCastHit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                    interactable.Interact();      
            }
        }


       
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(ray.origin,ray.direction,Color.red,0.5f);
    }
}
