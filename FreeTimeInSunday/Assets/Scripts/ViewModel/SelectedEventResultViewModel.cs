using UnityEngine;

public class SelectedEventResultViewModel
{
    private int _afterHP;
    private int _afterElapsedTime;
    private string _description;
    private string _resultDescription;
    private bool _isPassedDay;
    public SelectedEventResultViewModel(int afterHP,
                                        int afterElapsedTime,
                                        string description,
                                        string resultDescription,
                                        bool isPassedDay)
    {
        _afterHP = afterHP;
        _afterElapsedTime = afterElapsedTime;
        _description = description;
        _resultDescription = resultDescription;
        _isPassedDay = isPassedDay;
    }
    public int AfterHP => _afterHP;
    public int AfterElapsedTime => _afterElapsedTime;
    public string Description => _description;
    public string ResultDescription => _resultDescription;
    public bool IsPassedDay => _isPassedDay;
}
