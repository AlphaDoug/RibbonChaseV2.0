//
//  ACAdViewController.h
//  Appcoachs
//
//  Created by JiangAijun on 16/3/14.
//  Copyright © 2016年 JiangAijun. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "ACAdViewControllerEventDelegate.h"
#import "ACShowBannerType.h"

@class ACCustomNavigationBar, ACLoadView;

@interface ACAdViewController : UIViewController

@property (nonatomic, strong) ACLoadView *loadProgressBar;

/**
 *  principal agent
 */
@property (nonatomic, weak) id<ACAdViewControllerEventDelegate> delegate;


/**
 *  custom navigation bar
 */
@property (nonatomic, strong) ACCustomNavigationBar *navBar;


/**
 *  Set status bar style
 */
@property (nonatomic, assign) UIStatusBarStyle statusBarStyle;


/**
 * Set error message
 */
@property (nonatomic, copy) NSString *errorAlertMessage;

/**
 * set color of error message
 */
@property (nonatomic, assign) UIColor *errorAlertMessageTextColor;


/**
 * set font of error message
 */
@property (nonatomic, assign) NSInteger errorAlertMessageTextSize;

/** is reward video or no;default is YES*/
@property (nonatomic, assign) BOOL rewarded;


/**
 *  Request to load image ads
 *
 *  @param slotId    AppcoachS and customers to jointly agreed with the ad bit number  example：1
 *  @param ot        0:portrait （纵向）; 1: landscape （横向） example：0
 */
- (void)setAdInfoWithSlotid:(NSInteger)slotId orientation:(NSInteger)ot;


/**
 *  Request to load video ads
 *
 *  @param slotId    AppcoachS and customers to jointly agreed with the ad bit number  example：2
 */
- (void)setAdInfoWithSlotid:(NSInteger)slotId;


@end
