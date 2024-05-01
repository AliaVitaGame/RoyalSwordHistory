using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public float jumpForce = 10f; // Jump force
    public Transform groundCheck; // Ground check object
    public LayerMask groundLayer; // Layer mask for the ground

    public Text dialogText;
    public string[] dialogLines; // Array of dialog lines

    private int currentLineIndex = 0;
    private bool isTyping = false;


    [SerializeField] private LayerMask _NPCMask;
    [SerializeField] private float _detectRadius;
    [SerializeField] private GameObject _actionCloud,_dialogCanvas;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator _animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }


    void Update()
    {
      
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

      
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

   
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        NPCCommunication();
    }




    private void NPCCommunication() {
        if (Physics2D.OverlapCircle(transform.position, _detectRadius, _NPCMask))
        {
            _actionCloud.SetActive(true);
            _animator.SetBool("IsActive", true);
            if (Input.GetKeyDown(KeyCode.E)) { 
                _dialogCanvas.SetActive(true);
                if (_dialogCanvas)
                    StartCoroutine(StartDialog());
            }
        }
        else
        {
            _dialogCanvas.SetActive(false);
            _animator.SetBool("IsActive", false);
            _actionCloud.SetActive(false);
        }
    }


    private IEnumerator StartDialog()
    {
        
        while (currentLineIndex < dialogLines.Length)
        {
    
            dialogText.text = "";

         
            isTyping = true;

       
            foreach (char c in dialogLines[currentLineIndex].ToCharArray())
            {
                
                dialogText.text += c;

               
                yield return new WaitForSeconds(0.08f);
            }
            
            isTyping = false;

            yield return new WaitForSeconds(1.0f);

            currentLineIndex++;
        }

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _detectRadius);
    }
}
