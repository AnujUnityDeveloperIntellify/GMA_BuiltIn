namespace UISystem
{
    public static class configuration
    {
        public static bool isFreshApp = true;
    }

    public enum PopupName
    {
        None,
        Loading
    }

    public enum ScreenName
    {
        None,
        BTMUiIntrectionGuidedScreen,
        BTMGrabObjectGuidedScreen,
        OTCGameGuidedScreen,
        OTCUiIntrectionTutorialScreen,
        OTCGrabObjectGuidedTutorialScreen,
        HSLeftThumbTutorialScreen,
        HSRightThumbTutorialScreen,
        HSExitATutorialScreen,
        HSMetaForQuitTutorialScreen,
        OTCGameGuidedHandsTutorialScreen,
        OTCGameGuidedSpotTutorialScreen,
        OTCGameGuidedCleanTutorialScreen,
        OTCGameGuidedTimerTutorialScreen,
        HSGameSecetionScreen,
        BTMBasedModelGuidedScreen,
        BTMStadiumChunksGuidedScreen,
        BTMStadiumTimmerGuidedScreen,
        BTMStadiumCompleteScreen,
        BTMStadiumTimeUpScreen,
        OTCLevelCompleteScreen,
        OTCLevelFailScreen,
        OTCExitSureScreen,
        BTMQuitScreen,
        HSShowTutorialScreen,
        BTMStadiumChunkFindGuidedScreen,
        HSLoadingScreen,
        OTCWelcomeScreen,
        BTMWelcomeScreen,
        GMAUiInteractionGuidedScreen,
        GMAGameStartScreen,
        GMAQuitScreen,
        GMAPickedGuidedScreen,
        GMAChooseCorrectGuidedScreen,
        GMAGameCompleteScreen,
        GMAWelcomeScreen,
        GMALeaderBoardGuidedScreen,
        GMAYearDescriptionMenuScreen,
        GMAGameOverScreen
    }

    public enum AudioClipName
    {
        None,
        Recycling
        // Add more clip names here as needed
    }
}
