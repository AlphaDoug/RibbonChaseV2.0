//
//  AdVideoView.h
//  Appcoachs
//
//  Created by Aike on 16/6/8.
//  Copyright © 2016年 JiangAijun. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "ACVideoViewEventDelegate.h"
@class ACVideoAdModel;
@interface ACVideoView : UIView<ACVideoViewEventDelegate>

@property (nonatomic, weak) id<ACVideoViewEventDelegate>delegate;

/** 是否是激励视频*/
@property (nonatomic, assign) BOOL rewarded;

/**
 *  close Video Player
 */
- (void)closeVideoPlayer;

/**
 *  Request to load video ads
 *
 *  @param slotId    AppcoachS and customers to jointly agreed with the ad bit number  example：2
 */
- (void)loadAdWithSlotid:(NSInteger)slotId;

- (void)playWithAdModel:(ACVideoAdModel *)adModel url:(NSURL *)url;


@end
