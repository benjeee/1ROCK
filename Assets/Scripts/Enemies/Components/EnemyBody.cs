using UnityEngine;

public class EnemyBody : EnemyPart
{
    public override void TakeDamage(int dmg)
    {
        parent.TakeDamage(dmg);
        renderer.material = damageTakenMaterial;
        timeSinceDamageTaken = 0;
    }
}