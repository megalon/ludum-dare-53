using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public enum Team
    {
        PLAYER,
        ENEMY
    }

    [SerializeField]
    private float _health;
    [SerializeField]
    private Team _team;

    [SerializeField]
    private UnityEvent<float> OnHurt;
    [SerializeField]
    private UnityEvent OnDie;

    public float Health { get => _health; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dmg">How much damage to do</param>
    /// <param name="callingTeam">The team the object is on that is calling this method</param>
    public void Hurt(float dmg, Team callingTeam)
    {
        if (callingTeam == _team) return;

        //if (gameObject != null)
        //{
        //    Debug.Log($"{gameObject.name} took {dmg} damage");
        //}

        _health -= dmg;

        OnHurt.Invoke(dmg);

        if (_health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnDie.Invoke();
    }
}
