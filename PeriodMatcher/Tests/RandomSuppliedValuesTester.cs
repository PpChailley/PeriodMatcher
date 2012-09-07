using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gbd.PeriodMatching.Tools;
using NLog;
using NUnit.Framework;

namespace Gbd.PeriodMatching.Tests
{
  public class RandomSuppliedValuesTester : PeriodMatcherTester
  {


    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    private readonly Random _rnd = new Random(DateTime.Now.Millisecond);






    #region Smoke Self Tests

    [Test]
    [Category("SelfTests")]
    public void NCrunchSmokeTest()
    {
      throw new NotImplementedException("Just check that this is executed");
    }

    [Test]
    [Category("SelfTests")]
    public void NCrunchSmokeTest2()
    {
      throw new NotImplementedException("Just check that this is executed");
    }

    [TestCase(100, Result = 10)]
    [Category("SelfTests")]
    public long a(long selectedTimer)
    {
      //SplitLong split = new SplitLong(selectedTimer);
      //return MultiplyByRandomPowerOf2NoOverflow(split.HOB, split.LOB);
      return (long)10;

    }


    [TestCase(100)]
    [Category("SelfTests")]
    public void b(long selectedTimer)
    {
      //SplitLong split = new SplitLong(selectedTimer);
      //return MultiplyByRandomPowerOf2NoOverflow(split.HOB, split.LOB);
      throw new NotImplementedException();

    }


    #endregion


    #region Self Tests

    [Test]
    public void AssertIsAPowerOfTwoSelfTestsSuccess(
      [Range(0, 62, 1)]         int power)
    {
      long shouldBeAPowerOf2 = ((long) 1) << power;
      AssertIsAPowerOfTwo(shouldBeAPowerOf2);
    }

    [Test]
    [ExpectedException(typeof(AssertionException))]
    public void AssertIsAPowerOfTwoSelfTestsFailure(
      [Range(2, 62, 1)]         int power,
      [Values(-1, 1)]           int offset)
    {
      long shouldNotBeAPowerOf2 = ((long)1) << power;
      shouldNotBeAPowerOf2 += offset;
      AssertIsAPowerOfTwo(shouldNotBeAPowerOf2);
    }



    #endregion


    #region Helpers

    public void AssertIsAPowerOfTwo(long a)
    {
      long currentPowerOfTwo = (long.MaxValue/2) + 1;
      Assert.That(currentPowerOfTwo, Is.EqualTo(((long) 1) << 62));

      while(currentPowerOfTwo > 0)
      {
        if (a == currentPowerOfTwo)
          return;

        currentPowerOfTwo = currentPowerOfTwo / 2;
      }

      throw new AssertionException(String.Format("Provided number is not a power of 2: {0:0} (0x{0:X16})", a));
    }

    #endregion



  }
}
