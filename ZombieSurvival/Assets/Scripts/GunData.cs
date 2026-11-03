using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    public AudioClip shotClip;
    public AudioClip reloadClip;

    public float damage = 10f;
    public int startAmmoRemain = 500;
    public int magCapacity = 100;
    public float timeBetFire = 0.02f;
    public float reloadTime = 1.5f;
}
