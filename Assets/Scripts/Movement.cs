using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour {

    public static Movement Instance;

    public bool isFacingLeft = true;

    public float speed = 1;
    public float walkSpeed;
    public float sprintSpeed;
    public float staminaTime;
    public float staminaUsed;
    public bool sprinting = false;

    public Animator anim;
    private Rigidbody2D rb;
    private Vector3 change;

    private void Awake() {
        if(Instance != null) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (!Health.Instance.dead) {
            change = Vector3.zero;
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");
            change.Normalize();

            if (change != Vector3.zero) {
                MovePlayer();
            } else {
                if (!Stamina.Instance.startRecovery && !Stamina.Instance.resting) {
                    Stamina.Instance.startRecovery = true;
                }
                Stamina.Instance.resting = true;
                sprinting = false;
                anim.SetBool("Walking", false);
                anim.SetBool("Running", false);
            }
        }
    }

    void MovePlayer() {
        if (Input.GetKey(KeyCode.LeftShift) && Stamina.Instance.stamina > 0) {
            Stamina.Instance.resting = false;
            anim.SetBool("Running", true);
            anim.SetBool("Walking", false);
            rb.MovePosition(transform.position + change * sprintSpeed * speed * GameManager.DeltaTime);
            StartCoroutine(UseStamina());
        } else {
            sprinting = false;
            anim.SetBool("Walking", true);
            anim.SetBool("Running", false);
            rb.MovePosition(transform.position + change * walkSpeed * speed * GameManager.DeltaTime);
        }
        if (change.x > 0) {
            isFacingLeft = false;
            transform.localScale = new Vector3(-1, 1, 0);
        } else if (change.x < 0) {
            isFacingLeft = true;
            transform.localScale = new Vector3(1, 1, 0);
        }
    }

    IEnumerator UseStamina() {
        if (!sprinting) {
            sprinting = true;
            while (sprinting) {
                Stamina.Instance.ReduceStamina(staminaUsed);
                yield return new WaitForSeconds(staminaTime);
            }
        }
    }
}
