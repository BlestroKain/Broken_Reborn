using System.ComponentModel;
using Intersect.Enums;

namespace Intersect.Editor.General;

public class NotifiableBestiaryUnlock : INotifyPropertyChanged
{
    private BestiaryUnlock _unlockType;
    private int _amount;

    public string DisplayName => $"{_unlockType}: {_amount}";

    public BestiaryUnlock UnlockType
    {
        get => _unlockType;
        set
        {
            if (_unlockType == value)
            {
                return;
            }

            _unlockType = value;
            OnPropertyChanged(nameof(UnlockType));
            OnPropertyChanged(nameof(DisplayName));
        }
    }

    public int Amount
    {
        get => _amount;
        set
        {
            if (_amount == value)
            {
                return;
            }

            _amount = value;
            OnPropertyChanged(nameof(Amount));
            OnPropertyChanged(nameof(DisplayName));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

