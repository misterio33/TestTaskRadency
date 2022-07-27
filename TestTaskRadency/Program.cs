using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

Console.WriteLine(Order("3 16 9 38 95 1131268 16  49455 347464 59544965313 496636983114762 85246814996697111111111111111111111"));// 130 24 34 80 108 45 64 58 76"
Console.WriteLine("-----------------------");
Console.WriteLine(Order("    2022 70 123    3344 13 "));// "13 123 2022 70 3344"
Console.WriteLine("-----------------------");
Console.WriteLine(Order(null));// "Empty String"
Console.ReadKey();

string Order(string input)
{
    if (string.IsNullOrEmpty(input))
    {
        return string.Empty;
    }

    Dictionary<string, int> countedWeights = new();
    Dictionary<string, int> countedWeightsAmount = new();
    
    var inputNumbersArray = NormalizeInputString(input);
    
    foreach (var number in inputNumbersArray)
    {
        countedWeights[number] = CountNumberWeightNew(number);
        
        if (countedWeightsAmount.ContainsKey(number))
        {
            countedWeightsAmount[number] += 1;
        }
        else
        {
            countedWeightsAmount.Add(number, 1);
        }
    }

    var sortedDict = 
        from entry in countedWeights 
        orderby entry.Value, entry.Key
        select entry;

    return GenerateResult(sortedDict, countedWeightsAmount);
}

IEnumerable<string> NormalizeInputString(string input)
{
    input = Regex.Replace(Strings.Trim(input), @"\s+", " ");
    var normalizedString = input.Split(' ');
    return normalizedString;
}

int CountNumberWeightNew(string number)
{
    return number.Where(t => t is >= '0' and <= '9').Sum(t => (t - '0'));
}

string GenerateResult(IEnumerable<KeyValuePair<string, int>> sortedDict, Dictionary<string, int> numberInStringCounter)
{
    StringBuilder result = new();

    foreach (var pair in sortedDict)
    {
        for (var i = 0; i < numberInStringCounter[pair.Key] ; i++)
        {
            result.Append(pair.Key).Append(' ');
        }
    }

    result.Length--;
    return result.ToString();
}