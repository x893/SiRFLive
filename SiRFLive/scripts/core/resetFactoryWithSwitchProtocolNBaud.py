import scriptGlobals
import scriptUtilities
import scriptSim

def rxInit():
    scriptUtilities.openPorts()
	
    # Initialize
    # set atten
    scriptUtilities.setAtten(defaultAtten) 

    # setup each active com ports     
    comIdx = 0	
    for usePort in scriptGlobals.DutMainPortsList:	
	if (usePort < 0):
	    comIdx = comIdx + 1
	    continue
	myPort = scriptGlobals.DutPortManagersList[comIdx]
	myPort.comm.RxCtrl.SaveTestSetup("RX COM Port","Com" + `usePort`)
	myPort.comm.RxCtrl.SaveTestSetup("RX Serial Number",scriptGlobals.DutNamesList[comIdx])
		
	myPort.comm.RxCtrl.PollSWVersion()
	myPort.comm.ReadAutoReplyData(scriptGlobals.ScriptConfigFilePath)	
	myPort.comm.RxCtrl.SetValidatePostionFlag(True)
	myPort.comm.RxCtrl.OpenChannel("SSB")
	myPort.comm.RxCtrl.OpenChannel("STAT") 
	
	comIdx = comIdx + 1    
    scriptUtilities.init()
    
try:
    General.clsGlobal.Abort = False
    General.clsGlobal.AbortSingle = False
    scriptGlobals.Exiting = False
    scriptGlobals.TestAborted = False
    
    # Read configuration parameters
    # Data in pair key and value
    sCfg = ConfigParser.ConfigParser()    
    sCfg.read(scriptGlobals.ScriptConfigFilePath)
    
    General.clsGlobal.LoopitTimeout = int(scriptUtilities.ConvertToDouble(sCfg.get('TEST_PARAM','TIMEOUT')))
    General.clsGlobal.LoopitIteration = sCfg.getint('TEST_PARAM','NUMBER_OF_LOOP') 
    General.clsGlobal.LoopitResetType = sCfg.get('TEST_PARAM','RESET_TYPE')
    earlyCompletionFlag = sCfg.getint('TEST_PARAM','ALLOW_EARLY_TERMINATION')
    ephCollectTime = sCfg.getint('TEST_PARAM','SOAK_TIME_SEC')
    collectionTime = sCfg.getint('TEST_PARAM','COLLECTION_TIME_SEC')
    
    #scriptGlobals.MainFrame.AutoTestAbortHdlr.SetSiRFLiveEventHdlr += scriptUtilities.abortTest 
    testContinue = True
    if (scriptGlobals.SignalSource == General.clsGlobal.SIM):
	if (scriptSim.isSimRunning() == True):
	    result = MessageBoxEx.Show("SIM is running -- Proceed?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information,20000)
	    if (result == DialogResult.Yes):
		testContinue = True
		scriptSim.simStop()
		scriptSim.simLoad(scriptGlobals.SimFile)
		# scriptSim.simSetAllChanAtten(scriptGlobals.SimInitialAtten)
		scriptSim.simRun()
	    else:
		testContinue = False
	else:
	    scriptSim.simLoad(scriptGlobals.SimFile)
	    # scriptSim.simSetAllChanAtten(scriptGlobals.SimInitialAtten)
	    scriptSim.simRun()
    scriptGlobals.MainFrame.Delay(5)
    if (testContinue == True):	
	# set to high level for factory reset
	if (scriptGlobals.SignalType.ToLower() == "dbhz"):
	    defaultAtten = Utilities.HelperFunctions.GetCalibrationAtten(scriptGlobals.CableLoss,40,scriptGlobals.SignalType)
	elif (scriptGlobals.SignalType.ToLower() == "dbm"):
	    defaultAtten = Utilities.HelperFunctions.GetCalibrationAtten(scriptGlobals.CableLoss,-130,scriptGlobals.SignalType)
	else:
	    print "Signal Type is not correct"
	    defaultAtten = 0
	# set atten
	scriptUtilities.setAtten(defaultAtten)    
	scriptGlobals.getTTFFs = []
	scriptGlobals.getNavs = []
	
	rxInit()	
	
	# wait for nav
	waitTTFFLoops = 0
	while(waitTTFFLoops < 13):
	    if(scriptUtilities.waitForNav() == True):
		break
	    else:
		mainFrame.Delay(10)
	    waitTTFFLoops = waitTTFFLoops + 1
	    mainFrame.Delay(10)	
	
	# Begin test
	scriptUtilities.logApp("*", scriptGlobals.TestBeginLabel)
	print "Number of test levels %d" % (len(scriptGlobals.TestSignalLevelsList))
	for levelIndex in range(0, len(scriptGlobals.TestSignalLevelsList)):
	    level = scriptGlobals.TestSignalLevelsList[levelIndex]    
	    if (scriptGlobals.TestAborted == True):
		break
	    scriptUtilities.logApp("*", scriptGlobals.InitializedLabel)
	    atten = Utilities.HelperFunctions.GetCalibrationAtten(scriptGlobals.CableLoss,level,scriptGlobals.SignalType)	    
	    # Create directory for log files
	    Now = time.localtime(time.time())
	    timeNowStr = time.strftime("%m%d%Y_%H%M%S", Now)
	    comIdx = 0	
	    for usePort in scriptGlobals.DutMainPortsList:	
		if (usePort < 0):
		    comIdx = comIdx + 1
		    continue
		
		myPort = scriptGlobals.DutPortManagersList[comIdx]
		baseName = "%s%s_%s_%s_%s_%s" %(scriptGlobals.TestResultsDirectory,timeNowStr,scriptGlobals.DutNamesList[comIdx],myPort.comm.PortName,scriptGlobals.ScriptName,level)		
		portLogFile = baseName + scriptGlobals.LogFileExtsList[comIdx]
		ttffLogFile = baseName + "_ttff.csv"
		myPort.comm.RxCtrl.PollSWVersion()
		# update Test Info	    
		if (level < 0):
		    testSetupLevelString = "%s%s%s" %("m",`-level`.replace('.','p'),scriptGlobals.SignalType) 	
		else:
		    testSetupLevelString = "%s%s" %(`level`.replace('.','p'),scriptGlobals.SignalType) 
		    
		scriptGlobals.TestID = "%s-%s" % (scriptGlobals.TestName,testSetupLevelString)	 
		Now = time.localtime(time.time())
		scriptGlobals.StartTime = time.strftime("%m/%d/%Y %H:%M:%S", Now)
		myPort.comm.RxCtrl.DutStationSetup.SignalLevel = level
		scriptUtilities.updateDUTInfo(comIdx)
		myPort.comm.m_TestSetup.Atten = atten
		
		myPort.comm.Log.OpenFile(portLogFile)		
		myPort.comm.RxCtrl.ResetCtrl.OpenTTFFLog(ttffLogFile)
		myPort.comm.RxCtrl.ResetCtrl.ResetCount = 0
		myPort.comm.RxCtrl.ResetCtrl.LoopitInprogress = True
		myPort.comm.RxCtrl.ResetCtrl.ResetInterval = General.clsGlobal.LoopitTimeout
		comIdx = comIdx + 1
	    
	    scriptUtilities.setResetInitParams()
	    # ephCollectTime = 900
	    displayStr = "%s: Wait %d sec for DUT to collect GPS information%s Press OK to cancel wait time" % (time.strftime("%Y/%m/%d %H:%M:%S", time.localtime()),ephCollectTime, "\r\n\r\n    ")
	    scriptUtilities.logApp("*",displayStr)
	    result = MessageBoxEx.Show(displayStr, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information,int(ephCollectTime)*1000);
	    logStr = "#### Dropping atten to desired level %f ####" % (level)
	    scriptUtilities.logApp("*", logStr)
	    
	    diffAtten = atten - defaultAtten
	    if ((levelIndex == 0) and (diffAtten > 5) and (atten > 5)):
		dropAtten = divmod(atten,5)
		drop5dBLoop = dropAtten[0]
		restAtten = dropAtten[1]
		
		for dropIndex in range(0, int(drop5dBLoop)+1):		
		    atten1 = 5*dropIndex + defaultAtten
		    scriptUtilities.setAtten(atten1)		
		    mainFrame.Delay(20)
		
		atten1 = restAtten +  atten1
		scriptUtilities.setAtten(atten1)
		
	    else:
		scriptUtilities.setAtten(atten)
	    
	    navStatusCntArray = len(scriptGlobals.DutMainPortsList) * [0]
	    resetIdx = 0
	    resetTotal = 1
	    startTestTime = time.clock()
	    if (General.clsGlobal.LoopitIteration < 0):
		displayStr = "%s: Reset Test runs for %ds" % (time.strftime("%Y/%m/%d %H:%M:%S", time.localtime()),collectionTime)
		resetTotal = 1
		print displayStr
	    else:
		resetTotal = General.clsGlobal.LoopitIteration
	    # for resetIdx in range(0,General.clsGlobal.LoopitIteration):
	    while (resetIdx < resetTotal):
		if (scriptGlobals.TestAborted == True):
		    break
		stopTime = time.clock()
		elapsedTestTime = stopTime - startTestTime
		if ((General.clsGlobal.LoopitIteration < 0) and
		    (collectionTime > 0) and (elapsedTestTime > collectionTime)):
		    # stop test now		    
		    break	    
		
		rLogStr = "####### Reset Number %d #######" %(resetIdx+1)
		scriptUtilities.logApp("*", rLogStr)
		scriptUtilities.reset(General.clsGlobal.LoopitResetType)		 
		navStatusArray = len(scriptGlobals.DutMainPortsList) * [False]
		count = 0
		startDelay = General.clsGlobal.LoopitTimeout/10;
		while(count <= startDelay):
		    if (scriptGlobals.TestAborted == True):
			break
		    print "Wait for nav loop %d" %(count)
		    comIdx = 0    
		    for usePort in scriptGlobals.DutMainPortsList:	
			if (usePort < 0):
			    comIdx = comIdx + 1
			    continue
			myPort = scriptGlobals.DutPortManagersList[comIdx]
			if (navStatusArray[comIdx] == False):
			    navStatusArray[comIdx] = mainFrame.GetNavStatus(myPort.comm.PortName)		    
		
			    if (navStatusArray[comIdx] == True):
				navStatusCntArray[comIdx] = navStatusCntArray[comIdx] + 1			
				logStr = "Loop %d: %s Rx nav -- %d/%d loops" % (resetIdx+1, myPort.comm.PortName,navStatusCntArray[comIdx],General.clsGlobal.LoopitIteration)
				myPort.comm.WriteApp(logStr);
				print logStr		    
			comIdx = comIdx + 1
		    
		    navStatus = True
		    for tmpStatus in navStatusArray:
			navStatus =  navStatus and tmpStatus
		    count = count + 1
		    mainFrame.Delay(10)
		    if (navStatus == True):	
			# all RXs nav
			break		

		#if (navStatus == False):		
		comIdx = 0	
		for usePort in scriptGlobals.DutMainPortsList:	
		    if (usePort < 0):
			comIdx = comIdx + 1
			continue
		    myPort = scriptGlobals.DutPortManagersList[comIdx]
		    # if (navStatusArray[comIdx] == False):
		    myPort.comm.RxCtrl.ResetCtrl.ResetTTFFAvailable = False
		    myPort.comm.RxCtrl.ResetCtrl.LogTTFFCsv()		    
		    myPort.comm.RxCtrl.ResetCtrl.ResetPositionAvailable = False			
		    		
		    comIdx = comIdx + 1    
		resetIdx = resetIdx + 1
		if (collectionTime > 0):
		    resetTotal = resetTotal + 1
		    
	    comIdx = 0    
	    for usePort in scriptGlobals.DutMainPortsList:	
		if (usePort < 0):
		    comIdx = comIdx + 1
		    continue
		myPort = scriptGlobals.DutPortManagersList[comIdx]
		myPort.comm.Log.CloseFile();
		myPort.comm.RxCtrl.ResetCtrl.CloseTTFFLog()
		comIdx = comIdx + 1
	
	# Done now clean up
	scriptGlobals.Exiting = True
	comIdx = 0    
	for usePort in portList:
	    if (usePort < 0):
		comIdx = comIdx + 1
		continue
	    myPort = scriptGlobals.DutPortManagersList[comIdx]
	    myPort.comm.RxCtrl.ResetCtrl.LoopitInprogress = False
	    myPort.comm.ClosePort()
	    # myPort.StopAsyncProcess()
	    comIdx = comIdx + 1	
    
    timeoutSpec = General.clsGlobal.LoopitTimeout - 1;
    ttffLimitStr = `timeoutSpec` + "," + `timeoutSpec` + "," + `timeoutSpec`
    
    myReport = mainFrame.ReportCtrl
    myReport.Percentile = "50,67,95"
    myReport.TTFFLimit = ttffLimitStr
    myReport.HrErrLimit = "50,100,150"
    myReport.TimeoutVal = `timeoutSpec`
    myReport.LimitVal = "100"
    myReport.TTFFReportType = 3;
    myReport.ReportLayout = SiRFLive.Reporting.Report.ReportLayoutType.GroupBySWVersion
    myReport.Summary_Reset_V2(logDirectory)
    
    myReport.ReportLayout = SiRFLive.Reporting.Report.ReportLayoutType.GroupByResetType
    myReport.Summary_Reset_V2(logDirectory)
    
    scriptGlobals.MainFrame.SetScriptDone(True)
    mainFrame.UpdateGUIFromScript()
    print "Done: %s" % (scriptGlobals.ScriptName)
    
except:
    scriptUtilities.ExceptionHandler()