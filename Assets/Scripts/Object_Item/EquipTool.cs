using UnityEngine;

public class EquipTool : Equip
{
    public float attackRate;
    private bool attacking;
    public float attackDistance;
    public float useStamina;

    [Header("Resouce")]
    public bool doesGatherResouce;

    [Header("Combat")]
    public bool doesDealDamage;
    public int damage;
   
    private Animator animator;
    private Camera camera;
    void Start()
    {
        animator = GetComponent<Animator>();
        camera = Camera.main;
    }

    public override void OnAttackInput()
    {
        if (!attacking)
        {
            if (CharacterManager.Instance.Player.condition.UseStamina(useStamina)) 
            {
                attacking = true;
                animator.SetTrigger("Attack");
                Invoke("OnCanAttack", attackRate);
            }
        }
    }

    void OnCanAttack() 
    {
        attacking=false;
    }

    void Onhit() 
    {
        Ray ray=camera.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, attackDistance)) 
        {
            if (doesGatherResouce && hit.collider.TryGetComponent(out Resource resource)) 
            {
                resource.Gather(hit.point, hit.normal);
            }

            if (doesDealDamage && hit.collider.TryGetComponent(out IDamagalbe damagable))
            {
                damagable.TakePhysicalDamage(damage);
            }
        }
    }
}