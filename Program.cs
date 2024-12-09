using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        double input = 324532445464;
        string result = MoneyConvert(input);
        Console.WriteLine(result);
    }

    private static void HandleUnit(int unitCounter, ref List<string> result)
    {
        if (unitCounter < 0)
        {
            return;
        }

        string[] units = ["nghìn", "triệu", "tỉ"];
        int unitsLength = units.Length;
        int lastUnitIndex = unitsLength - 1;

        int unitIndex = unitCounter % unitsLength;
        int overIndex = unitCounter / unitsLength;

        result.Add(units[unitIndex]);

        for (int j = 0; j < overIndex; j++)
        {
            result.Add(units[lastUnitIndex]);
        }
    }

    private static string MoneyConvert(double input)
    {
        List<string> result = [];
        string inputText = FillInput(input);
        int inputLength = inputText.Length;
        int unitCounter = inputLength / 3 - 2;

        for (int i = 0; i < inputLength; i += 3)
        {
            string hundred = inputText[i].ToString();
            string ten = inputText[i + 1].ToString();
            string one = inputText[i + 2].ToString();

            if (hundred != "0" || ten != "0" || one != "0")
            {
                result.Add(HandleHundred(hundred));
                result.Add(HandleTen(ten, one));
                result.Add(HandleOne(one, ten));
                HandleUnit(unitCounter, ref result);
            }

            unitCounter--;
        }

        result.Add("đồng");
        result = result.Where(word => !string.IsNullOrEmpty(word)).ToList();
        return string.Join(" ", result);
    }

    private static string FillInput(double input)
    {
        string inputText = input.ToString("F0");

        while (inputText.Length % 3 != 0)
        {
            inputText = "-" + inputText;
        }

        return inputText;
    }

    private static string GetText(string number)
    {
        return number switch
        {
            "0" => "không",
            "1" => "một",
            "2" => "hai",
            "3" => "ba",
            "4" => "bốn",
            "5" => "năm",
            "6" => "sáu",
            "7" => "bảy",
            "8" => "tám",
            "9" => "chín",
            _ => string.Empty
        };
    }

    private static string HandleHundred(string number)
    {
        return number == "-" ? string.Empty : GetText(number) + " trăm";
    }

    private static string HandleTen(string number, string one)
    {
        if (number == "-")
        {
            return string.Empty;
        }
        else if (number == "0" && one == "0")
        {
            return string.Empty;
        }
        else if (number == "0")
        {
            return " lẻ";
        }
        else if (number == "1")
        {
            return " mười";
        }
        else
        {
            return GetText(number) + " mươi";
        }
    }

    private static string HandleOne(string number, string ten)
    {
        if (number == "5" && ten != "0" && ten != "-")
        {
            return "lăm";
        }
        else if (number == "1" && ten != "0" && ten != "1" && ten != "-")
        {
            return "mốt";
        }
        else if (number == "0")
        {
            return string.Empty;
        }
        else
        {
            return GetText(number);
        }
    }
}