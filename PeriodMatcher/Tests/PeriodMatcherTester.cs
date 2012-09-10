using System;
using Gbd.PeriodMatching.Matcher;
using NLog;
using NLog.Config;
using NLog.Targets;
using NUnit.Framework;

namespace Gbd.PeriodMatching.Tests
{

  [TestFixture]
  public abstract class PeriodMatcherTester
  {


    protected const int MaxP = AbstractPeriodMatcher.MaxPeriodsSupported;
    protected const int MaxT = AbstractPeriodMatcher.MaxTimersSupported;
    protected const int MaxS32 = int.MaxValue;
    protected const uint MaxU32 = uint.MaxValue;
    protected const int MinS32 = int.MinValue;
    protected const long MaxS64 = long.MaxValue;
    protected const ulong MaxU64 = ulong.MaxValue;
    protected const long MinS64 = long.MinValue;


    protected PeriodMatcher Sandbox;


    public const string ApplicationSourcePath = @"C:\ProjetsDev\trunk\Neosynergix\QA\Tools\PeriodMatcher\PeriodMatcher";
    public const string NlogCfgFile = ApplicationSourcePath + @"\NLog.cfg";
    public const string NlogDefaultLogFile = ApplicationSourcePath + @"\PeriodMatcher.log";

    [TestFixtureSetUp]
    public void TestFixtureSetup()
    {
      //System.Environment.SetEnvironmentVariable("NLOG_GLOBAL_CONFIG_FILE", NlogCfgFile);
      AbstractPeriodMatcher.InitializeNLog();
    }


    [SetUp]
    public void Setup()
    {
      //Log.Trace(" ** SetUp **");
      Sandbox = new PeriodMatcher();
    }


    [TearDown]
    public void TearDown()
    {
      //Log.Trace(" ** TearDown **");
      Sandbox = null;
      //System.GC.Collect();
    }
  }
}
