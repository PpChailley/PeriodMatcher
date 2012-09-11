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
      Sandbox.PeriodsToMatch.Clear();

      if (enableMaxTimers)
      {
        Sandbox.ConstraintMaxTimers = maxTimers;
      }

      Sandbox.Assign();
    }


    [Test]
    [ExpectedException(typeof(InvalidOperationException))]
    public void InvalidateResultsWhenInputChanges()
    {
      Sandbox.PeriodsToMatch.Add(100);
      Sandbox.Assign();

      Sandbox.PeriodsToMatch.Add(200);
      int a = Sandbox.TimersAssignment.Count;
      Assert.That(a, Is.Not.Null);
    }


    [TestCase(true, true, ExpectedException = typeof(InvalidOperationException))]
    [TestCase(true, false, ExpectedException = typeof(InvalidOperationException))]
    [TestCase(false, true, ExpectedException = typeof(InvalidOperationException))]
    [TestCase(false, false)]
    public void InvalidateResultsWhenParametersChange(bool changeMaxMultiplier, bool changeMaxTimers)
    {
      Sandbox.PeriodsToMatch.Add(100);
      Sandbox.Assign();

      if (changeMaxMultiplier)    Sandbox.ConstraintMaxMultiplier = 64;
      if (changeMaxTimers)        Sandbox.ConstraintMaxTimers = 10;


      int a = Sandbox.TimersAssignment.Count;
      Assert.That(a, Is.Not.Null);
    }


    #endregion

    #region Robustness and limits


    [Test]
    [ExpectedException(typeof(AssertionException))]
    public void ForbiddenToUseNullPeriods()
    {
      Sandbox.Assign();
    }


    [Test]
    [ExpectedException(typeof(AssertionException))]
    public void ForbiddenToUseTooManyPeriods(
      [Values(MaxP+1, MaxP+100)]                      int nbPeriods,
      [Values(1, MaxU32, (long)MaxU32+1, MaxS64)]     long period)
    {
      for (int i =0; i< nbPeriods ; i++)
      {
        Sandbox.PeriodsToMatch.Add(period);
      }

      Sandbox.Assign();
    }


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
    public void CapacityLimitsAllRange(
      [Values(0, 1, 10, 100, MaxP-1, MaxP)]                                             int nbPeriods,
      [Values(1, 10, MaxT-1, MaxT)]                                                     int nbTimers,
      [Values(1, 2, 9, 100, MaxS32-1, MaxS32, (long)MaxS32+1, MaxS64-1, MaxS64)]        long periodsValue)
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
