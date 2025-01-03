namespace VietnameseCurrencyConvert
{
    public class CurrencyConvert
    {
        public static string Convert(int number)
        {
            // Handle limits
            if (number < 0)
            {
                throw new Exception("Cannot convert negative number!");
            }

            if (number > 999999999)
            {
                throw new Exception("The number is too large to convert!");
            }

            List<string> result = [];

            int million = number / 1000000;
            int thousand = number % 1000000 / 1000;
            int hundred = number % 1000;

            if (million > 0)
            {
                ReadThreeDigits(million, ref result);
                result.Add("triệu");
            }

            if (thousand > 0)
            {
                ReadThreeDigits(thousand, ref result);
                result.Add("nghìn");
            }

            if (result.Count == 0 && hundred == 0) // Case only 0
            {
                result.Add("không");
            }
            else
            {
                ReadThreeDigits(hundred, ref result);
            }

            result.Add("đồng");

            return string.Join(" ", result);
        }

        private static void ReadThreeDigits(int number, ref List<string> result)
        {
            // Case 000
            if (number == 0)
            {
                return;
            }

            string[] numberNames = ["không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín"];

            int hundred = number / 100;
            int ten = number % 100 / 10;
            int one = number % 10;

            // Hundred
            if (result.Count == 0 ? hundred > 0 : hundred >= 0)
            {
                result.Add(numberNames[hundred] + " trăm");

                if (ten == 0 && one != 0)
                {
                    result.Add("linh");
                }
            }

            // Ten
            if (ten > 0)
            {
                result.Add(ten == 1 ? "mười" : numberNames[ten] + " mươi");
            }

            // One
            if (one > 0)
            {
                if (one == 1 && ten > 1)
                {
                    result.Add("mốt");
                }
                else if (one == 5 && ten > 0)
                {
                    result.Add("lăm");
                }
                else
                {
                    result.Add(numberNames[one]);
                }
            }
        }
    }
}