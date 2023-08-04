using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header ("Bird Settings")]

    public GameObject bird;
    [SerializeField] private int jumpForce = 3;

    [Header ("Sound Settings")]
    [SerializeField] private AudioSource soundController;
    [SerializeField] private AudioClip wingSound;
    [SerializeField] private AudioClip scoreUpSound;
    [SerializeField] private AudioClip dieSound;

    [Header ("Pipe Settings")]

    public GameObject pipePrefab;
    [SerializeField] private float pipeDelay = 2;
    private float pipeRemainTime;

    [Header ("UI Settings")]
    [SerializeField] private GameObject startImage;
    [SerializeField] private GameObject gameOverImage;
    [SerializeField] private TMP_Text scoreText;

    [Header ("Other Settings")]

    public float movingSpeed = 0.05f;
    [SerializeField] private GameObject baseObjects;
    [SerializeField] private GameObject backgroundNight;
    [SerializeField] private GameObject backgroundDay;

    private int score = 0;

    private bool gameStarted = false;
    public bool gameOver = false;

    private Rigidbody2D birdRB;
    private Animator birdAnim;

    // Start is called before the first frame update
    void Start()
    {
        pipeRemainTime = pipeDelay;
        
        birdRB = bird.GetComponent<Rigidbody2D>();
        birdAnim = bird.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (gameStarted && !gameOver) {
            bird.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(birdRB.velocity.y, 5) * Mathf.Rad2Deg); // Направление птички в сторону полета

            pipeRemainTime -= Time.deltaTime; // Таймер на спавн труб
            if (pipeRemainTime <= 0) {
                SpawnPipe();
                pipeRemainTime = pipeDelay;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            if (!gameStarted) {
                startImage.SetActive(false);
                gameStarted = true;

                birdRB.simulated = true;
                birdAnim.enabled = true;
            }

            if (gameOver) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            } else {
                birdRB.velocity = new Vector2(0, jumpForce);
                soundController.PlayOneShot(wingSound, 0.5f);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    private void SpawnPipe() {
        float randomY = Random.Range(-0.7f, 1.5f);
        Vector2 pos = new Vector2(6, randomY);
        GameObject newPipe = Instantiate(pipePrefab, pos, Quaternion.identity);
        newPipe.name = "pipe";
    }

    public void AddScore() {
        score++;

        scoreText.text = "";
        foreach (int num in NumSplit(score)) {
            scoreText.text += "<sprite name=\"" + num.ToString() + "\">";
        }

        soundController.PlayOneShot(scoreUpSound, 0.5f);
    }

    private int[] NumSplit(int num) { // code from stackoverflow
        int[] a = new int[num.ToString().Length];

        for (int i = 0; i < a.Length; i++) {
            a[i] = num % 10;
            num /= 10;
        }

        System.Array.Reverse(a);
        return a;
    }

    public void GameEnd() {
        if (gameOver) return;

        gameOver = true;

        gameOverImage.SetActive(true);
        birdAnim.enabled = false;
        birdRB.constraints = RigidbodyConstraints2D.None;

        soundController.PlayOneShot(dieSound, 0.5f);
    }
}
