using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager instance;

    GameManager.GameStatus gameStatus;
    bool isAnimating;

    [SerializeField] Transform detectivePiece;
    [SerializeField] Transform nPCSSpotlightPos;
    [SerializeField] Transform detectiveSpotlightPos;

    [Header("Hand Instances")]
    [SerializeField] GameObject LeftHand;
    [SerializeField] GameObject RightHand;

    [Header("Available Positions")]
    [SerializeField] Transform leftRestPos;
    [SerializeField] Transform rightRestPos;

    [SerializeField] Transform detectivePieceOffsetPos;

    Vector3 tempNPCPos;
    Vector3 tempDetectivePos;

    public bool IsAnimating() { return isAnimating; }
    public Transform GetRightHand() { return RightHand.transform; }
    public Transform GetLeftHand() { return LeftHand.transform; }

    private void Awake()
    {
        if (instance != null) Debug.Log("Error: There are multiple instances exits at the same time (HandManager)");
        instance = this;
    }

    void Start()
    {
        GameManager.instance.onStatusUpdated += UpdateHands;
    }

    private void UpdateHands()
    {
        gameStatus = GameManager.instance.GetStatus();  

        switch (gameStatus)
        {
            case GameManager.GameStatus.None:
                break;
            case GameManager.GameStatus.Table:
                StartCoroutine(ChangeHandPosition(leftRestPos, LeftHand.transform));
                StartCoroutine(ChangeHandPosition(rightRestPos, RightHand.transform));
                break;
            case GameManager.GameStatus.Map:
                break;
            case GameManager.GameStatus.Diorama:
                StartCoroutine(ChangeHandPosition(leftRestPos, LeftHand.transform));
                StartCoroutine(ChangeHandPosition(rightRestPos, RightHand.transform));
                break;
            case GameManager.GameStatus.Newspaper:
                break;
            case GameManager.GameStatus.PlayingCard:
                break;
            case GameManager.GameStatus.InspectEvidence:
                break;
            case GameManager.GameStatus.Call:
                break;
            case GameManager.GameStatus.Dialogue:
                StartCoroutine(ChangeHandPosition(detectivePieceOffsetPos, LeftHand.transform));
                StartCoroutine(ChangeHandPosition(GameManager.instance.GetSelectedNPC().GetHandOffsetPos(), RightHand.transform));
                break;
        }
    }

    public void StartSwapDioramaHandPosition()
    {
        StartCoroutine(ChangeHandPosition(DioramaManager.instance.GetCurrentDiorama().GetComponent<Diorama>().GetLeftHandOffset(), LeftHand.transform));
        StartCoroutine(ChangeHandPosition(DioramaManager.instance.GetCurrentDiorama().GetComponent<Diorama>().GetRightHandOffset(), RightHand.transform));
    }

    public void BackToTable()
    {
        StartCoroutine(ChangeHandPosition(leftRestPos, LeftHand.transform));
        StartCoroutine(ChangeHandPosition(rightRestPos, RightHand.transform));
    }

    public void ChangeHandParent(Transform _to, Transform _hand)
    {
        _hand.SetParent(_to);
    }

    public void MovePiecesBack()
    {
        StartCoroutine(ChangePiecePosition(tempNPCPos, GameManager.instance.GetSelectedNPC().transform));
        StartCoroutine(ChangePiecePosition(tempDetectivePos, detectivePiece));
    }

    public void MoveNPCPieceToSpotlight()
    {
        tempNPCPos = GameManager.instance.GetSelectedNPC().transform.position;
        StartCoroutine(ChangePiecePosition(nPCSSpotlightPos.position + transform.up * 0.25f, GameManager.instance.GetSelectedNPC().transform));
    }

    public void MoveDetectivePieceToSpotlight()
    {
        tempDetectivePos = detectivePiece.position;
        StartCoroutine(ChangePiecePosition(detectiveSpotlightPos.position - transform.up * 0.2f, detectivePiece));
    }

    private void AdditionalTrigger()
    {
        switch (gameStatus)
        {
            case GameManager.GameStatus.None:
                break;
            case GameManager.GameStatus.Table:
                break;
            case GameManager.GameStatus.Map:
                break;
            case GameManager.GameStatus.Diorama:
                break;
            case GameManager.GameStatus.Newspaper:
                break;
            case GameManager.GameStatus.PlayingCard:
                break;
            case GameManager.GameStatus.InspectEvidence:
                break;
            case GameManager.GameStatus.Call:
                break;
            case GameManager.GameStatus.Dialogue:
                MoveDetectivePieceToSpotlight();
                MoveNPCPieceToSpotlight();
                break;
            default:
                break;
        }
    }

    public void MovePiecesUpAndDown(int _targetIndex)
    {
        StartCoroutine(MovePiecesUpAndDownEnume(_targetIndex));
    }

    IEnumerator MovePiecesUpAndDownEnume(int _targetIndex)
    {
        Transform _piece = transform;

        switch (_targetIndex)
        {
            case 0:
                _piece = detectivePiece;
                break;
            case 1:
                _piece = GameManager.instance.GetSelectedNPC().transform;
                break;
        }

        float offset = 0.1f;
        float frequently = 10;
        float startY = _piece.position.y;
        float time = 0.0f;

        while (NarrativeGame.Dialogue.PlayerConversant.instance.GetIsAudioPlaying() || startY > _piece.position.y + 0.001f || startY < _piece.position.y - 0.001f)
        {
            yield return new WaitForSeconds(Time.deltaTime);

            float sin = Mathf.Sin(frequently * time);

            time += Time.deltaTime;

            float newY = startY + offset * sin;

            _piece.position = new Vector3(_piece.position.x, newY, _piece.position.z);
        }

        _piece.position = new Vector3(_piece.position.x, startY, _piece.position.z);
    }

    IEnumerator ChangeHandPosition(Transform _to, Transform _hand)
    {
        if (_to == _hand.parent) yield break;

        isAnimating = true;

        _hand.SetParent(null);

        float lerp = 0;

        Vector3 startPos = _hand.position;
        Vector3 endPos = _to.position;

        Vector3 startAngle = _hand.rotation.eulerAngles;
        Vector3 endAngle = _to.rotation.eulerAngles;

        float delta = Time.deltaTime / 0.75f;

        while (lerp < 1)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            lerp += delta;

            float angle = 90 - lerp * 180;

            float y = 0.75f * Mathf.Cos(angle * Mathf.Deg2Rad);

            _hand.position = Vector3.Lerp(startPos, endPos, lerp);
            _hand.position += transform.up * y;
            _hand.rotation = Quaternion.Slerp(Quaternion.Euler(startAngle), Quaternion.Euler(endAngle), lerp);
        }

        _hand.SetParent(_to);

        isAnimating = false;

        AdditionalTrigger();
    }

    IEnumerator ChangePiecePosition(Vector3 _to, Transform _piece)
    {
        isAnimating = true;

        float lerp = 0;

        Vector3 startPos = _piece.position;
        Vector3 endPos = _to;

        float delta = Time.deltaTime / 0.5f;

        while (lerp < 1)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            lerp += delta;

            float angle = 90 - lerp * 180;

            float y = 0.75f * Mathf.Cos(angle * Mathf.Deg2Rad);

            _piece.position = Vector3.Lerp(startPos, endPos, lerp);
            _piece.position += transform.up * y;
        }

        isAnimating = false;
    }
}
