using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie_scripts : MonoBehaviour
{
    public Transform player;
    public bool ischasing;
    private NavMeshAgent nav;
    public float range;
    public float atk_range;
    public float hp;
    public Animator animator;
    public float time = 0;
    public Move_maya player_stats;
    public bool ishiting;
    public GameObject health_pot;
    public Weapons DropWeapon;

    public int STR;
    public int AGI;
    public int CON;
    public int ARMOR;
    public float minDMG;
    public float maxDMG;
    public float baseDMG;
    public int level;
    public long xp;
    public int money;

    private bool m_isDead = false;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        player_stats = player.GetComponent<Move_maya>();

        level = player_stats.level;
        // + 15% par lvl
        CON = (int)(2 * (1 + 0.4 * (level - 1)));
        AGI = (int)(10 * (1 + 0.4 * (level - 1)));
        STR = (int)(4 * (1 + 0.4 * (level - 1)));
        ARMOR = (int)(10 * (1 + 0.2 * (level - 1)));
        xp = (int)(20 * (1 + 0.6 * (level - 1)));
        money = (int)(2 * (1 + 0.5 * (level - 1)));

        hp = 5 * CON;
        minDMG = STR / 2;
        maxDMG = minDMG + 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp > 0)
        {
            if (Vector3.Distance(player.position, transform.position) < range)
                ischasing = true;
            if (Vector3.Distance(player.position, transform.position) < atk_range && player_stats.hp > 0)
            {
                ischasing = false;
                animator.SetInteger("State", 2);
                nav.SetDestination(transform.position);
            }
            if (player_stats.hp < 0)
            {
                ischasing = false;
                animator.SetInteger("State", 0);
                nav.SetDestination(transform.position);
            }
            if (ischasing && player)
            {
                nav.SetDestination(player.position);
            }
            if (nav.hasPath)
            {
                animator.SetInteger("State", 1);
            }
        }
        else if (m_isDead == false)
        {
            StartCoroutine(Die());
        }
    }

    public void attack()
    {
        if (Random.Range(0, 100) < (75 + AGI - player_stats.AGI))
        {
            player_stats.hp -= (int)(Random.Range(minDMG, maxDMG) * (1 - player_stats.ARMOR / 200));
            if (player_stats.hp <= 0)
            {
                xp += player_stats.xp;
                money += player_stats.money;
            }
        }
    }

    IEnumerator Die()
    {
        m_isDead = true;
        animator.SetInteger("State", 3);
        yield return new WaitForSeconds(5f);

        for (float i = 0f; i < 2f; i += Time.deltaTime)
        {
            nav.enabled = false;
            transform.Translate(Vector3.down * Time.deltaTime);
            yield return new WaitForEndOfFrame();

        }
        GameObject tmp = Instantiate(DropWeapon, transform.position, Quaternion.identity).gameObject;
        tmp.transform.position = gameObject.transform.position + (Vector3.up * 3) + Vector3.left;
        if (Random.Range(1, 10) > 5)
            Instantiate(health_pot, (transform.position + (Vector3.up * 3)), transform.rotation);
        Destroy(this.gameObject);
    }
}
