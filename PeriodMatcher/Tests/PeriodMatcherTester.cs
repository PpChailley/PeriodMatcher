using System.Collections.Generic;
using System.Collections.ObjectModel;
using Gbd.PeriodMatching.Matcher;
using NUnit.Framework;

namespace Gbd.PeriodMatching.Tests
{

  [TestFixture]
  class PeriodMatcherTester
  {

    private const int MaxP = PeriodMatcher.MaxPeriodsSupported;
    private const int MaxT = PeriodMatcher.MaxTimersSupported;

    private PeriodMatcher _sandbox;

    #region Setup And TearDown

    [SetUp]
    public void Setup()
    {
      _sandbox = new PeriodMatcher();
    }


    [TearDown]
    public void TearDown()
    {
      _sandbox = null;
      System.GC.Collect();
    }


    #endregion

    #region Smoke Tests

    //[Test]
    //public void AssignNoConstraintSmokeTest(
    //  [Values(0, 12, 777777)]     long period,
    //  [Range(0, MaxP, 1)]         int nbPeriods)
    [TestCase(12,13)]
    public void AssignNoConstraintSmokeTest(long period, int nbPeriods)
    {
      for (int i = 0; i < nbPeriods; i++)
      {
        _sandbox.PeriodsToMatch.Add(period);
      }

      _sandbox.Assign();

      Assert.That(_sandbox.TimersAssignment, Is.All.EqualTo(period));
    }


    [TestCase(false)]
    [TestCase(true, 12)]
    [TestCase(true, ExpectedException=typeof(AssertionException))]
    [TestCase(true, -1, ExpectedException=typeof(AssertionException))]
    [TestCase(true, MaxT+1, ExpectedException=typeof(AssertionException))]
    public void ForbiddenCasesSmokeTest(bool enableMaxTimers, int maxTimers = 0)
    {
      if (enableMaxTimers)
      {
        _sandbox.ConstraintMaxTimers = maxTimers;
      }

      _sandbox.Assign();
    }





    #endregion

    #region Robustness and limits


    [Test]
    public void ForbiddenCasesAllRange(
      [Range(1, MaxT)]      int maxTimers)
    {
      _sandbox.ConstraintMaxTimers = maxTimers;
      _sandbox.PeriodsToMatch = new Collection<long>();
      _sandbox.Assign();
    }

    [Test]
    [ExpectedException(typeof(AssertionException))]
    public void ForbiddenCasesRobustness(
      [Values(int.MinValue, int.MaxValue, -1, 0, MaxT + 1)]      int maxTimers)
    {
      _sandbox.ConstraintMaxTimers = maxTimers;

      _sandbox.Assign();
    }


    [Test]
    [Ignore("There is a bug in NCrunch. Using CapacityLimitsAllRange2 tests instead")]
    public void CapacityLimitsAllRange(
      [Random(0, MaxP, 10)]          int nbPeriods,
      [Random(1, MaxT, 5)]            int nbTimers,
      [Random(0, 1000*1000, 5)]       int periodsValue)
    {
      _sandbox.ConstraintMaxTimers = nbTimers;
      
      List<long> periods = new List<long>(nbPeriods*2);
      for (int i = 0; i < nbPeriods; i++)
      {
        periods.Add(periodsValue);
      }
      _sandbox.PeriodsToMatch = periods;

      _sandbox.Assign();
    }

    [TestCase(0, 1, 666)]
    [TestCase(0, MaxT, 666)]
    [TestCase(MaxP, 1, 666)]
    [TestCase(MaxP, MaxT, 666)]
    public void CapacityLimitsAllRange2(int nbPeriods,int nbTimers,int periodsValue)
    {
      _sandbox.ConstraintMaxTimers = nbTimers;

      List<long> periods = new List<long>(nbPeriods * 2);
      for (int i = 0; i < nbPeriods; i++)
      {
        periods.Add(periodsValue);
      }
      _sandbox.PeriodsToMatch = periods;

      _sandbox.Assign();
    }


    [Test]
    [ExpectedException(typeof(AssertionException))]
    public void CapacityLimitsRobustness(
      [Values(1, 5, MaxP, MaxP+1)]                                        int nbPeriods, 
      [Values(int.MinValue, -1, 0, 5, MaxT, MaxT+1, int.MaxValue)]        int nbTimers, 
      [Values(int.MinValue, -1, 0, 5, int.MaxValue)]                      int periodsValue)
    {
      _sandbox.ConstraintMaxTimers = nbTimers;

      List<long> periods = new List<long>();
      for (int i = 0; i < nbPeriods; i++)
      {
        periods.Add(periodsValue);
      }
      _sandbox.PeriodsToMatch = periods;

      _sandbox.Assign();


      
      if (nbPeriods >= 0 && nbPeriods <= MaxP
          && nbTimers > 0 && nbTimers <= MaxT
          && periodsValue >= 0
          )
      {
        //Assert.Ignore("This is not a robustness case and has already been tested by nominal cases");
        Assert.Fail("This is not a robustness case and has already been tested by nominal cases");
      }


    }



    #endregion

  }
}
