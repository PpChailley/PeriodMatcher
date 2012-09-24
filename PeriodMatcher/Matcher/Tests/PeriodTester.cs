using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Gbd.PeriodMatching.Matcher.Tests
{
  [TestFixture]
  public class PeriodTester
  {
    private Period _period;

    [SetUp]
    public void SetUp()
    {
      _period = new Period(new VoidMatcher());
    }

    [TestCase(1, 1, Result = true)]
    [TestCase(100, 100, Result = true)]
    [TestCase(int.MaxValue - 1, int.MaxValue - 1, Result = true)]
    [TestCase(int.MaxValue, int.MaxValue, Result = true)]
    [TestCase((long)int.MaxValue + 1, (long)int.MaxValue + 1, Result = true)]
    [TestCase(long.MaxValue - 1, long.MaxValue - 1, Result = true)]
    [TestCase(2, 1, Result = true)]
    [TestCase(4, 1, Result = true)]
    [TestCase(0x8000, 1, Result = true)]
    public bool MatchTester(long period, long timer)
    {
      _period.P = period;

      return _period.Matches(new Timer{T=timer});
    }







  }
}
