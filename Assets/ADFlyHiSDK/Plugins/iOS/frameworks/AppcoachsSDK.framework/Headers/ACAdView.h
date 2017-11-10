//
//  ACAdView.h
//  Appcoachs
//
//  Created by JiangAijun on 16/3/8.
//  Copyright © 2016年 JiangAijun. All rights reserved.
//

#import <UIKit/UIKit.h>

#import "ACAdViewEventDelegate.h"

@interface ACAdView : UIView<UIGestureRecognizerDelegate>


@property (nonatomic, weak) id<ACAdViewEventDelegate> delegate;


/**
 *  Request to load image ads
 *
 *  @param slotId    AppcoachS and customers to jointly agreed with the ad bit number  example：6
 *
 *  @param adCounts  Number of ads (server control limit).  
                     if you don't write this parameter, the CardView/BannerAdView/ScreenFullView default is 1.
 *
 *  @param ot        0:portrait   1: landscape    example：0
 */
- (void)loadAdWithSlotid:(NSInteger)slotId adCounts:(NSInteger)adCounts  orientation:(NSInteger)ot;  

- (void)loadAdWithSlotid:(NSInteger)slotId orientation:(NSInteger)ot;

- (void)loadAdCompleteAndCacheImage;

/**
 *  Set the information displayed on the interface (if no network)
 *
 *  @param alertMessage  prompt information
 */
- (void)setErrorAlertMessage:(NSString *)alertMessage;


@end
