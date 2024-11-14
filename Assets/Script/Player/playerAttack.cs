using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackPoint;
    public Transform point;
    public float raio;
    public LayerMask enemies;
    

    public float attackRateMelee;
    float attackTimeMelee = 0f;

    public float attackRateRanged;
    float attackTimeRanged = 0f;

    [SerializeField]
    private Slider attackSlider;

    [SerializeField]
    private Slider rangedSlider;

    public float offset;
    public GameObject harpoon;


    // Start is called before the first frame update
    void Start()
    {
        attackSlider.maxValue = attackRateMelee;
        rangedSlider.maxValue = attackRateRanged;
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
        updateSliderMelee();
        updateWeapon();
        updateSliderRanged();

    }

    void updateSliderMelee()
    {
        if (attackTimeMelee <= attackRateMelee)
        {
            attackTimeMelee += Time.deltaTime;
        }
        if (attackTimeMelee > attackRateMelee)
        {
            attackTimeMelee = attackRateMelee;
        }

        attackSlider.value = attackTimeMelee;
    }

    void updateSliderRanged()
    {
        if (attackTimeRanged <= attackRateRanged)
        {
            attackTimeRanged += Time.deltaTime;
        }
        if (attackTimeRanged > attackRateRanged)
        {
            attackTimeRanged = attackRateRanged;
        }

        rangedSlider.value = attackTimeRanged;
    }

    void getInput()
    {
        if (attackTimeMelee == attackRateMelee)
        {
            if (Input.GetMouseButtonDown(0))
            {
                attack(1);
                attackTimeMelee = 0;
                //Debug.Log("re");
            }
        }

        if (attackTimeRanged == attackRateRanged)
        {
            if (Input.GetMouseButtonDown(1))
            {
                shoot();
                attackTimeRanged = 0;
                //Debug.Log("re");
            }
        }
    }

    public void updateWeapon()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(diff.y,diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f,0f,rotateZ+offset);
    }

    public void shoot()
    {
      Instantiate(harpoon, point.position, transform.rotation);
 
    }

    
    public void attack(int dmg)
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, raio, enemies);
        foreach (Collider2D e in enemy)
        {
            enemyHealth health = e.GetComponent<enemyHealth>();
            if (health != null)
            {
                health.TakeDamage(dmg);
                Debug.Log("receba");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, raio);
    }
}
