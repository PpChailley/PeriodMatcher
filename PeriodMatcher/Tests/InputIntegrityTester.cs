using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;

namespace Gbd.PeriodMatching.Tests
{
  public class InputIntegrityTester : PeriodMatcherTester
  {



    #region Smoke Tests & Functional Tests




    [TestCase(false)]
    [TestCase(true, 12)]
    [TestCase(true, ExpectedException = typeof(AssertionException))]
    [TestCase(true, -1, ExpectedException = typeof(AssertionException))]
    [TestCase(true, MaxT + 1, ExpectedException = typeof(AssertionException))]
    public void ForbiddenCasesSmokeTest(bool enableMaxTimers, int maxTimers = 0)
    {
      if (enableMaxTimers)
      {
        Sandbox.ConstraintMaxTimers = maxTimers;
      }

      Sandbox.Assign();
    }


    [Test]
    [ExpectedException(typeof(InvalidOperationException))]
    
    public void NoAccessShouldBeGivenBeforeComputationTestsAreDone()
    {
      Sandbox.PeriodsToMatch.Add(100);
      Sandbox.Assign();

      Sandbox.PeriodsToMatch.Add(200);
      int a = Sandbox.TimersAssignment.Count;
      Assert.That(a, Is.Not.Null);
    }


    #endregion

    #region Robustness and limits


    [Test]
    
    public void ForbiddenCasesAllRange(
      [Range(1, MaxT)]      int maxTimers)
    {
      Sandbox.ConstraintMaxTimers = maxTimers;
      Sandbox.PeriodsToMatch = new Collection<long>();
      Sandbox.Assign();
    }

    [Test]
    
    [ExpectedException(typeof(AssertionException))]
    public void ForbiddenCasesRobustness(
      [Values(int.MinValue, int.MaxValue, -1, 0, MaxT + 1)]      int maxTimers)
    {
      Sandbox.ConstraintMaxTimers = maxTimers;

      Sandbox.Assign();
    }


    [Test]
    [Ignore("There is a bug in NCrunch. Using CapacityLimitsAllRange2 tests instead")]
    public void CapacityLimitsAllRange(
      [Random(0, MaxP, 10)]          int nbPeriods,
      [Random(1, MaxT, 5)]            int nbTimers,
      [Random(0, 1000 * 1000, 5)]       int periodsValue)
    {
      Sandbox.ConstraintMaxTimers = nbTimers;

      List<long> periods = new List<long>(nbPeriods * 2);
      for (int i = 0; i < nbPeriods; i++)
      {
        periods.Add(periodsValue);
      }
      Sandbox.PeriodsToMatch = periods;

      Sandbox.Assign();
    }

    [TestCase(0, 1, 666)]
    [TestCase(0, MaxT, 666)]
    [TestCase(MaxP, 1, 666)]
    [TestCase(MaxP, MaxT, 666)]
    
    public void CapacityLimitsAllRange2(int nbPeriods, int nbTimers, int periodsValue)
    {
      Sandbox.ConstraintMaxTimers = nbTimers;

      List<long> periods = new List<long>(nbPeriods * 2);
      for (int i = 0; i < nbPeriods; i++)
      {
        periods.Add(periodsValue);
      }
      Sandbox.PeriodsToMatch = periods;

      Sandbox.Assign();
    }


    [Test]
    
    [ExpectedException(typeof(AssertionException))]
    public void CapacityLimitsRobustness(
      [Values(1, 5, MaxP, MaxP + 1)]                                        int nbPeriods,
      [Values(int.MinValue, -1, 0, 5, MaxT, MaxT + 1, int.MaxValue)]        int nbTimers,
      [Values(int.MinValue, -1, 0, 5, int.MaxValue)]                      int periodsValue)
    {
      Sandbox.ConstraintMaxTimers = nbTimers;

      List<long> periods = new List<long>();
      for (int i = 0; i < nbPeriods; i++)
      {
        periods.Add(periodsValue);
      }
      Sandbox.PeriodsToMatch = periods;

      Sandbox.Assign();

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
