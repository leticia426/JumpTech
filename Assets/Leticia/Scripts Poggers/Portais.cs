using UnityEngine;

public class Portais : MonoBehaviour
{
    [SerializeField] private GameObject nextDoor; // Próxima porta
    private PersonagemController PC;
    public LayerMask playerLayer;
    public float radius = 2.0f; // Define o raio desejado para detecção
    public bool Teleport;
    public bool canTeleport;
    public bool IsPlayerFar;



    private void Start()
    {
        PC = FindObjectOfType<PersonagemController>();
        canTeleport = true;
    }

    private void Update()
    {
        if (IsPlayerNear() && !Teleport && ProgressManager.Instance.Teleported == true)
        {
            IsPlayerFar = true;
        }
        if (IsPlayerFar && !IsPlayerNear())
        {
            ProgressManager.Instance.Teleported = false;
            IsPlayerFar = false;
        }

        if (Teleport && !ProgressManager.Instance.Teleported && canTeleport)
        {
            ProgressManager.Instance.Teleported = true;
            newPosition();
        }

        if (IsPlayerNear() && ProgressManager.Instance.Teleported)
        {
            canTeleport = false;
        }

        else
        {
            canTeleport = true;
        }
    }

    private void newPosition()
    {
        PC.rb.transform.position = new Vector2(nextDoor.transform.position.x, nextDoor.transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Teleport = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Teleport = false; // Define como falso quando o personagem sai do raio do portal
        }
    }

    private bool IsPlayerNear()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, playerLayer);
        return (hit != null);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}