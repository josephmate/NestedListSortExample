using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedListSortExample
{
    public class CompareByPrice : IComparer<List<KeyValuePair<String, String>>>
    {
        public CompareByPrice()
        {
        }

        /**
         * https://docs.microsoft.com/en-us/dotnet/api/system.collections.icomparer.compare?view=netframework-4.8
         * 
         * A signed integer that indicates the relative values of x and y, as shown in the following table.
         * Value 	Meaning
         * Less than 0 	x is less than y.
         * 0 	x equals y.
         * Greater than 0 	x is greater than y. 
         */
        public int Compare(List<KeyValuePair<String, String>> x, List<KeyValuePair<String, String>> y)
        {
            Nullable<double> xvalue = findPrice(x);
            Nullable<double> yvalue = findPrice(y);

            if(xvalue == null && yvalue == null)
            {
                return 0;
            }

            if(xvalue == null)
            {
                // yvalue is not null and should come before xvalue
                return 1;
            }

            if (yvalue == null)
            {
                // yvalue is  null and should after xvalue
                return -1;
            }

            return xvalue.Value.CompareTo(yvalue.Value);
        }

        public Nullable<double> findPrice(List<KeyValuePair<String, String>> list)
        {
            foreach(KeyValuePair<String, String>keyValue in list)
            {
                if(keyValue.Key.Equals("price"))
                {
                    try
                    {
                        return double.Parse(keyValue.Value);
                    } catch(FormatException)
                    {
                        // could not parse the double from the string
                        return null;
                    }
                    
                }
            }
            // list did not have a price keyvalue
            return null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<List<KeyValuePair<String, String>>> data = GenerateSampleList();

            Console.WriteLine("=== Before Sorting ===");
            printList(data);


            data.Sort(new CompareByPrice());
            Console.WriteLine("=== After Sorting ===");
            printList(data);
        }

        static void printList(List<List<KeyValuePair<String, String>>> data)
        {
            int i = 0;
            foreach (List<KeyValuePair<String, String>> subList in data)
            {
                i++;
                Console.WriteLine(i);
                foreach (KeyValuePair<String, String> keyValue in subList)
                {
                    Console.WriteLine("\t" + keyValue.Key + "=" + keyValue.Value);
                }
            }
        }

        /**
         * Generates a sample list/
         */
        static List<List<KeyValuePair<String, String>>> GenerateSampleList()
        {
            return new List<List<KeyValuePair<String, String>>>
            {
                new List<KeyValuePair<String,String>>
                {
                    new KeyValuePair<string, string>("price", "1000"),
                    new KeyValuePair<string, string>("color", "blue"),
                    new KeyValuePair<string, string>("modelNumber", "123"),
                    new KeyValuePair<string, string>("manufacturer", "Asus")
                },
                new List<KeyValuePair<String,String>>
                {
                    new KeyValuePair<string, string>("color", "blue"),
                    new KeyValuePair<string, string>("price", "cannot parse this price"),
                    new KeyValuePair<string, string>("modelNumber", "123"),
                    new KeyValuePair<string, string>("manufacturer", "Asus")
                },
                new List<KeyValuePair<String,String>>
                {
                    new KeyValuePair<string, string>("color", "blue"),
                    new KeyValuePair<string, string>("modelNumber", "123"),
                    new KeyValuePair<string, string>("price", "10.1"),
                    new KeyValuePair<string, string>("manufacturer", "Asus")
                },
                // no price
                new List<KeyValuePair<String,String>>
                {
                    new KeyValuePair<string, string>("color", "blue"),
                    new KeyValuePair<string, string>("modelNumber", "123"),
                    new KeyValuePair<string, string>("manufacturer", "Asus")
                },
                new List<KeyValuePair<String,String>>
                {
                    new KeyValuePair<string, string>("color", "blue"),
                    new KeyValuePair<string, string>("modelNumber", "145"),
                    new KeyValuePair<string, string>("manufacturer", "Acer"),
                    new KeyValuePair<string, string>("price", "12")
                },
                new List<KeyValuePair<String,String>>
                {
                    new KeyValuePair<string, string>("price", "45"),
                    new KeyValuePair<string, string>("color", "blue"),
                    new KeyValuePair<string, string>("modelNumber", "56"),
                    new KeyValuePair<string, string>("manufacturer", "Dell")
                }
            };
        }
    }
}
