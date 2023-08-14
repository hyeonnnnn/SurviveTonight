using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // ü��
    [SerializeField] int maxHp;
    int currentHp;
    [SerializeField] TextMeshProUGUI hpText;

    // ���ǵ� ���� ����
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    float applySpeed;

    [SerializeField] float jumpForce;

    // ���� ����
    bool isGround = true;
    bool isDamage = false;
    bool isDead = false;

    // ī�޶� �ΰ���
    [SerializeField] float lookSensitivity;
    
    // ī�޶� �Ѱ�
    [SerializeField] float cameraRotationLimit;
    float currentCameraRotation_X = 0f;

    [SerializeField] Camera theCamera;
    [SerializeField] Rigidbody myRigid;
    [SerializeField] CapsuleCollider capsuleCollider;

    [SerializeField] Inventory theInventory;
    [SerializeField] WeaponManager theWeaponManager;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        applySpeed = walkSpeed;
        hpText.text = "<color=yellow>" + currentHp + "</color>/" + maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        IsGround();
        Move();

        // �κ��丮�� Ȱ��ȭ�Ǹ� ī�޶�, ĳ���͸� ������
        if (Inventory.inventoryActivated == false)
        {
            CameraRotation();
            CharacterRotation();
        }
    }

    void GetInput()
    {
        // ����: Space
        if (Input.GetKeyDown(KeyCode.Space) && isGround) Jump();

        // �޸���: Left Shift
        if (Input.GetKey(KeyCode.LeftShift)) Running();
        if (Input.GetKeyUp(KeyCode.LeftShift)) RunningCancle();
    }

    void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    }

    void Jump()
    {
        myRigid.velocity = transform.up * jumpForce;
    }

    void Running()
    {
        applySpeed = runSpeed;
    }

    void RunningCancle()
    {
        applySpeed = walkSpeed;
    }

    // ĳ���� �̵�
    void Move()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 hMove = transform.right * hAxis;
        Vector3 vMove = transform.forward * vAxis;

        Vector3 _velocity = (hMove + vMove).normalized * applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    // ī�޶� ȸ�� (����)
    void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotation_X -= _cameraRotationX;
        currentCameraRotation_X = Mathf.Clamp(currentCameraRotation_X, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotation_X, 0f, 0f);
    }

    // ĳ���� ȸ�� (�¿�)
    void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;

        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }

    public void Damage(int _dmg, Vector3 _targetPos)
    {
        if (isDead == false && !isDamage)
        {
            currentHp -= _dmg;
            hpText.text = "<color=yellow>" + currentHp + "</color>/" + maxHp;
            StartCoroutine(OnDamage());

            if (currentHp <= 0)
            {
                Dead();
                return;
            }
        }
    }

    IEnumerator OnDamage()
    {
        isDamage = true;
        yield return new WaitForSeconds(2.5f); // ���� Ÿ��
        isDamage = false; // ���� Ÿ�� ����
    }

    void Dead()
    {
        isDead = true;
        Debug.Log("���� ����");
    }

    // �ʵ������ ����
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("FieldItem"))
        {
            Item item = collision.GetComponent<FieldItems>().item;

            // ��ġ ����
            if (item.itemName == "Hammer")
            {
                theWeaponManager.ActivateHammer();
            }

            theInventory.AcquireItem(item);
            Destroy(collision.gameObject);
        }
    }
}
