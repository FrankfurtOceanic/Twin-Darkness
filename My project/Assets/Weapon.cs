using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float Damage = 10f;
    public float Range = 100f;
    public float FireRate = 15f; //per second
    public Camera fpsCam;
    public ParticleSystem MuzzleEffect;
    public Animator anim;

    public AudioSource SoundSource;
    public AudioClip SoundClip;
    private float TimeToFire = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= TimeToFire)
        {
            TimeToFire = Time.time + 1f / FireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        MuzzleEffect.Play();
        SoundSource.PlayOneShot(SoundClip);
        anim.SetTrigger("Shoot");

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, Range))
        {
            Enemy shot = hit.transform.GetComponent<Enemy>();
            if(shot != null)
            {
                shot.TakeDamage(Damage);
            }
        }
    }
}
