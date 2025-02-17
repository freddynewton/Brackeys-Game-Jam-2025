using UnityEngine;
using Pathfinding;

public class NPCNavigation : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Vector2 _targetDestination;
    [Range (0f, 5f)][SerializeField] private float _nextWaypointDistance = 3f;
    [Range (0f, 3f)][SerializeField] private float _stoppingDistance = 1f;
    [SerializeField] private bool _isChasing = true;

    [Header("References")]
    [SerializeField] private Seeker _seeker;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private UnitMovementController _unitMovementController;

    [HideInInspector] public bool _goingToExit = false;

    private Path _path;
    private int _currentWaypoint = 0;
    private bool _reachedEndOfPath = false;
    private Vector2 _direction = Vector2.zero;

    void Awake()
    {
        _seeker ??= GetComponent<Seeker>();
        _rigidbody2D ??= GetComponent<Rigidbody2D>();
        _unitMovementController ??= GetComponent<UnitMovementController>();
        if (_targetDestination != null)
        {
            UpdatePath();
        }

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Pathfinding();
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            _path = p;
            _currentWaypoint = 0;
        }
    }

    private void Pathfinding()
    {
        if (_path == null)
        {
            return;
        }
        
        if(_currentWaypoint >= _path.vectorPath.Count)
        {
            _reachedEndOfPath = true;
            _direction = Vector2.zero;
            return;
        }
        else
        {
            _reachedEndOfPath = false;
        }

        _direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rigidbody2D.position).normalized;
        _unitMovementController.ApplyInput(_direction);

        float distance = Vector2.Distance(_rigidbody2D.position, _path.vectorPath[_currentWaypoint]);
        if (distance < _nextWaypointDistance)
        {
            _currentWaypoint++;
        }
    }

    private void UpdatePath()
    {
        if (_seeker.IsDone())
        {
            _seeker.StartPath(_rigidbody2D.position, _targetDestination, OnPathComplete);
        }
    }

    public bool HasStopped()
    {
        return _reachedEndOfPath;
    }

    public Vector2 GetCurrentDirection()
    {
        return _direction;
    }

    public void SetNewDestination(Vector2 newDestination)
    {
        _targetDestination = newDestination;
        UpdatePath();
    }
}
