using System;
using System.Collections;
using System.Collections.Generic; // List<T>를 사용하기 위해 필요한 using 문
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

    #region 변수
    // 이전 이동 방향을 저장하는 변수
    public Vector2 vector2OfLastDirection;

    // 기본 이동 속도 및 대쉬 속도
    public float floatOfSpeed;
    public float floatOfDashSpeed;

    // 이동 입력 값
    public float horizontal;
    public float vertical;

    // 대쉬 관련 변수
    public float dashInterval = 1f; // 대쉬 간격
    public float dashDistance = 5f; // 최대 대쉬 거리
    public float dashDuration = 0.2f; // 최대 대쉬 지속 시간

    private Vector2 dashDirection; // 대쉬 방향을 저장하는 변수
    public float lastDashTime = 0f; // 마지막 대쉬 시간

    public float attackInterval = 3f; // 대쉬 간격
    public float lastAttackTime = 0f; // 마지막 대쉬 시간

    public float parryInterval = 3f; // 대쉬 간격
    public float lastParryTime = 0f; // 마지막 대쉬 시간

    // 달리기 관련 변수
    public float runSpeedMultiplier = 2f; // 달리기 속도 배율

    private Rigidbody2D rigidbody2DOfPlayer;
    public GameObject gameObjectOfDirection;

    public GameObject gameObjectOfWeapon;

    public PlayerStateManager playerStateManager;
    private bool isDashing = false; // 대쉬 중인지 여부를 확인하는 플래그
    public bool isAttaking = false; // 대쉬 중인지 여부를 확인하는 플래그
    public bool isParry = false; // 대쉬 중인지 여부를 확인하는 플래그
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

    #region 기본함수
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

        #region 기타 함수



        #region 그래픽쪽

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

    #region 스테이트 기능

    private void PlayerIdle()
    {
        // Idle 상태에서의 동작 처리
        rigidbody2DOfPlayer.velocity = Vector2.zero;
    }

    // 플레이어 이동
    private void PlayerMove(float speedMultiplier = 1f)
    {

        Vector2 movementDirection = new Vector2(horizontal, vertical).normalized;
        Vector2 movementVelocity = movementDirection * floatOfSpeed * speedMultiplier * Time.fixedDeltaTime;
        rigidbody2DOfPlayer.velocity = movementVelocity;

        if (gameObjectOfDirection != null)
        {
            Vector2 direction = new Vector2(horizontal, vertical).normalized;

            // 방향키가 눌려있을 때만 회전값을 갱신합니다.
            if (direction != Vector2.zero)
            {
                float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                // 90도를 더하여 정면을 향하도록 회전
                gameObjectOfDirection.transform.rotation = Quaternion.Euler(new Vector3(0, 0, targetAngle + 90));
                vector2OfLastDirection = direction;
            }
            //else if (vector2OfLastDirection != Vector2.zero)
            //{
            //    // 방향키를 누르지 않은 경우에는 이전 방향으로 돌아갑니다.
            //    float targetAngle = Mathf.Atan2(vector2OfLastDirection.y, vector2OfLastDirection.x) * Mathf.Rad2Deg;
            //    // 90도를 더하여 정면을 향하도록 회전
            //    gameObjectOfDirection.transform.rotation = Quaternion.Euler(new Vector3(0, 0, targetAngle + 90));
            //}
        }
    }

    private void PlayerDash()
    {
        // 대쉬 간격이 지나지 않았으면 대쉬를 실행하지 않음
        if (Time.time - lastDashTime < dashInterval)
        {
            return;
        }

        // 대쉬 방향 설정 (마지막 이동 방향)
        dashDirection = new Vector2(horizontal, vertical).normalized;

        // 방향키가 눌려있을 때만 대쉬를 실행
        if (dashDirection != Vector2.zero)
        {
            StartCoroutine(PerformDash());
            lastDashTime = Time.time; // 대쉬가 실행되었으므로 현재 시간을 lastDashTime에 저장
        }
    }

    private void PlayerAttack()
    {
        // 대쉬 간격이 지나지 않았으면 대쉬를 실행하지 않음
        if (Time.time - lastAttackTime < attackInterval)
        {
            return;
        }

        // 대쉬 방향 설정 (마지막 이동 방향)
        dashDirection = new Vector2(horizontal, vertical).normalized;

        PlayerAtk.GetComponent<PlayerAttack>().PlayerAtk();

        StartCoroutine(PerformAttack());
        StartCoroutine(AttackObj());
        lastAttackTime = Time.time; // 대쉬가 실행되었으므로 현재 시간을 lastDashTime에 저장
    }

    private void PlayerParry()
    {
        // 대쉬 간격이 지나지 않았으면 대쉬를 실행하지 않음
        if (Time.time - lastParryTime < parryInterval)
        {
            return;
        }
        //Debug.Log("PlayerParry");
        // 대쉬 방향 설정 (마지막 이동 방향)
        dashDirection = new Vector2(horizontal, vertical).normalized;

        StartCoroutine(PerformParry());
        lastParryTime = Time.time; // 대쉬가 실행되었으므로 현재 시간을 lastDashTime에 저장
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

    #region 업데이트문의 2개



    private void UpdateState()
    {
        if (isDashing) // 대쉬 중이라면
        {
            currentState = PlayerState.Dash; // 대쉬 상태를 유지
            return;
        }

        if (isAttaking) // 공격 중이라면
        {
            currentState = PlayerState.Attack; // 공격 상태를 유지
            return;
        }

        if (isParry) // 공격 중이라면
        {
            currentState = PlayerState.Parry; // 공격 상태를 유지
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


    // 이동 입력 받기
    private void InputKey()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    //메뉴 조작키 입력받기
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


    #region 그외 기타 함수

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

    #region 코루틴

    // 대쉬 수행 코루틴
    private IEnumerator PerformDash()
    {
        if (CheckStaminaCost(50))
        {
            isDashing = true;

            // 대쉬 시작 위치
            Vector2 startPosition = rigidbody2DOfPlayer.position;
            PlayerDashAnimation();

            // 대쉬 거리와 대쉬 속도를 기반으로 대쉬 시간을 계산
            float adjustedDashDuration = Mathf.Min(dashDistance / floatOfDashSpeed, dashDuration);

            float startTime = Time.time;
            while (Time.time < startTime + adjustedDashDuration)
            {
                // 대쉬 방향과 거리 계산
                Vector2 dashMove = dashDirection * floatOfDashSpeed * Time.fixedDeltaTime;

                // 벽과의 충돌 처리를 위해 사용할 변수
                Vector2 newPosition = rigidbody2DOfPlayer.position + dashMove;

                // 레이캐스트를 사용하여 대쉬 도중 충돌 감지
                RaycastHit2D hit = Physics2D.Raycast(rigidbody2DOfPlayer.position, dashDirection, dashDistance, LayerMask.GetMask("Obstacle"));

                if (hit.collider != null)
                {
                    // 레이캐스트가 벽에 충돌한 경우 충돌 지점까지만 대쉬 실행
                    newPosition = hit.point;
                    isDashing = false; // 충돌 시 대쉬 종료
                }

                rigidbody2DOfPlayer.MovePosition(newPosition);

                yield return null;
            }

            isDashing = false;
        }

    }

    // 대쉬 수행 코루틴
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
            // 공격 지속 시간만큼 대기
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
        // 공격 지속 시간만큼 대기
        yield return new WaitForSeconds(0.5f);

        isParry = false;
    }

    #endregion



}
