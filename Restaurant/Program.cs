using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Restaurant
{
    public class RestaurantPick
    {
        #region You_should_not_modify_this_region


        private class Restaurant
        {
            public int RestaurantId { get; set; }
            public Dictionary<string, decimal> Menu { get; set; }
        }

        private readonly List<Restaurant> _restaurants = new List<Restaurant>();

        /// <summary>
        /// Reads the file specified at the path and populates the restaurants list
        /// </summary>
        /// <param name="filePath">Path to the comma separated restuarant menu data</param>
        public void ReadRestaurantData(string filePath)
        {
            try
            {
                var records = File.ReadLines(filePath);

                foreach (var record in records)
                {
                    var data = record.Split(',');
                    var restaurantId = int.Parse(data[0].Trim());
                    var restaurant = _restaurants.Find(r => r.RestaurantId == restaurantId);

                    if (restaurant == null)
                    {
                        restaurant = new Restaurant { Menu = new Dictionary<string, decimal>() };
                        _restaurants.Add(restaurant);
                    }

                    restaurant.RestaurantId = restaurantId;
                    restaurant.Menu.Add(data.Skip(2).Select(s => s.Trim()).Aggregate((a, b) => a.Trim() + "," + b.Trim()), decimal.Parse(data[1].Trim()));
                }

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        static void Main(string[] args)
        {
            var restaurantPicker = new RestaurantPick();
            
            restaurantPicker.ReadRestaurantData(
                Path.GetFullPath(
                    Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, @"../../../../restaurant_data.csv")
                    )
                );

            // Item is found in restaurant 2 at price 6.50
            var bestRestaurant = restaurantPicker.PickBestRestaurant("burdekin_plum,canistel,black_walnut,black_raspberry,black_sapote");

            Console.WriteLine(bestRestaurant.Item1 + ", " + bestRestaurant.Item2);

            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        #endregion



        #region You_can_modify_this_region
        /// <summary>
        /// Takes in items you would like to eat and returns the best restaurant that serves them.
        /// </summary>
        /// <param name="items">Items you would like to eat (seperated by ',')</param>
        /// <returns>Restaurant Id and price tuple</returns>
        public Tuple<int, decimal> PickBestRestaurant(string items)
        {

            var items1 = items.Split(",").ToList();
            decimal bestPrice = -1;
            var bestResturant = -1;
            foreach (var resturant in _restaurants)
            {
                var n = new List<string>(resturant.Menu.Keys);
                List<String> allItems = new List<string>();

                foreach (var temp2 in n)
                {
                    var temp3 = temp2.Split(",").ToList();
                    temp3.ForEach(item => allItems.Add(item));
                }

                var firstNotSecond = items1.Except(allItems).ToList();
                if(firstNotSecond.Count > 0)
                {
                    continue;
                }

                for (int x = 0; x < n.Count; x++)
                {
                    var split = n[x].Split(',').ToList();
                    firstNotSecond = split.Except(items1).ToList();
                    if(firstNotSecond.Count == split.Count)
                    {
                        n.RemoveAt(x);
                        x--;
                    }
                }
                List<List<String>> listofMenuOfOneResturant = new List<List<string>>();
                for (int k = 1; k <= items1.Count; k++)
                {
                    foreach (IEnumerable<string> i in Combinations(n, k))
                    {
                        var tempList = i.ToList();
                        List<String> temp1 = new List<string>();
                        foreach (var temp2 in tempList)
                        {
                            var temp3 = temp2.Split(",").ToList();
                            temp3.ForEach(item => temp1.Add(item));
                        }

                        firstNotSecond = items1.Except(temp1).ToList();
                        var dontAdd = false;
                        if (firstNotSecond.Count == 0)
                        {
                            foreach (var check in tempList)
                            {
                                firstNotSecond = items1.Except(check.Split(",").ToList()).ToList();
                                if (firstNotSecond.Count() == items1.Count)
                                {
                                    dontAdd = true;
                                }
                            }
                            if (dontAdd != true)
                                listofMenuOfOneResturant.Add(i.ToList());
                        }
                    }
                }
               

                foreach (var comb in listofMenuOfOneResturant)
                {
                    decimal tempCost = 0;
                    foreach(var oneComb in comb)
                    {
                        tempCost += resturant.Menu[oneComb];
                    }
                    if(bestPrice==-1 || bestPrice > tempCost)
                    {
                        bestPrice = tempCost;
                        bestResturant = resturant.RestaurantId;
                    }
                    
                  

                }                
            }
            if (bestResturant == -1)
            {
                return null;
            }
            return new Tuple<int, decimal>(bestResturant, bestPrice);
        }


        //Combinations reference from https://www.technical-recipes.com/2017/obtaining-combinations-of-k-elements-from-n-in-c/

        private static bool NextCombination(IList<int> num, int n, int k)
        {
            bool finished;

            var changed = finished = false;

            if (k <= 0) return false;

            for (var i = k - 1; !finished && !changed; i--)
            {
                if (num[i] < n - 1 - (k - 1) + i)
                {
                    num[i]++;

                    if (i < k - 1)
                        for (var j = i + 1; j < k; j++)
                            num[j] = num[j - 1] + 1;
                    changed = true;
                }
                finished = i == 0;
            }

            return changed;
        }

        private static IEnumerable Combinations<T>(IEnumerable<T> elements, int k)
        {
            var elem = elements.ToArray();
            var size = elem.Length;

            if (k > size) yield break;

            var numbers = new int[k];

            for (var i = 0; i < k; i++)
                numbers[i] = i;

            do
            {
                yield return numbers.Select(n => elem[n]);
            } while (NextCombination(numbers, size, k));
        }


        #endregion
    }
}

