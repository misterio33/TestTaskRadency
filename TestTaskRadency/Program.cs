using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

Console.WriteLine(Order("45 34 24 108 76 58 64 130 80"));// 130 24 34 80 108 45 64 58 76"
Console.WriteLine("-----------------------");
Console.WriteLine(Order("    2022 70 123    3344 13 "));// "13 123 2022 70 3344"
Console.WriteLine("-----------------------");
Console.WriteLine(Order(""));// "Empty String"
Console.ReadKey();


string Order(string input)
{
    if (string.IsNullOrEmpty(input))
    {
        return "Empty String";
    }

    Dictionary<string, int> countedWeights = new();
    
    var inputNumbersArray = NormalizeInputString(input);
    
    foreach (var number in inputNumbersArray)
    {
        countedWeights[number] = CountNumberWeight(number);
    }

    var sortedDict = 
        from entry in countedWeights 
        orderby entry.Value, entry.Key
        select entry;

    return GenerateResult(sortedDict);
}

IEnumerable<string> NormalizeInputString(string input)
{
    input = Regex.Replace(Strings.Trim(input), @"\s+", " ");
    var normalizedString = input.Split(' ');
    return normalizedString;
}

int CountNumberWeight(string number)
{
    var weight = 0;
    var numberInt = Convert.ToInt32(number);

    while (numberInt != 0) 
    {
        weight += numberInt % 10;
        numberInt /= 10;
    }
    
    return weight;
}

string GenerateResult(IEnumerable<KeyValuePair<string, int>> sortedDict)
{
    StringBuilder result = new();

    foreach (var pair in sortedDict)
    {
        result.Append(pair.Key).Append(' ');
    }

    result.Length--;
    return result.ToString();
}