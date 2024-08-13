using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NgoshiPlayerContoroller : NetworkBehaviour
{
    [SerializeField] float speed = 1f;
    Animator animator;

    void Start()
    {
//        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (IsOwner)
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");
            // �T�[�o�[(�z�X�g)�̃��\�b�h���Ăяo��
            MoveServerRpc(h, v);
        }

        if (IsServer)
        {
            // �X�e�[�W���痎������߂��Ă�����
            if (transform.position.y < -10.0)
            {
                transform.position = new Vector3(Random.Range(-3, 3), 3, Random.Range(-3, 3));
            }
        }
    }

    [ServerRpc]
    void MoveServerRpc(float vertical, float horizontal)
    {
        // �����̓T�[�o�[(�z�X�g)�ł������������
        var moveInput = new Vector3(horizontal, 0, vertical);
        if (moveInput.magnitude > 0f)
            transform.LookAt(transform.position + moveInput);

        transform.position += moveInput * Time.deltaTime * speed;
//        animator.SetFloat("Speed", moveInput.magnitude);
    }
}
