using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour {

    public static Movement Instance;

    public SpriteRenderOrderSystem[] playerParts;

    public bool canMove = true;

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
        if (canMove) {
            change = Vector3.zero;
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");
            change.Normalize();

            if (change.x > 0) {
                transform.localScale = new Vector3(-1, 1, 1);
            } else if (change.x < 0) {
                transform.localScale = new Vector3(1, 1, 1);
            }

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
        foreach (SpriteRenderOrderSystem part in playerParts) {
            part.UpdateSorting();
        }
        if (Input.GetKey(KeyCode.LeftShift) && Stamina.Instance.stamina > 0) {
            Stamina.Instance.resting = false;
            anim.SetBool("Running", true);
            anim.SetBool("Walking", false);
            rb.MovePosition(transform.position + change * sprintSpeed * speed * Time.deltaTime);
            StartCoroutine(UseStamina());
        } else {
            sprinting = false;
            anim.SetBool("Walking", true);
            anim.SetBool("Running", false);
            rb.MovePosition(transform.position + change * walkSpeed * speed * Time.deltaTime);
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
