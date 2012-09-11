using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gbd.PeriodMatching.Tools;
using NUnit.Framework;

namespace Gbd.PeriodMatching.Tests
{
  public class RandomSuppliedValuesSelfTest :RandomSuppliedValuesTester
  {

    #region Self Tests

    [Test]
    public void AssertIsAPowerOfTwoSelfTestsSuccess(
      [Range(0, 62, 1)]         int power)
    {
      long shouldBeAPowerOf2 = ((long)1) << power;
      Assert.That(IsAPowerOfTwo(shouldBeAPowerOf2), Is.True);
    }

    [Test]
    [ExpectedException(typeof(AssertionException))]
    public void AssertIsAPowerOfTwoSelfTestsFailure(
      [Range(2, 62, 1)]         int power,
      [Values(-1, 1)]           int offset)
    {
      long shouldNotBeAPowerOf2 = ((long)1) << power;
      shouldNotBeAPowerOf2 += offset;
      Assert.That(IsAPowerOfTwo(shouldNotBeAPowerOf2), Is.True);
    }


    [Test]
    public void MultiplyByRandomPowerOf2NoOverflowSelfTests(
      [Values(0, int.MaxValue, 99999999)]            int HOB,
      [Values(1, int.MaxValue, 99997598)]            int LOB
      )
    {
      long period = new SplitLong((uint)HOB, (uint)LOB).ToLong();
      long timer = MultiplyByRandomPowerOf2NoOverflow(HOB, LOB);
      long multiplier = timer / period;

      Assert.That(multiplier * period, Is.EqualTo(timer), "Generated timer is not a multiple of the period (integer rounding occurred)");
      Assert.That(IsAPowerOfTwo(multiplier), Is.True);
    }



    [Test]
    public void GeneratePeriodsForTimerTester(
      [Values(0, 1, 10, 912, MaxS32 - 1, MaxS32)]             int timerHOB,
      [Values(1, 10, 951, MaxS32 - 1, MaxS32)]                  int timerLOB,
      [Values(1, 2, 3, 4, 10, 100, MaxP - 1, MaxP)]             int nbPeriods)
    {
      SplitLong timer = new SplitLong((uint)timerHOB, (uint)timerLOB);

      var periods = GeneratePeriodsForTimer(timer.ToLong(), nbPeriods);
      var multipliers = periods.Select(period => period/timer.ToLong()).ToList();

      Assert.That(multipliers, Is.All.Matches(new Predicate<long>(IsAPowerOfTwo)));
      
      foreach(long period in periods)
      {
        Assert.That(IsAPowerOfTwoProduct(period, timer.ToLong()), Is.True);
      }

    }




    #endregion

    #region Smoke Self Tests

    [Test]
    [Category("SelfTests")]
    [ExpectedException(typeof(NotImplementedException))]
    public void NCrunchSmokeTest()
    {
      throw new NotImplementedException("Just check that this is executed");
    }

    [Test]
    [Category("SelfTests")]
    [Ignore("This test requires Moq")]
    public void MultiplyByRandomPowerOfTwoOverloads(
      long timer,
      int injectedShiftFromRandom)
    {
      // Mock<Random> m = new Mock<Random>();

      SplitLong splitTimer = new SplitLong(timer);
      long resultWithRealMethod = MultiplyByRandomPowerOf2NoOverflow(splitTimer.HOB, splitTimer.LOB);
      long resultWithOverloadLong = MultiplyByRandomPowerOf2NoOverflow(timer);
      long resultWithOverloadSplitLong = MultiplyByRandomPowerOf2NoOverflow(splitTimer);
      long resultWithOverload2Sint = MultiplyByRandomPowerOf2NoOverflow((int)splitTimer.HOB, (int)splitTimer.LOB);

      Assert.That(resultWithOverloadLong, Is.EqualTo(resultWithRealMethod));
      Assert.That(resultWithOverloadSplitLong, Is.EqualTo(resultWithRealMethod));
      Assert.That(resultWithOverload2Sint, Is.EqualTo(resultWithRealMethod));

    }


    #endregion

    #region Testing helpers

    [Test]
    [Category("SelfTests")]
    public void SplitLongTests(
      [Values(long.MinValue, long.MinValue + 1L, -1L, long.MaxValue - 1L, long.MaxValue)] long selectedTimer)
    {
      SplitLong s = new SplitLong(selectedTimer);
      long rebuilt = s.ToLong();

      Assert.That(rebuilt, Is.EqualTo(selectedTimer));
    }

    [Test]
    [Category("SelfTests")]
    // NUnit is picky with longs that have int-compatible values (looks like it auto-casts them to int32)
    // For some obscure reason, values 0 and 1 fail the test outside the test code. Better ask StackOverflow someday
    public void SplitLongTests(
      [Values(int.MinValue, int.MinValue + 1, -1, int.MaxValue - 1, int.MaxValue)] int selectedTimer)
    {
      SplitLong s = new SplitLong(selectedTimer);
      long rebuilt = s.ToLong();

      Assert.That((int)rebuilt, Is.EqualTo(selectedTimer));
    }


    [Test]
    [Category("SelfTests")]
    public void SplitLongTests(
      [Values(0, 1, ulong.MaxValue - 1, ulong.MaxValue)] ulong selectedTimer)
    {
      SplitLong s = new SplitLong(selectedTimer);
      Assert.That(s.ToULong(), Is.EqualTo(selectedTimer));
    }


    #endregion



  }
}
