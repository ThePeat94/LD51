﻿using Nidavellir.Audio;
using Nidavellir.Scriptables.Audio;
using UnityEngine;

namespace Nidavellir.Towers.Projectiles
{
    public class BaseBullet : Projectile
    {
        [SerializeField] private SfxData hitSfxData;

        public override void Init(GameObject target, Vector3 targetPosition, float damage)
        {
            base.Init(target, targetPosition, damage);
            transform.LookAt(targetPosition);
        }

        public override void Move(float deltaTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, this.targetPosition, this.moveSpeed * deltaTime);

            if (Vector3.Distance(transform.position, this.targetPosition) <= 0.01f)
            {
                if (this.target != null)
                {
                    target
                        .GetComponent<EnemyHealthController>()
                        .TakeDamage(this.damage);

                    SfxPlayer.Instance.PlayOneShot(hitSfxData);
                }

                Destroy(this.gameObject);
            }
        }
    }
}