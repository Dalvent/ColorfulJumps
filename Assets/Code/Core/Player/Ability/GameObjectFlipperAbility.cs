using Code.Ability;
using UnityEngine;

public class GameObjectFlipperAbility : BaseAbility
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private bool _isFacingRight = true;

    public bool IsFacingRight => _isFacingRight;

    private bool NeedFaceRight(float direction)
    {
        return direction > 0 && !_isFacingRight;
    }

    private bool NeedFaceLeft(float direction)
    {
        return direction < 0 && _isFacingRight;
    }

    public void FaceTo(float direction)
    {
        if (NeedFaceLeft(direction))
            Flip();
        else if (NeedFaceRight(direction))
            Flip();
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        var gameObjectScale = _gameObject.transform.localScale;
        gameObjectScale.x = -gameObjectScale.x;
        _gameObject.transform.localScale = gameObjectScale;
    }
}