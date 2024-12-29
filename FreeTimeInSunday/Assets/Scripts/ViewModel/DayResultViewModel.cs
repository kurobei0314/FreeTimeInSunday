using System.Collections.Generic;
using UnityEngine;

public class DayResultViewModel
{
    private List<string> _resultTextLists;
    public DayResultViewModel()
    {
      _resultTextLists = new List<string>();
    }

    public void AddResultText(string resultText)
      => _resultTextLists.Add(resultText);

    public List<string> ResultTextLists;
}
