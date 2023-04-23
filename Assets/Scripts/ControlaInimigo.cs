using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour
{
    public GameObject Player;
    public float EnemySpeed = 5;
    private Rigidbody _enemyRigidbody;
    private Animator _enemyAnimator;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        int _kindZumbiGenerator = Random.Range(1, 28);
        transform.GetChild(_kindZumbiGenerator).gameObject.SetActive(true);
        _enemyRigidbody = GetComponent<Rigidbody>();
        _enemyAnimator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        float _distance = Vector3.Distance(transform.position, Player.transform.position);

        Vector3 _direction = Player.transform.position - transform.position;

        Quaternion _newRotation = Quaternion.LookRotation(_direction);
        _enemyRigidbody.MoveRotation(_newRotation);

        if (_distance > 2.5)
        {
            _enemyRigidbody.MovePosition(
                _enemyRigidbody.position +
                _direction.normalized * EnemySpeed * Time.deltaTime);

            _enemyAnimator.SetBool("Atacando", false);
        }
        else
        {
            _enemyAnimator.SetBool("Atacando", true);
        }
    }

    void AtackPlayer()
    {
        Time.timeScale = 0;
        float _damage = Random.Range(10f, 25f);
        Player.GetComponent<ControlaJogador>().Life -= 25f;
        Player.GetComponent<ControlaJogador>().ReceiveDamage(_damage);

    }
}
