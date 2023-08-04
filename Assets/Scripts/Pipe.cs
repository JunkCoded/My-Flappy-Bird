using UnityEngine;

public class Pipe : MonoBehaviour
{
    private GameController controller;

    // Start is called before the first frame update
    void Start() {
        controller = GameObject.Find("Game Controller").GetComponent<GameController>();
    }

    void FixedUpdate() {
        if (controller.gameOver) return;

        Vector3 pos = transform.position;
        pos.x -= controller.movingSpeed;
        transform.position = pos;

        if (pos.x <= -6) Destroy(gameObject);
    }
}
