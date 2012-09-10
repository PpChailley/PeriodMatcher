using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Gbd.PeriodMatching.Tests;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Gbd.PeriodMatching.Matcher
{
  public abstract class AbstractPeriodMatcher
  {

    private static Logger Log = LogManager.GetCurrentClassLogger();    
    
    public const int MaxTimersSupported = 100;
    public const int MaxPeriodsSupported = 50*1000;


    public abstract void Assign();



    public static void InitializeNLog()
    {
      var config = new LoggingConfiguration();
      var target = new FileTarget {FileName = PeriodMatcherTester.NlogDefaultLogFile};
      config.AddTarget("logfile", target);
      config.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, target));
      LogManager.Configuration = config;

      Log = LogManager.GetCurrentClassLogger();

      Log.Info("NLog Initialized");
    }




    protected int _constraintMaxTimers = 0;
    protected bool _constraintEnableTimers = false;

    public int ConstraintMaxTimers
    {
      set
      {
        _constraintMaxTimers = value;
        _constraintEnableTimers = true;
        _timersAssignmentDone = false;
      }
    }


    protected ICollection<long> _periodsToMatch = null;

    public ICollection<long> PeriodsToMatch
    {
      set { _periodsToMatch = value; }
      get
      {
        _timersAssignmentDone = false;
        return _periodsToMatch ?? (_periodsToMatch = new Collection<long>());
      }
    }

    protected bool _timersAssignmentDone = false;
    protected List<long> _timersAssignment;

    public List<long> TimersAssignment
    {
      get
      {
        if (_timersAssignmentDone == false)
          throw new InvalidOperationException("Cannot access the result of the computation before calling Assign()");

        return new List<long>(_timersAssignment);
      }
    }


    private int _constraintMaxMultiplier = 0;

    public int ConstraintMaxMultiplier
    {
      get { return _constraintMaxMultiplier; }
      set 
      { 
        _constraintMaxMultiplier = value;
        _timersAssignmentDone = false;
      }
    }


  }
}
