using UnityEngine;
using Pathfinding;

public class NPCNavigation : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Vector2 _targetDestination;
    [Range (0f, 5f)][SerializeField] private float _nextWaypointDistance = 0.1f;

    [Header("References")]
    [SerializeField] private Seeker _seeker;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private UnitMovementController _unitMovementController;

    private Path _path;
    private int _currentWaypoint = 0;
    private bool _reachedEndOfPath = false;
    private Vector2 _direction = Vector2.zero;

    void Awake()
    {
        _seeker ??= GetComponent<Seeker>();
        _rigidbody2D ??= GetComponent<Rigidbody2D>();
        _unitMovementController ??= GetComponent<UnitMovementController>();
    }

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
            _unitMovementController.ApplyInput(Vector2.zero);
            return;
        }
        else
        {
            _reachedEndOfPath = false;
        }
        if (_currentWaypoint > 0)
        {
            _direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rigidbody2D.position).normalized;
            _unitMovementController.ApplyInput(_direction);

            float distance = Vector2.Distance(transform.position, _path.vectorPath[_currentWaypoint]);
            if (distance < _nextWaypointDistance)
            {
                _currentWaypoint++;
            }
        }
        else
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
        //_unitMovementController.ApplyInput(Vector2.zero);
        _targetDestination = newDestination;
        UpdatePath();
    }

    public void Stop()
    {
        _reachedEndOfPath = true;
        _path = null;
    }
}
