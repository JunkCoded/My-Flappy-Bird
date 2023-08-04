using UnityEngine;

public class Bird : MonoBehaviour
{
    private GameController controller;

    // Start is called before the first frame update
    void Start() {
        controller = GameObject.Find("Game Controller").GetComponent<GameController>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Deadly") controller.GameEnd();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "pipe") controller.AddScore();
    }
}
