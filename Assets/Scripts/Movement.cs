using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour {

    public SpriteRenderOrderSystem[] playerParts;

    public float speed = 1;
    public float walkSpeed;
    public float sprintSpeed;
    public float staminaTime;
    public float staminaUsed;
    public bool sprinting = false;

    public Animator anim;
    private Rigidbody2D rb;
    private Vector3 change;

    public Transform groundChecker;
    Vector3 hitDir = new Vector3(0, 0, 1);
    public float hitDistance;
    public LayerMask layer;

    void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (change.x > 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        } else if (change.x < 0) {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (change.y > 0) {
            Vector3 changePos = groundChecker.position;
            changePos.y = transform.position.y - 0.3f;
            groundChecker.position = changePos;
        } else if (change.y < 0) {
            Vector3 changePos = groundChecker.position;
            changePos.y = transform.position.y - 0.5f;
            groundChecker.position = changePos;
        }

        if (change != Vector3.zero) {
            MovePlayer();
        } else {
            if(!Stamina.Instance.startRecovery && !Stamina.Instance.resting) {
                Stamina.Instance.startRecovery = true;
            }
            Stamina.Instance.resting = true;
            sprinting = false;
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
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
