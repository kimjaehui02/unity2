using System;
using System.Collections;
using System.Collections.Generic; // List<T>�� ����ϱ� ���� �ʿ��� using ��
using UnityEngine;

public enum PlayerState
{
    Idle,
    Move,
    Dash,
    Attack,
    Parry
}

public class Players : MonoBehaviour
{
    public PlayerState currentState = PlayerState.Idle;
    public PlayerAnimationManager PlayerAnimationManager;

    #region ����
    // ���� �̵� ������ �����ϴ� ����
    public Vector2 vector2OfLastDirection;

    // �⺻ �̵� �ӵ� �� �뽬 �ӵ�
    public float floatOfSpeed;
    public float floatOfDashSpeed;

    // �̵� �Է� ��
    public float horizontal;
    public float vertical;

    // �뽬 ���� ����
    public float dashInterval = 1f; // �뽬 ����
    public float dashDistance = 5f; // �ִ� �뽬 �Ÿ�
    public float dashDuration = 0.2f; // �ִ� �뽬 ���� �ð�

    private Vector2 dashDirection; // �뽬 ������ �����ϴ� ����
    public float lastDashTime = 0f; // ������ �뽬 �ð�

    public float attackInterval = 3f; // �뽬 ����
    public float lastAttackTime = 0f; // ������ �뽬 �ð�

    public float parryInterval = 3f; // �뽬 ����
    public float lastParryTime = 0f; // ������ �뽬 �ð�

    // �޸��� ���� ����
    public float runSpeedMultiplier = 2f; // �޸��� �ӵ� ����

    private Rigidbody2D rigidbody2DOfPlayer;
    public GameObject gameObjectOfDirection;

    public GameObject gameObjectOfWeapon;

    public PlayerStateManager playerStateManager;
    private bool isDashing = false; // �뽬 ������ ���θ� Ȯ���ϴ� �÷���
    public bool isAttaking = false; // �뽬 ������ ���θ� Ȯ���ϴ� �÷���
    public bool isParry = false; // �뽬 ������ ���θ� Ȯ���ϴ� �÷���
    public bool boolOfPaused=false;

    [SerializeField]
    private bool isPaused = false;
    [SerializeField]
    private bool isShopping = false;
    [SerializeField]
    private bool isInventory = false;
    [SerializeField]
    private bool isHpCheat = false;
    [SerializeField]
    private int cheatHp = 0;
    public GameObject PlayerAtk;

    #endregion

    #region �⺻�Լ�
    private void Start()
    {
        rigidbody2DOfPlayer = GetComponent<Rigidbody2D>();
    }
   
    private void Update()
    {
        InputKey();
        InputKey2();
        InputKeyCheat();
        HpCheat();  
        UpdateState();
        switch (currentState)
        {
            case PlayerState.Dash:
                //PlayerDashAnimation();
              
                PlayerDash();
                break;
            case PlayerState.Attack:
                //PlayerAttackAnimation();

                PlayerAttack();

                break;
            case PlayerState.Parry:
                //PlayerParryAnimation();
                PlayerParry();
                break;
        }
    }
    private void FixedUpdate()
    {
        InputKey();
        UpdateState();
        switch (currentState)
        {
            case PlayerState.Idle:
                StaminaRecovery(2);
                PlayerIdleAnimation();
                PlayerIdle();
                break;
            case PlayerState.Move:
                PlayerMoveAnimation();
                if (Input.GetKey(KeyCode.LeftShift) && CheckStaminaCost(2))
                    PlayerMove(runSpeedMultiplier);
                else
                {
                    PlayerMove(1f);
                    if (!Input.GetKey(KeyCode.LeftShift))
                        StaminaRecovery(1);
                }
                break;
        }
    }
        #endregion

        #region ��Ÿ �Լ�



        #region �׷�����

        private void PlayerIdleAnimation()
    {
        PlayerAnimationManager.PlayerIdleAnimation();
    }

    private void PlayerMoveAnimation()
    {
        PlayerAnimationManager.PlayerMoveAnimation(horizontal, vertical);
    }

    private void PlayerDashAnimation()
    {
        if (Time.time - lastDashTime < dashInterval)
        {
            return;
        }
        PlayerAnimationManager.PlayerDashAnimation();
    }

    private void PlayerAttackAnimation()
    {
        if (Time.time - lastAttackTime < attackInterval)
        {
            return;
        }
        
        PlayerAnimationManager.PlayerAttackAnimation();
    }

    private void PlayerParryAnimation()
    {
        if (Time.time - lastParryTime < parryInterval)
        {
            return;
        }

        PlayerAnimationManager.PlayerParryAnimation();
    }
    #endregion

    #region ������Ʈ ���

    private void PlayerIdle()
    {
        // Idle ���¿����� ���� ó��
        rigidbody2DOfPlayer.velocity = Vector2.zero;
    }

    // �÷��̾� �̵�
    private void PlayerMove(float speedMultiplier = 1f)
    {

        Vector2 movementDirection = new Vector2(horizontal, vertical).normalized;
        Vector2 movementVelocity = movementDirection * floatOfSpeed * speedMultiplier * Time.fixedDeltaTime;
        rigidbody2DOfPlayer.velocity = movementVelocity;

        if (gameObjectOfDirection != null)
        {
            Vector2 direction = new Vector2(horizontal, vertical).normalized;

            // ����Ű�� �������� ���� ȸ������ �����մϴ�.
            if (direction != Vector2.zero)
            {
                float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                // 90���� ���Ͽ� ������ ���ϵ��� ȸ��
                gameObjectOfDirection.transform.rotation = Quaternion.Euler(new Vector3(0, 0, targetAngle + 90));
                vector2OfLastDirection = direction;
            }
            //else if (vector2OfLastDirection != Vector2.zero)
            //{
            //    // ����Ű�� ������ ���� ��쿡�� ���� �������� ���ư��ϴ�.
            //    float targetAngle = Mathf.Atan2(vector2OfLastDirection.y, vector2OfLastDirection.x) * Mathf.Rad2Deg;
            //    // 90���� ���Ͽ� ������ ���ϵ��� ȸ��
            //    gameObjectOfDirection.transform.rotation = Quaternion.Euler(new Vector3(0, 0, targetAngle + 90));
            //}
        }
    }

    private void PlayerDash()
    {
        // �뽬 ������ ������ �ʾ����� �뽬�� �������� ����
        if (Time.time - lastDashTime < dashInterval)
        {
            return;
        }

        // �뽬 ���� ���� (������ �̵� ����)
        dashDirection = new Vector2(horizontal, vertical).normalized;

        // ����Ű�� �������� ���� �뽬�� ����
        if (dashDirection != Vector2.zero)
        {
            StartCoroutine(PerformDash());
            lastDashTime = Time.time; // �뽬�� ����Ǿ����Ƿ� ���� �ð��� lastDashTime�� ����
        }
    }

    private void PlayerAttack()
    {
        // �뽬 ������ ������ �ʾ����� �뽬�� �������� ����
        if (Time.time - lastAttackTime < attackInterval)
        {
            return;
        }

        // �뽬 ���� ���� (������ �̵� ����)
        dashDirection = new Vector2(horizontal, vertical).normalized;

        PlayerAtk.GetComponent<PlayerAttack>().PlayerAtk();

        StartCoroutine(PerformAttack());
        StartCoroutine(AttackObj());
        lastAttackTime = Time.time; // �뽬�� ����Ǿ����Ƿ� ���� �ð��� lastDashTime�� ����
    }

    private void PlayerParry()
    {
        // �뽬 ������ ������ �ʾ����� �뽬�� �������� ����
        if (Time.time - lastParryTime < parryInterval)
        {
            return;
        }
        //Debug.Log("PlayerParry");
        // �뽬 ���� ���� (������ �̵� ����)
        dashDirection = new Vector2(horizontal, vertical).normalized;

        StartCoroutine(PerformParry());
        lastParryTime = Time.time; // �뽬�� ����Ǿ����Ƿ� ���� �ð��� lastDashTime�� ����
    }

    private void PlayerDamaged()
    {

    }
    private void StaminaRecovery(int RecoveryStamina)
    {
        if(playerStateManager.GetPlayerCurrentStamina()+ RecoveryStamina <= playerStateManager.GetPlayerMaxStamina())
        {
            playerStateManager.AddPlayerCurrentStamina(RecoveryStamina);
        }
        else
        {
            playerStateManager.AddPlayerCurrentStamina(playerStateManager.GetPlayerMaxStamina() - playerStateManager.GetPlayerCurrentStamina());
        }
    }
    private bool CheckStaminaCost(int CostStamina)
    {
        if (playerStateManager.GetPlayerCurrentStamina() >= CostStamina)
        {
            playerStateManager.AddPlayerCurrentStamina(-CostStamina);
            return true;
        }
        else return false;

    }

    #endregion

    #region ������Ʈ���� 2��



    private void UpdateState()
    {
        if (isDashing) // �뽬 ���̶��
        {
            currentState = PlayerState.Dash; // �뽬 ���¸� ����
            return;
        }

        if (isAttaking) // ���� ���̶��
        {
            currentState = PlayerState.Attack; // ���� ���¸� ����
            return;
        }

        if (isParry) // ���� ���̶��
        {
            currentState = PlayerState.Parry; // ���� ���¸� ����
            return;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            //Debug.Log(11223);
            currentState = PlayerState.Attack;
            
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            currentState = PlayerState.Parry;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            currentState = PlayerState.Dash;
        }
        else if (horizontal != 0 || vertical != 0)
        {
            currentState = PlayerState.Move;
        }
        else
        {
            currentState = PlayerState.Idle;
        }
        
    }


    // �̵� �Է� �ޱ�
    private void InputKey()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    //�޴� ����Ű �Է¹ޱ�
    private void InputKey2()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isShopping) isShopping = false;
            else if (isPaused) isPaused = false;
            else if (isInventory) isInventory = false;
            else isPaused = true;
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (isInventory) isInventory = false;
            else isInventory = true;
        }
        if (isPaused || isInventory || isShopping) boolOfPaused = true;
        else boolOfPaused = false;
        Time.timeScale = boolOfPaused ? 0f : 1f;
    }
    private void InputKeyCheat()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (isHpCheat) isHpCheat = false;
            else 
            {
                isHpCheat = true;
                cheatHp = playerStateManager.GetPlayerCurrentHp();
            }
        }
    }
    private void HpCheat()
    {
        if (isHpCheat)
        {
            playerStateManager.SetPlayerCurrentHp(cheatHp);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && collision.transform.tag == "SoulShop" && !isShopping)
        {
            isShopping = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && collision.transform.tag == "SoulShop" && !isShopping)
        {
            isShopping = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && collision.transform.tag == "SoulShop" && !isShopping)
        {
            isShopping = true;
        }
    }



    #endregion


    #region �׿� ��Ÿ �Լ�

    public void EnemyAttack()
    {
        if(isParry ==  true)
        {
            StartCoroutine(PlayerAnimationManager.Parried());
        }
        else
        {
            StartCoroutine(PlayerAnimationManager.Hited());
        }
    }

    public bool GetIsPaused()
    {
        return isPaused;
    }
    public void SetIsPaused(bool ispaused)
    {
        isPaused = ispaused;
    }

    public bool GetIsShopping()
    {
        return isShopping;
    }
    public bool GetIsInventory()
    {
        return isInventory;
    }

    #endregion


    #endregion

    #region �ڷ�ƾ

    // �뽬 ���� �ڷ�ƾ
    private IEnumerator PerformDash()
    {
        if (CheckStaminaCost(50))
        {
            isDashing = true;

            // �뽬 ���� ��ġ
            Vector2 startPosition = rigidbody2DOfPlayer.position;
            PlayerDashAnimation();

            // �뽬 �Ÿ��� �뽬 �ӵ��� ������� �뽬 �ð��� ���
            float adjustedDashDuration = Mathf.Min(dashDistance / floatOfDashSpeed, dashDuration);

            float startTime = Time.time;
            while (Time.time < startTime + adjustedDashDuration)
            {
                // �뽬 ����� �Ÿ� ���
                Vector2 dashMove = dashDirection * floatOfDashSpeed * Time.fixedDeltaTime;

                // ������ �浹 ó���� ���� ����� ����
                Vector2 newPosition = rigidbody2DOfPlayer.position + dashMove;

                // ����ĳ��Ʈ�� ����Ͽ� �뽬 ���� �浹 ����
                RaycastHit2D hit = Physics2D.Raycast(rigidbody2DOfPlayer.position, dashDirection, dashDistance, LayerMask.GetMask("Obstacle"));

                if (hit.collider != null)
                {
                    // ����ĳ��Ʈ�� ���� �浹�� ��� �浹 ���������� �뽬 ����
                    newPosition = hit.point;
                    isDashing = false; // �浹 �� �뽬 ����
                }

                rigidbody2DOfPlayer.MovePosition(newPosition);

                yield return null;
            }

            isDashing = false;
        }

    }

    // �뽬 ���� �ڷ�ƾ
    public IEnumerator PerformAttack()
    {
        if (CheckStaminaCost(50))
        {
            isAttaking = true;
            rigidbody2DOfPlayer.velocity = Vector2.zero;
            PlayerAttackAnimation();
            //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 1f);
            //
            //for (int i = 0; i < hitEnemies.Length; i++)
            //{
            //    StartCoroutine(PlayerAnimationManager.Hited(hitEnemies[i].gameObject));
            //}

            //Debug.Log(hitEnemies.Length);
            // ���� ���� �ð���ŭ ���
            yield return new WaitForSeconds(0.5f);

            isAttaking = false;
        }
        
    }

    public IEnumerator AttackObj()
    {
        gameObjectOfWeapon.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        gameObjectOfWeapon.SetActive(false);

    }

    private IEnumerator PerformParry()
    {
        isParry = true;
        rigidbody2DOfPlayer.velocity = Vector2.zero;
        PlayerParryAnimation();
        // ���� ���� �ð���ŭ ���
        yield return new WaitForSeconds(0.5f);

        isParry = false;
    }

    #endregion



}
