using UnityEngine;

public class BaseMoving : MonoBehaviour
{
    private GameController controller;

    // Start is called before the first frame update
    void Start() {
        controller = GameObject.Find("Game Controller").GetComponent<GameController>();
    }

    void FixedUpdate()
    {
        if (controller.gameOver) return;

        foreach (Transform child in transform) {
            Vector3 childPos = child.position;

            childPos.x -= controller.movingSpeed;
            child.position = childPos;

            if (childPos.x <= -6.72f) {
                childPos.x = 6.72f - controller.movingSpeed;
                child.position = childPos;
            }
        }
    }
}
