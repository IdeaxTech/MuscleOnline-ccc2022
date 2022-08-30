using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] DragHandler _moveController;

    [SerializeField] float _moveSpeed = 7f;

    private Vector2 _movePointerPosBigin;

    private Vector3 _moveVector;

    [SerializeField] private GameObject _camera;

    [SerializeField] private DragHandler _lookController;

    [SerializeField] private float _angularPerPixel = 1f;


    bool isOnGround = false; // 地面に立っているかどうか
    bool isJumping = false; // ジャンプしているかどうか
    private Animator animator;
    private Vector2 _lookPointerPosPre;

    private void Awake()
    {
        _moveController.OnBeginDragEvent += OnBeginDrag;
        _moveController.OnDragEvent += OnMoveDrag;
        _moveController.OnEndDragEvent += OnEndDrag;

        _lookController.OnBeginDragEvent += OnBeginDragLook;
        _lookController.OnDragEvent += OnLookDrag;

        animator = GetComponent<Animator>();
    }

    private void Update()
    {


        UpdateMove(_moveVector);
    }

    private void OnBeginDrag(PointerEventData eventData)
    {
        _movePointerPosBigin = eventData.position;
    }

    private void OnMoveDrag(PointerEventData eventData)
    {
        var vector = eventData.position - _movePointerPosBigin;
        _moveVector = new Vector3(vector.x, 0f, vector.y);

        animator.SetFloat("Movement", 0.7f);
    }

    private void OnEndDrag(PointerEventData eventData)
    {
        _moveVector = Vector3.zero;
        animator.SetFloat("Movement", 0f);
    }

    private void UpdateMove(Vector3 vector)
    {
        Vector3 cameraForward = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1)).normalized;
        //Debug.Log(_camera.transform.forward);
        Vector3 moveForward = cameraForward * vector.normalized.z + Camera.main.transform.right * vector.normalized.x;



        transform.position += moveForward * _moveSpeed * Time.deltaTime;

        //Debug.Log(vector.normalized);

        _camera.transform.position += moveForward * _moveSpeed * Time.deltaTime;
        if (_moveVector != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }

    }


    //カメラ移動


    private void OnBeginDragLook(PointerEventData eventData)
    {
        _lookPointerPosPre = _lookController.GetPositionOnCanvas(eventData.position);
    }

    private void OnLookDrag(PointerEventData eventData)
    {
        var pointerPosOnCanvas = _lookController.GetPositionOnCanvas(eventData.position);
        // キャンバス上で前フレームから何px操作したかを計算
        var vector = pointerPosOnCanvas - _lookPointerPosPre;
        // 操作量に応じてカメラを回転
        LookRotate(new Vector2(-vector.y, vector.x));
        _lookPointerPosPre = pointerPosOnCanvas;
    }

    private void LookRotate(Vector2 angles)
    {
        Vector2 deltaAngles = angles * _angularPerPixel;
        //transform.eulerAngles += new Vector3(0f, deltaAngles.y);
        _camera.transform.localEulerAngles += new Vector3(deltaAngles.x, deltaAngles.y);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BossEvent")
        {

            Invoke("ChangeScene", 0.1f);
        }
    }
    void ChangeScene()
    {
        SceneManager.LoadScene("AppearanceScene");
    }
}