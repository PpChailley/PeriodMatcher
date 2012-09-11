using System;
using System.Collections.Generic;

namespace Gbd.PeriodMatching.Tools
{
  public static class RandomExtensions
  {

    public static void Shuffle<T>(this IList<T> list, Random rnd)
    {
      
      int n = list.Count;
      while (n > 1)
      {
        n--;
        int k = rnd.Next(n + 1);
        T value = list[k];
        list[k] = list[n];
        list[n] = value;
      }
    }


    public static void Shuffle<T>(this IList<T> list)
    {
      list.Shuffle(new Random());
    }

    public static long NextLong(this Random r, long maxValue)
    {
      double d = r.NextDouble();
      long randomValue = (long) (d*maxValue + 1);
      return randomValue;
    }

  }
}
