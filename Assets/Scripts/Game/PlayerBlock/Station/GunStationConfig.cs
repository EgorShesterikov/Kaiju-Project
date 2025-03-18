using UnityEngine;

namespace Kaiju
{
    [CreateAssetMenu(fileName = "GunStationConfig", menuName = "Configs/GunStationConfig")]
    public class GunStationConfig : ScriptableObject
    {
        [SerializeField] private float speedGunRotation = 2;
        [SerializeField, Range(0, 1)] private float clampAngleGunRotation = 0.5f;

        [Space] 
        [SerializeField] private GunStationBullet bullet;
        [SerializeField] private float bulletLifeTime = 5f;
        [SerializeField] private float bulletSpeed = 0.1f;
        [SerializeField, Range(0, 1)] private float bulletSpread = 0.1f;
        [SerializeField] private float bulletDamage = 3f;

        public float SpeedGunRotation => speedGunRotation;
        public float ClampAngleGunRotation => clampAngleGunRotation;

        public GunStationBullet Bullet => bullet;
        public float BulletLifeTime => bulletLifeTime;
        public float BulletSpeed => bulletSpeed;
        public float BulletSpread => bulletSpread;
        public float BulletDamage => bulletDamage;
    }
}