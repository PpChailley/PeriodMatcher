using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Gbd.PeriodMatching.Tools.Tests
{
  public class PowerOfTwoMathTester
  {


    [TestCase(0x1, Result=true)]
    [TestCase(0x2, Result=true)]
    [TestCase(0x4, Result = true)]
    [TestCase(0x8, Result = true)]
    [TestCase(0x10, Result = true)]
    [TestCase(0x20, Result = true)]
    [TestCase(0x40, Result = true)]
    [TestCase(0x80, Result = true)]
    [TestCase(0x100, Result = true)]
    [TestCase(0x1000, Result = true)]
    [TestCase(0x10000, Result = true)]
    [TestCase(0x100000, Result = true)]
    [TestCase(0x100000, Result = true)]
    [TestCase(0x1000000, Result = true)]
    [TestCase(0x10000000, Result = true)]
    [TestCase(0x100000000, Result = true)]
    [TestCase(0x1000000000, Result = true)]
    [TestCase(0x10000000000, Result = true)]
    [TestCase(0x100000000000, Result = true)]
    [TestCase(0x1000000000000, Result = true)]
    [TestCase(0x10000000000000, Result = true)]
    [TestCase(0x100000000000000, Result = true)]
    [TestCase(0x1000000000000000, Result = true)]

    [TestCase(0x0, Result = false)]
    [TestCase(0x3, Result = false)]
    [TestCase(0x5, Result=false)]
    [TestCase(0x7, Result=false)]
    [TestCase(0x9, Result=false)]
    [TestCase(0xF, Result=false)]
    [TestCase(0x11, Result=false)]
    [TestCase(0x1F, Result=false)]
    [TestCase(0x21, Result=false)]
    [TestCase(0x3F, Result=false)]
    [TestCase(0x41, Result=false)]
    [TestCase(0xFF, Result=false)]
    [TestCase(0x101, Result=false)]
    [TestCase(0xFFF, Result=false)]
    [TestCase(0x1001, Result=false)]
    [TestCase(0xFFFF, Result=false)]
    [TestCase(0x10001, Result=false)]
    [TestCase(0xFFFFF, Result=false)]
    [TestCase(0x100001, Result=false)]
    [TestCase(0xFFFFFF, Result=false)]
    [TestCase(0x1000001, Result=false)]
    [TestCase(0xFFFFFFF, Result=false)]
    [TestCase(0x10000001, Result=false)]
    [TestCase(0xFFFFFFFF, Result=false)]
    [TestCase(0x100000001, Result=false)]
    [TestCase(0xFFFFFFFFF, Result=false)]
    [TestCase(0x100000001, Result=false)]
    [TestCase(0xFFFFFFFFFF, Result=false)]
    [TestCase(0x10000000001, Result=false)]
    [TestCase(0xFFFFFFFFFFF, Result=false)]
    [TestCase(0x100000000001, Result=false)]
    [TestCase(0xFFFFFFFFFFFF, Result=false)]
    [TestCase(0x1000000000001, Result=false)]
    [TestCase(0xFFFFFFFFFFFFF, Result=false)]
    [TestCase(0x10000000000001, Result=false)]
    [TestCase(0xFFFFFFFFFFFFFF, Result=false)]
    [TestCase(0x100000000000001, Result=false)]
    [TestCase(0xFFFFFFFFFFFFFFF, Result=false)]
    [TestCase(0x1000000000000001, Result=false)]
    //[TestCase(0xFFFFFFFFFFFFFFFF, Result=false)]
    public bool IsAPowerOfTwoTester(long value)
    {
      return PowerOfTwoMath.IsAPowerOfTwo(value);
    }


    [Test]
    public void IsAPowerOfTwoProductNominalTester(
      [Values(3, 4, 652, 0xBEEF, 0xABEEF, 0xEADBEEF)]      long reference,
      [Values(0, 1, 2, 31, 32, 61, 62)]                     int shift,
      [Values(-1, 0, 1, 7)]                             long offset)

    {
      long product = (reference << shift) + offset;
      long computedMultiplier;

      bool result = PowerOfTwoMath.IsAPowerOfTwoProduct(product, reference, out computedMultiplier);

      if (offset == 0)
      {
        Assert.That(result, Is.True);
        Assert.That(computedMultiplier, Is.EqualTo(((long)1) << shift));
      }
      else
      {
        Assert.That(result, Is.False);
      }
    }


    [TestCase(0, 0, true)]
    [TestCase(0, 1, false)]
    [TestCase(0, 0x7FFFFFFFFFFFFFFF, false)]
    [TestCase(0, 0xFFFFFFFFFFFFFFFF, false)]
    [TestCase(1, 0, false)]
    [TestCase(1, 1, true, Result = 1)]
    [TestCase(1, 2, false)]
    [TestCase(1, 0x7FFFFFFFFFFFFFFF, false)]
    [TestCase(1, 0xFFFFFFFFFFFFFFFF, false)]
    [TestCase(2, 0, false)]
    [TestCase(2, 1, true, Result = 2)]
    [TestCase(3, 0, false)]
    [TestCase(3, 1, false)]
    [TestCase(3, 2, false)]
    [TestCase(3, 3, true, Result = 1)]
    [TestCase(0xFF, 0, false)]
    [TestCase(0xFF, 1, false)]
    [TestCase(0xFF, 0xFF, true, Result = 1)]
    [TestCase(long.MaxValue, 1, false)]
    [TestCase(long.MaxValue, long.MaxValue, true, Result = 1)]
    public long IsAPowerOfTwoProductLimitTester(long product, long reference, bool expectedResult)
    {
      long computedMultiplier;
      bool result = PowerOfTwoMath.IsAPowerOfTwoProduct(product, reference, out computedMultiplier);

      Assert.That(result, Is.EqualTo(expectedResult));

      return computedMultiplier;
    }


  }
}
