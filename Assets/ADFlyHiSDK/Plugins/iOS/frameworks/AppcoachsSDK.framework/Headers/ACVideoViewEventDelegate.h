//
//  AdVideoViewEventDelegate.h
//  Appcoachs
//
//  Created by Aike on 16/6/8.
//  Copyright © 2016年 JiangAijun. All rights reserved.
//
#import <Foundation/Foundation.h>
@protocol ACVideoViewEventDelegate <NSObject>

@optional

/**
 * Load advertisement data success.
 */
- (void)adDataDidLoadSuccess:(UIView *)videoView;

/**
 * Load advertisement data fail.
 */
- (void)adDataDidLoadFail:(UIView *)videoView Error:(NSError *)error;

/**
 * The advertisement view had clicked;
 */
- (void)adViewDidClicked:(UIView *)videoView AdInfo:(NSDictionary*)adInfo;

/**
 * The video advertisement play completed or others;
 */
- (void)adViewDidCompleted:(UIView *)videoView AdInfo:(NSDictionary*) adInfo;

/**
 * The adView had closed.
 */
- (void)adViewDidClose:(UIView *)videoView;

/**
 * The adView start play video
 */
- (void)adViewStartPlayVideo:(UIView *)videoView;


/**
 * The adView Play Video Error
 */
- (void)adViewPlayVideoError:(UIView *)videoView;


#pragma mark - 用于点击全屏切换的代理方法
/**
 *  The adView Full Screen Play
 */
- (void)adViewDidFullScreen:(UIView *)videoView;

/**
 *  The adView Shrink Screen Play
 */
- (void)adViewDidShrinkScreen:(UIView *)videoView;

/**
 *  The adView play duration currentPlaybackTime
 */
- (void)adView:(UIView *)videoView duration:(NSTimeInterval)duration currentPlaybackTime:(NSTimeInterval)currentPlaybackTime;
@end