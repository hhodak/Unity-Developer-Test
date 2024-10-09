using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    RaycastHit hit;
    PlayerMovement playerMovement;
    [SerializeField] LayerMask mask;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, mask))
            {
                switch (hit.transform.tag)
                {
                    case "Ground":
                        playerMovement.MoveToLocation(hit.point);
                        break;
                    case "NPC":
                        playerMovement.AttackTarget(hit.transform);
                        break;
                    default: break;
                }
            }
        }
    }
}
