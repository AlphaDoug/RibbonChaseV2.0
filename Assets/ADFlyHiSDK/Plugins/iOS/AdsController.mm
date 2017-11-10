
#import "UnityAppController.h"
#import "MZADManager.h"

extern "C" {
	int _oritation;

    void _initSDK(char* projId, char* channelId, int oritation){
		_oritation = oritation;
        [[MZADManager shareMZADManager] mz_initMZADManagerWithProjId:[NSString stringWithUTF8String:projId]
                                                        andChannelId:[NSString stringWithUTF8String:channelId]];

		if(oritation == 1) 
		{
			[[MZADManager shareMZADManager] mz_loadADDataWithADStyleId:MZADVideoAD
                                                 deviceOrientation:portraitOnly];
		}
		else 
		{
			[[MZADManager shareMZADManager] mz_loadADDataWithADStyleId:MZADVideoAD
                                                 deviceOrientation:landscapeOnly];
		}
        
    }
   
    void _preLoadAds(){
        if(_oritation == 1) 
		{
			[[MZADManager shareMZADManager] mz_loadADDataWithADStyleId:MZADVideoAD
                                                 deviceOrientation:portraitOnly];
		}
		else 
		{
			[[MZADManager shareMZADManager] mz_loadADDataWithADStyleId:MZADVideoAD
                                                 deviceOrientation:landscapeOnly];
		}
    }

    bool _isADLoaded(){
       return [[MZADManager shareMZADManager] mzADLoadFinished];
    }

    void _showBanner(){
        if (![[MZADManager shareMZADManager] mzADLoadFinished])
            return;
        CGRect screen = [UIScreen mainScreen].bounds;
        [[MZADManager shareMZADManager] mz_showBannerADWithBannerFrame:CGRectMake(screen.size.width/2 - (MIN(screen.size.width, screen.size.height))/2, screen.size.height - 45, MIN(screen.size.width, screen.size.height), 45)
                                              andCurrentViewController:UIApplication.sharedApplication.keyWindow.rootViewController
                                                          andShowState:^(MZADViewShowState state) {
                                                              if (state == MZADViewShowStateFinish)
                                                              {
                                                                  NSLog(@"banner complete");
                                                              }
                                                              else if (state == MZADViewShowStateFailed)
                                                              {
                                                                  NSLog(@"banner failed");
                                                              }
                                                              else if (state == MZADViewShowStateFailed)
                                                              {
                                                                  NSLog(@"banner closed");
                                                              }
                                                              
                                                          }];
    }

    void _hideBanner(){
    }

    void _showMZAds(float x, float y, float w, float h){
        if (![[MZADManager shareMZADManager] mzADLoadFinished])
            return;
        
        [[MZADManager shareMZADManager] mz_showRecomendADWithViewFrame:CGRectMake(x, y, w, h)
                                              andCurrentViewController:UIApplication.sharedApplication.keyWindow.rootViewController
                                                          andShowState:^(MZADViewShowState state) {
                                                              if (state == MZADViewShowStateFinish)
                                                              {
                                                                  NSLog(@"recommend complete");
                                                              }
                                                              else if (state == MZADViewShowStateFailed)
                                                              {
                                                                  NSLog(@"recommend failed");
                                                              }
                                                          }];
        
    }

    void _showFullScreen(){
        if (![[MZADManager shareMZADManager] mzADLoadFinished])
            return;
        
        [[MZADManager shareMZADManager] mz_showFullScreenADWithCurrentViewController:UIApplication.sharedApplication.keyWindow.rootViewController andShowState:^(MZADViewShowState state) {
            if (state == MZADViewShowStateFinish)
            {
                NSLog(@"fullscreen complete");
            }
            else if (state == MZADViewShowStateFailed)
            {
                NSLog(@"fullscreen failed");
            }
            else if (state == MZADViewIsClosed)
            {
                NSLog(@"fullscreen closed");
            }
        }];

    }

    void _showVideo(char* callBackName){
            NSString* cbn = [NSString stringWithUTF8String:callBackName];
            [[MZADManager shareMZADManager] mz_showVideoADWithCurrentViewController:UIApplication.sharedApplication.keyWindow.rootViewController
                                                                       andPlayState:^(MZVideoADPlayState state) {
																		   if (state == MZVideoADPlayStateStart)
                                                                           {
                                                                               UnitySendMessage([@"AdFlyHiSDK" UTF8String], cbn.UTF8String, "0");
                                                                               
                                                                           }
                                                                           else if (state == MZVideoADPlayStateCompleted)
                                                                           {
                                                                               UnitySendMessage([@"AdFlyHiSDK" UTF8String], cbn.UTF8String, "1");
                                                                               
                                                                           }
                                                                           else if (state == MZVideoADPlayStateFailed)
                                                                           {
                                                                               UnitySendMessage([@"AdFlyHiSDK" UTF8String], cbn.UTF8String, "-1");
                                                                           }
                                                                       }];
        }


}
