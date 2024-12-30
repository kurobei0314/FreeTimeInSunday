using System.Collections.Generic;

public class DayResultViewModel
{
    private List<string> _resultTextLists;
    public DayResultViewModel()
    {
      _resultTextLists = new List<string>();
    }

    public void AddResultText(string resultText)
      => _resultTextLists.Add(resultText);
    
    public void ClearResultText()
      => _resultTextLists.Clear();

    public List<string> ResultTextLists => _resultTextLists;
}
