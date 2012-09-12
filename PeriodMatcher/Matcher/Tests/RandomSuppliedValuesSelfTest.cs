using System;
using System.Collections.Generic;
using System.Linq;
using Gbd.PeriodMatching.Matcher;
using Gbd.PeriodMatching.Tools;
using Moq;
using NUnit.Framework;

namespace Gbd.PeriodMatching.Tests
{
  [TestFixture]
  public class RandomSuppliedValuesSelfTest
  {

    private RandomSuppliedValuesTester _testedTester;

    protected const int MaxP = PeriodMatcherTester.MaxP;
    protected const int MaxT = PeriodMatcherTester.MaxT;
    protected const int MaxS32 = PeriodMatcherTester.MaxS32;
    protected const uint MaxU32 = PeriodMatcherTester.MaxU32;
    protected const int MinS32 = PeriodMatcherTester.MinS32;
    protected const long MaxS64 = PeriodMatcherTester.MaxS64;
    protected const ulong MaxU64 = PeriodMatcherTester.MaxU64;
    protected const long MinS64 = PeriodMatcherTester.MinS64;

    [SetUp]
    public void SetUp()
    {
      _testedTester = new RandomSuppliedValuesTester();
    }

    [TearDown]
    public void TearDown()
    {
      
    }




    #region Self Tests

    [Test]
    public void AssertIsAPowerOfTwoSelfTestsSuccess(
      [Range(0, 62, 1)]         int power)
    {
      long shouldBeAPowerOf2 = ((long)1) << power;
      Assert.That(PowerOfTwoMath.IsAPowerOfTwo(shouldBeAPowerOf2), Is.True);
    }

    [Test]
    [ExpectedException(typeof(AssertionException))]
    public void AssertIsAPowerOfTwoSelfTestsFailure(
      [Range(2, 62, 1)]         int power,
      [Values(-1, 1)]           int offset)
    {
      long shouldNotBeAPowerOf2 = ((long)1) << power;
      shouldNotBeAPowerOf2 += offset;
      Assert.That(PowerOfTwoMath.IsAPowerOfTwo(shouldNotBeAPowerOf2), Is.True);
    }


    [Test]
    public void MultiplyByRandomPowerOf2NoOverflowSelfTests(
      [Values(0, int.MaxValue, 99999999)]            int HOB,
      [Values(1, int.MaxValue, 99997598)]            int LOB
      )
    {
      long period = new SplitLong((uint)HOB, (uint)LOB).ToLong();
      long timer = _testedTester.MultiplyByRandomPowerOf2NoOverflow(HOB, LOB);
      long multiplier = timer / period;

      Assert.That(multiplier * period, Is.EqualTo(timer), "Generated timer is not a multiple of the period (integer rounding occurred)");
      Assert.That(PowerOfTwoMath.IsAPowerOfTwo(multiplier), Is.True);
    }



    [Test]
    public void GeneratePeriodsForTimerTester(
      [Values(0, 1, 10, 912, MaxS32 - 1, MaxS32)]             int timerHOB,
      [Values(1, 10, 951, MaxS32 - 1, MaxS32)]                  int timerLOB,
      [Values(1, 2, 3, 4, 10, 100, MaxP - 1, MaxP)]             int nbPeriods)
    {
      SplitLong timer = new SplitLong((uint)timerHOB, (uint)timerLOB);

      List<long> periods = _testedTester.GeneratePeriodsForTimer(timer.ToLong(), nbPeriods);
      List<long> multipliers = periods.Select(period => period/timer.ToLong()).ToList();

      Assert.That(multipliers, Is.All.Matches(new Predicate<long>(PowerOfTwoMath.IsAPowerOfTwo)));
      
      foreach(long period in periods)
      {
        Assert.That(PowerOfTwoMath.IsAPowerOfTwoProduct(period, timer.ToLong()), Is.True);
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

    [TestCase(1L, 0)]
    [TestCase(1L, 1)]
    [TestCase(1L, 61)]
    [TestCase(1L, 62)]
    [TestCase(1L, 63, ExpectedException = typeof(InvalidOperationException))]
    [TestCase(1L, 64, ExpectedException = typeof(InvalidOperationException))]
    [TestCase(127L, 0)]
    [TestCase(127L, 1)]
    [TestCase(127L, 55)]
    [TestCase(127L, 56)]
    [TestCase(127L, 57, ExpectedException = typeof(InvalidOperationException))]
    [TestCase(127L, 63, ExpectedException = typeof(InvalidOperationException))]
    [TestCase(127L, 64, ExpectedException = typeof(InvalidOperationException))]
    [TestCase((long)MaxS32, 0)]
    [TestCase((long)MaxS32, 31)]
    [TestCase((long)MaxS32, 32)]
    [TestCase((long)MaxS32, 33, ExpectedException = typeof(InvalidOperationException))]
    [TestCase((long)MaxS32, 63, ExpectedException = typeof(InvalidOperationException))]
    [TestCase((long)MaxS32+1, 0)]
    [TestCase((long)MaxS32+1, 30)]
    [TestCase((long)MaxS32+1, 31)]
    [TestCase((long)MaxS32 + 1, 32, ExpectedException = typeof(InvalidOperationException))]
    [TestCase((long)MaxS32 + 1, 63, ExpectedException = typeof(InvalidOperationException))]
    [TestCase(MaxS64 -1, 0)]
    [TestCase(MaxS64 - 1, 1, ExpectedException = typeof(InvalidOperationException))]
    [TestCase(MaxS64 - 1, 2, ExpectedException = typeof(InvalidOperationException))]
    [TestCase(MaxS64 - 1, 63, ExpectedException = typeof(InvalidOperationException))]
    [TestCase(MaxS64, 0)]
    [TestCase(MaxS64, 1, ExpectedException = typeof(InvalidOperationException))]
    [TestCase(MaxS64, 2, ExpectedException = typeof(InvalidOperationException))]
    [TestCase(MaxS64, 63, ExpectedException = typeof(InvalidOperationException))]
    [Category("SelfTests")]
    public void MultiplyByRandomPowerOfTwoOverloads(long timer, int injectedShiftFromRandom)
    {
      Mock<Random> moqRandom = new Mock<Random>();
      moqRandom.Setup(r => r.Next(It.IsAny<int>())).Returns(injectedShiftFromRandom);
      //moqRandom.Verify(r => injectedShiftFromRandom, Times.Exactly(4));
      
      _testedTester.Rnd = moqRandom.Object;


      SplitLong splitTimer = new SplitLong(timer);
      long resultWithRealMethod = _testedTester.MultiplyByRandomPowerOf2NoOverflow(splitTimer.HOB, splitTimer.LOB);
      long resultWithOverloadLong = _testedTester.MultiplyByRandomPowerOf2NoOverflow(timer);
      long resultWithOverloadSplitLong = _testedTester.MultiplyByRandomPowerOf2NoOverflow(splitTimer);
      long resultWithOverload2Sint = _testedTester.MultiplyByRandomPowerOf2NoOverflow((int)splitTimer.HOB, (int)splitTimer.LOB);

      Assert.That(resultWithOverloadLong, Is.EqualTo(resultWithRealMethod));
      Assert.That(resultWithOverloadSplitLong, Is.EqualTo(resultWithRealMethod));
      Assert.That(resultWithOverload2Sint, Is.EqualTo(resultWithRealMethod));

    }


    #endregion

    #region Testing helpers

    [Test]
    [Category("SelfTests")]
    public void SplitLongTests(
      [Values(long.MinValue, long.MinValue + 1L, -1L, 0L, 1L, long.MaxValue - 1L, long.MaxValue)] long selectedTimer)
    {
      SplitLong s = new SplitLong(selectedTimer);
      long rebuilt = s.ToLong();

      Assert.That(rebuilt, Is.EqualTo(selectedTimer));
    }

    [Test]
    [Category("SelfTests")]
    public void SplitLongTests(
      [Values(int.MinValue, int.MinValue + 1, -1, 0, 1, int.MaxValue - 1, int.MaxValue)] int selectedTimer)
    {
      SplitLong s = new SplitLong(selectedTimer);
      long rebuilt = s.ToLong();

      Assert.That((int)rebuilt, Is.EqualTo(selectedTimer));
    }


    [Test]
    [Category("SelfTests")]
    public void SplitLongTests(
      [Values(0UL, 1UL, ulong.MaxValue - 1, ulong.MaxValue)] ulong selectedTimer)
    {
      SplitLong s = new SplitLong(selectedTimer);
      Assert.That(s.ToULong(), Is.EqualTo(selectedTimer));
    }


    #endregion



  }
}
