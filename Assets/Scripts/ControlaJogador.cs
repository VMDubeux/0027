using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour
{
    public float Life = 100f;
    public float PlayerSpeed = 10;
    public LayerMask GroundMask;
    public GameObject GameOverText;
    private Rigidbody _rigidbodyPlayer;
    private Animator _animatorPlayer;
    private Vector3 _direction;
    private float _damage;

    private void Start()
    {
        Time.timeScale = 1;
        _rigidbodyPlayer = GetComponent<Rigidbody>();
        _animatorPlayer = GetComponent<Animator>();
    }

    private void Update()
    {
        MovePlayer();
        MoveAnimation();
        ReloadSceneAfterDeath();
    }

    private void FixedUpdate()
    {
        _rigidbodyPlayer.MovePosition
            (_rigidbodyPlayer.position +
            (_direction * PlayerSpeed * Time.deltaTime));

        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(_ray.origin, _ray.direction * 100, Color.red);

        RaycastHit _impact;

        if (Physics.Raycast(_ray, out _impact, 100, GroundMask))
        {
            Vector3 _aimPlayerPosition = _impact.point - transform.position;
            _aimPlayerPosition.y = transform.position.y;
            Quaternion _newRotation = Quaternion.LookRotation(_aimPlayerPosition);
            _rigidbodyPlayer.MoveRotation(_newRotation);
        }
    }

    private void MovePlayer()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");
        _direction = new Vector3(eixoX, 0, eixoZ);
    }

    private void MoveAnimation()
    {
        if (_direction != Vector3.zero)
            _animatorPlayer.SetBool("Movendo", true);
        else
            _animatorPlayer.SetBool("Movendo", false);
    }

    public void ReceiveDamage(float _damage)
    {
        Life -= _damage;
        Debug.Log(Life);
        if (Life <= 0)
            GameOverText.SetActive(true);
    }

    private void ReloadSceneAfterDeath()
    {
        if (Life <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
                SceneManager.LoadScene("game");
        }
    }
}
