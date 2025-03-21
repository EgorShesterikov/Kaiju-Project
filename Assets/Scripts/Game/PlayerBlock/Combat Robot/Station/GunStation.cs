using UnityEngine;

namespace Kaiju
{
    public class GunStation : StationBase
    {
        private enum GunSideTypes
        {
            Left = -1,
            Right = 1,
        }

        [SerializeField] private Transform gun;
        [SerializeField] private Transform spawnBulleTransform;
        [SerializeField] private GunSideTypes gunSide;

        [Space]
        [SerializeField] private GunStationConfig config;

        public override void PressSpace(bool active)
        {
            if (active)
            {
                Fire();
            }
        }

        private void Fire()
        {
            var bulletRotation = gun.localRotation;
            bulletRotation.z += Random.Range(-config.BulletSpread, config.BulletSpread);

            var bullet = Instantiate(config.Bullet, spawnBulleTransform.position, bulletRotation);
            bullet.Initialize(config.BulletSpeed, config.BulletDamage, config.BulletLifeTime, bullet.transform.right * (int)gunSide);
        }

        public override void PressInstantVertical(float value)
        {
            RotateGun(value);
        }

        private void RotateGun(float value)
        {
            var rotation = gun.localRotation;

            rotation.z += config.SpeedGunRotation * value * Time.fixedDeltaTime * (int)gunSide;
            rotation.z = Mathf.Clamp(rotation.z, -config.ClampAngleGunRotation, config.ClampAngleGunRotation);

            gun.localRotation = rotation;
        }
    }
}