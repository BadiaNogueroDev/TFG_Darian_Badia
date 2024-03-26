using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;

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
        private bool firstFrameRecharge;
        private bool firstFrameEmpty;
        
        public void StartFiring()
        {
            if (socket.hasSelection && hasAmmo) { StartCoroutine(FiringSequence()); }
            else { audioSourceNoAmmo.Play(); }
        }

        private IEnumerator FiringSequence()
        {
            CreateProjectile();
            yield return new WaitForSeconds(fireWait);
        }

        private void Update()
        {
            if (socket.hasSelection && !firstFrameRecharge)
            {
                Recharge();
                firstFrameRecharge = true;
                firstFrameEmpty = false;
            }

            if (!socket.hasSelection && !firstFrameEmpty)
            {
                Empty();
                firstFrameRecharge = false;
                firstFrameEmpty = true;
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

            Manager.instance.data.ShotsFired++;
        }

        //Call once when getting socket
        public void Recharge()
        {
            Manager.instance.data.MagazineChanged++;
            
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
            GameObject tempCasing;
            tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
            tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
            tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);
            Destroy(tempCasing, destroyTimer);
        }
    }
}