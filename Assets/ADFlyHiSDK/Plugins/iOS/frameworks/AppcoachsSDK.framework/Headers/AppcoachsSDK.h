//
//  AppcoachsSDK.h
//  Appcoachs
//
//  Created by JiangAijun on 16/3/4.
//  Copyright © 2016年 JiangAijun. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <SystemConfiguration/SystemConfiguration.h>
#import <AdSupport/AdSupport.h>

#import "ACCustomNavigationBar.h"

#import "ACAdViewEventDelegate.h"
#import "ACAdViewControllerEventDelegate.h"
#import "ACVideoViewEventDelegate.h"

#import "ACAdView.h"
#import "ACBannerAdView.h"
#import "ACCardAdView.h"
#import "ACFullScreenAdView.h"

#import "ACAdViewController.h"
#import "ACGridOfferWallViewController.h"
#import "ACListOfferWallViewController.h"
#import "ACInsertScreenViewController.h"

#import "ACVideoView.h"
#import "ACGroupVideoView.h"
#import "ACInsertVideoView.h"
#import "ACPasterVideoView.h"
#import "ACShowBannerType.h"
#import "ACFullScreenController.h"
#import "ACVideoWallController.h"

#import "ACImageAdsModel.h"
#import "ACImageAdModel.h"
#import "ACVideoAdModel.h"
#import "ACVideoAdsModel.h"
#import "ACLineIconInfoModel.h"
#import "ACImpressionInfoModel.h"
#import "ACLineTrackingInfo.h"
#import "UIView+Extension.h"



@class ACImageAdsModel, ACVideoAdsModel;

@interface AppcoachsSDK : NSObject

@property (nonatomic, assign) BOOL isTestEnv;

@property (nonatomic, copy) NSString *adSiteId;

@property (nonatomic, assign) BOOL rewarded;

/**
 *  Switch test environment(test server)  Default as official server
 *
 *  @param toTestEnv Do you want to switch the test server,  YES：test   NO : official
 */
- (void)switchToTestEnv:(BOOL)toTestEnv;


/**
 *  Create AppcoachsSDK manager
 */
+ (AppcoachsSDK *)sharedAppcoachsSDKManager;

/**
 *  get sdk version number
 */
- (NSString *)AppcoachSDKVersion;


/**
 *  Registration of SiteId and Token
 *  @param siteId siteId
 *  @return YES：success，NO:fail
 */
- (BOOL)registerSDKWithSiteId:(NSString *)siteId;


/**
 *  Request to load image ads
 *
 *  @param slotId    AppcoachS and customers to jointly agreed with the ad bit number  example：6
 *  @param ads     Number of ads (server control limit)    example：8
 *  @param oritation  0:portrait   1: landscape    example：0
 *  @param success   Request success callback  (Block)
 *  @param failure   Request fail callback  (Block)
 */
- (void)loadImageAdWithSlotid:(NSInteger)slotId adCounts:(NSInteger)ads Oritation:(NSInteger)oritation Success:(void(^)(ACImageAdsModel *data))success Failure:(void(^)(NSError *error))failure;



/**
 *  Request to load image ads
 *
 *  @param slotId    AppcoachS and customers to jointly agreed with the ad bit number  example：6
 *  @param ads     Number of ads (server control limit)    example：8
 *  @param success   Request success callback  (Block)
 *  @param failure   Request fail callback  (Block)
 */
- (void)loadVideoAdWithSlotid:(NSInteger)slotId adCounts:(NSInteger)ads Success:(void(^)(ACVideoAdsModel *data))success Failure:(void(^)(NSError *error))failure;


/**
 *  Start up Cache
 *
 *  @param slotIds    AppcoachS and customers to jointly agreed with the ad bit number  example：6
 
 */
- (void)startupCacheWithSlotids:(NSArray *)slotIds;

@end
