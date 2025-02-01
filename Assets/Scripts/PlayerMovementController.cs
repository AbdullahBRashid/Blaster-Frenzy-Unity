using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    bool canMove = false;
    public void SetMove(bool canMove) {
        this.canMove = canMove;
    }
    

    void Update()
    {

        if (canMove) {
            // Take to mouse x position when clicking
            if (Input.GetMouseButton(0)) {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(mousePos.x, transform.position.y, transform.position.z);
            }

            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                transform.position = new Vector3(touchPos.x, transform.position.y, transform.position.z);
            }
        }
    }
}
