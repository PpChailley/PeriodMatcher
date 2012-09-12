using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Gbd.PeriodMatching.Tools
{
  public static class PowerOfTwoMath
  {


    public static bool IsAPowerOfTwo(long a)
    {
      long currentPowerOfTwo = (Int64.MaxValue / 2) + 1;
      Assert.That(currentPowerOfTwo, Is.EqualTo(((long)1) << 62));

      while (currentPowerOfTwo > 0)
      {
        if (a == currentPowerOfTwo)
          return true;

        currentPowerOfTwo = currentPowerOfTwo / 2;
      }

      //Log.Info(String.Format("Provided number is not a power of 2: {0:0} (0x{0:X16})", a));
      return false;
    }

    public static bool IsAPowerOfTwoProduct(long product, long reference)
    {
      long discarded;
      return IsAPowerOfTwoProduct(product, reference, out discarded);
    }


    public static bool IsAPowerOfTwoProduct(long product, long reference, out long multiplicator)
    {
      long currentPowerOfTwo = (Int64.MaxValue / 2) + 1;
      Assert.That(currentPowerOfTwo, Is.EqualTo(((long)1) << 62));

      while (currentPowerOfTwo > 0)
      {
        if (product == (currentPowerOfTwo * reference))
        {
          multiplicator = currentPowerOfTwo;
          return true;
        }

        currentPowerOfTwo = currentPowerOfTwo / 2;
      }

      //Log.Info(String.Format("Provided number is not a power of 2: {0:0} (0x{0:X16})", a));
      multiplicator = 0;
      return false;
    }
  }
}
