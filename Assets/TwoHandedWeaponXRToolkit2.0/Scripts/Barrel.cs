using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

namespace SniperDemo
{
    public class Barrel : MonoBehaviour
    {
        public float fireWait = 0.05f;
        private float ejectPower = 2.7f;
        private float destroyTimer = 2.0f;
        
        public GameObject casingPrefab;
        public GameObject projectilePrefab;
        public GameObject muzzleFlashPrefab;
        
        public Transform barrelLocation;
        public Transform casingExitLocation;
        
        public WeaponRecoil recoil;
        public AudioSource audioSourceShoot;
        public AudioSource audioSourceNoAmmo;
        public XRSocketInteractor socket;

        [SerializeField] private TMP_Text ammoText;
        [SerializeField] private int maxAmmo;
        private int currentAmmo;
        private bool hasAmmo;
        
        public void StartFiring()
        {
            if (socket.hasSelection && hasAmmo) { StartCoroutine(FiringSequence()); }
            else { audioSourceNoAmmo.Play(); }
        }

        private IEnumerator FiringSequence()
        {
            while (gameObject.activeSelf)
            {
                CreateProjectile();
                yield return new WaitForSeconds(fireWait);
            }
        }

        private void CreateProjectile()
        {
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);
            Destroy(tempFlash, destroyTimer);

            Projectile projectile = Instantiate(projectilePrefab, transform.position, transform.rotation).GetComponent<Projectile>();
            projectile.Launch();
            
            CasingRelease();
            recoil.StartRecoil(20.0f, 3f, 30f, 5.0f);
            audioSourceShoot.Play();

            currentAmmo--;
            ammoText.text = currentAmmo.ToString();
            if (currentAmmo <= 0) { hasAmmo = false; }
        }

        //Call once when getting socket
        public void Recharge()
        {
            hasAmmo = true;
            currentAmmo = maxAmmo;
            ammoText.text = currentAmmo.ToString();
        }

        //Call when finishing or removing socket
        public void Empty()
        {
            hasAmmo = false;
            currentAmmo = 0;
            ammoText.text = currentAmmo.ToString();
        }

        public void StopFiring()
        {
            StopCoroutine(FiringSequence());
        }

        void CasingRelease()
        {
            //Create the casing
            GameObject tempCasing;
            tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
            //Add force on casing to push it out
            tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
            //Add torque to make casing spin in random direction
            tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);
            //Destroy casing after X seconds
            Destroy(tempCasing, destroyTimer);
        }
    }
}