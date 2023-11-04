using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : GenericProjectile
{

    public Fireball(int damage, int duration, int interval) : base(damage, duration, interval)
    {

    }

    protected override IEnumerator projEffect()
    {
        if (duration > 0)
        {
            enemy.TakeDamage(1);
            duration--;
        }
        yield return new WaitForSeconds(interval);
    }
}
