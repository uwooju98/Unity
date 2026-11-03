using UnityEngine;
using UnityEngine.Events;

public class AmmoPack : MonoBehaviour, IItem
{
    public UnityEvent OnUse { get; set; } = new UnityEvent();

    public void Use(GameObject target)
    {
        PlayerShooter shooter = target.GetComponent<PlayerShooter>();
        if (shooter == null || shooter.gun == null)
        {
            return;
        }

        shooter.gun.SetAmmo();
        gameObject.SetActive(false);
        OnUse.Invoke();
    }
}